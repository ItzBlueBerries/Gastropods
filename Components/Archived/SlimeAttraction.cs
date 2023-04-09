using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Playables;

namespace Gastropods.Components
{
    internal class SlimeAttraction : MonoBehaviour
    {
        private List<Collider> collidersDetected = new List<Collider>();

        public float maxColliders = 6;
        public float attractionRadius = 20;

        void FixedUpdate() => AttractNearestSlime();

        void AttractNearestSlime()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius, LayerMask.GetMask("Actor"));

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
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget <= attractionRadius)
                {
                    AttractSlime(collider);
                }
            }
        }

        void AttractSlime(Collider collider)
        {
            if (!collider.GetComponent<Rigidbody>())
                return;

            if (!collider.GetComponent<IdentifiableActor>())
                return;

            if (!Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(collider.GetComponent<IdentifiableActor>().identType))
                return;

            Rigidbody rigidBody = collider.GetComponent<Rigidbody>();
            Vector3 direction = transform.position - collider.transform.position;
            rigidBody.AddForce(direction.normalized);
        }
    }
}
