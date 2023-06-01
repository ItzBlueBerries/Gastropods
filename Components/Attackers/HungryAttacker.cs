using Il2Cpp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Attackers
{
    internal class HungryAttacker : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Rigidbody gastroBody;
        private Transform target;
        private double fullTime;
        private double searchTime;
        private double delayTime;
        private int amountLeft;
        private bool isFull;

        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float timeWhileFull = 2.5f;
        public float searchRadius;
        public int amountTillFull = 3;
        public int timeTillSearch = UnityEngine.Random.Range(1, 3);

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            gastroBody = GetComponent<Rigidbody>();
            searchTime = timeDir.HoursFromNowOrStart(timeTillSearch);

            if (!gameObject.name.Contains("QueenGastropod") || !gameObject.name.Contains("KingGastropod") && Gastro.IsGastropod(GetComponent<IdentifiableActor>().identType))
                amountTillFull = 6;
            amountLeft = amountTillFull;

            if (searchRadius == default)
                searchRadius = UnityEngine.Random.Range(20, 60);

            if (target == null)
                FindFoodInScene();
        }

        void Update()
        {
            if (timeDir.HasReached(searchTime) && target == null)
                FindFoodInScene(); searchTime = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));

            if (timeDir.HasReached(fullTime) && isFull && fullTime != default)
            {
                amountLeft = amountTillFull;
                fullTime = default;
                isFull = false;
            }

            if (amountLeft == default && !isFull && fullTime == default)
            {
                fullTime = timeDir.HoursFromNowOrStart(timeWhileFull);
                isFull = true;
            }
        }

        void FixedUpdate() => MoveToFood();

        void SetDelayTime() => delayTime = timeDir.WorldTime() + 0.1;

        void FindFoodInScene()
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj == gameObject)
                    continue;

                if (!obj.GetComponent<Rigidbody>())
                    continue;

                if (!obj.GetComponent<IdentifiableActor>())
                    continue;

                if (Gastro.IsGastropod(obj.GetComponent<IdentifiableActor>().identType))
                    return;

                if (!Get<IdentifiableTypeGroup>("MeatGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType) || !Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
                    continue;

                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance <= searchRadius)
                    target = obj.transform; break;
            }
        }

        void MoveToFood()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                gastroBody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
                gastroBody.MoveRotation(Quaternion.Slerp(gastroBody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
            }
        }

        void EatFoodTarget(GameObject obj)
        {
            if (Get<IdentifiableTypeGroup>("MeatGroup").IsMember(gameObject.GetComponent<IdentifiableActor>().identType) || Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
            {
                SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);
                Destroyer.DestroyActor(obj, "HungryAttacker.OnCollisionEnter");
                amountLeft -= 1;
                SetDelayTime();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (isFull)
                return;

            GameObject obj = collision.gameObject;

            if (delayTime != default)
                return;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (obj.GetComponent<IdentifiableActor>().identType == gastropod)
                    return;
            }

            EatFoodTarget(obj);
        }
    }
}
