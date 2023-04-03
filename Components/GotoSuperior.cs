using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class GotoSuperior : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Transform target;
        private Rigidbody rigidBody;
        private double time;

        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float maxDistanceFromSuperior = 3;
        public float searchRadius;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            rigidBody = GetComponent<Rigidbody>();
            time = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));

            if (searchRadius == default)
                searchRadius = UnityEngine.Random.Range(50, 100);

            if (target == null)
                FindSuperiorInScene();
        }

        void Update()
        {
            if (timeDir.HasReached(time) && target == null)
                FindSuperiorInScene(); time = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));
        }

        void FixedUpdate() => MoveToSuperior();

        void FindSuperiorInScene()
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj == gameObject)
                    continue;

                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand >= 0.5f)
                {
                    if (obj.name.Contains(GetComponent<IdentifiableActor>().identType.name.Replace("Gastropod", "").ToLower() + "QueenGastropod"))
                    {
                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                        if (distance <= searchRadius)
                            target = obj.transform; break;
                    }
                }
                else if (rand <= 0.5f && rand >= 0.2f)
                {
                    if (obj.name.Contains(GetComponent<IdentifiableActor>().identType.name.Replace("Gastropod", "").ToLower() + "KingGastropod"))
                    {
                        float distance = Vector3.Distance(transform.position, obj.transform.position);
                        if (distance <= searchRadius)
                            target = obj.transform; break;
                    }
                }
                else if (rand <= 0.2f)
                    target = null;
            }
        }

        void MoveToSuperior()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget >= maxDistanceFromSuperior)
                    rigidBody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
                else
                    rigidBody.velocity = Vector3.zero;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
            }
        }
    }
}
