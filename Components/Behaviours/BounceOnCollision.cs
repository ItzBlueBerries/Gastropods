using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Behaviours
{
    internal class BounceActorOnCollision : MonoBehaviour
    {
        public float bounceForce = 7;

        void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.GetComponent<Rigidbody>())
                return;

            if (!collision.gameObject.GetComponent<IdentifiableActor>())
                return;

            Vector3 bounceDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody rigidBody = collision.gameObject.GetComponent<Rigidbody>();

            rigidBody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
