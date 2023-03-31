using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class CompanionController : MonoBehaviour
    {
        private Transform target;
        private Rigidbody rigidBody;

        public float moveSpeed = 5f;
        public float rotationSpeed = 5f;
        public float maxDistanceFromPlayer = 3.5f;

        void Start()
        {
            target = SceneContext.Instance.Player.transform;
            rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                // Move towards the target
                Vector3 direction = (target.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget >= maxDistanceFromPlayer)
                    rigidBody.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
                else
                    rigidBody.velocity = Vector3.zero;

                // Rotate towards the target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
