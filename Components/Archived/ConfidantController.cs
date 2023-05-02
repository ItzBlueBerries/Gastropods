using Il2Cpp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class ConfidantController : MonoBehaviour
    {
        private TimeDirector timeDir;
        private List<GameObject> attackedObjects = new List<GameObject>();
        private List<Collider> collidersDetected = new List<Collider>();
        private double timeTillRefill;
        private int totalConfidantsLeft;
        private bool refilling;
        private bool attackedTarget;

        public GameObject confidantPrefab;
        public int confidantCount = 1;
        public float searchRadius = 10;
        public float confidantSpeed = 3;
        public float reloadTime = 2.5f;
        public int maxColliders = 6;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            totalConfidantsLeft = confidantCount;

            if (Gastro.IsKingGastropod(GetComponent<IdentifiableActor>().identType) || Gastro.IsQueenGastropod(GetComponent<IdentifiableActor>().identType))
                totalConfidantsLeft = 3;

            InvokeRepeating("AttackNearestSlime", 3, 1);
        }

        void Update()
        {
            if (totalConfidantsLeft == default && !refilling && timeTillRefill == default)
            {
                timeTillRefill = timeDir.HoursFromNowOrStart(reloadTime);
                refilling = true;
            }

            if (timeDir.HasReached(timeTillRefill) && refilling && timeTillRefill != default)
            {
                totalConfidantsLeft = confidantCount;
                timeTillRefill = default;
                refilling = false;
            }
        }

        public int CheckConfidantCount() => totalConfidantsLeft;

        IEnumerator SetAttackedTarget()
        {
            attackedTarget = true;
            yield return new WaitForSeconds(1.0f);
            attackedTarget = false;
        }

        void AttackWithConfidant(Collider collider)
        {
            if (collider == null)
                return;
            GameObject confidant = Instantiate(confidantPrefab);
            confidant.GetComponent<Rigidbody>().MovePosition(confidant.transform.position + (collider.transform.position - confidant.transform.position).normalized * confidantSpeed * Time.fixedDeltaTime);
            confidant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(confidant.GetComponent<Rigidbody>().rotation, Quaternion.LookRotation(collider.transform.position), 5 * Time.fixedDeltaTime));

            Rigidbody rigidBody = collider.GetComponent<Rigidbody>();
            Vector3 direction = (collider.transform.position - confidant.transform.position).normalized;
            rigidBody.MovePosition(transform.position + direction * Time.fixedDeltaTime);

            totalConfidantsLeft -= 1;
        }

        void AttackNearestSlime()
        {
            if (refilling)
                return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("Actor"));
            foreach (Collider collider in colliders)
            {
                if (collidersDetected.Count >= maxColliders)
                    break;

                if (!collider.GetComponent<Rigidbody>())
                    continue;

                if (!collider.GetComponent<IdentifiableActor>())
                    continue;

                if (!Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(collider.GetComponent<IdentifiableActor>().identType))
                    continue;

                if (!collidersDetected.Contains(collider))
                    collidersDetected.Add(collider);
            }

            for (int i = collidersDetected.Count - 1; i >= 0; i--)
            {
                if (!colliders.Contains(collidersDetected[i]))
                    collidersDetected.RemoveAt(i);
            }

            foreach (Collider collider in collidersDetected)
            {
                if (attackedObjects.Contains(collider.gameObject))
                    continue;

                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget <= 5 && totalConfidantsLeft > 0 && !attackedTarget)
                {
                    AttackWithConfidant(collider);
                    attackedObjects.Add(collider.gameObject);
                    StartCoroutine("SetShotTarget");
                }
            }
        }
    }
}
