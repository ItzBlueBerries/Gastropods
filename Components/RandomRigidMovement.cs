using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class RandomRigidMovement : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public float repeatTime = 0.1f;
        public float repeatRate = 1;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            InvokeRepeating("RandomizedForce", repeatTime, repeatRate);
        }

        void RandomizedForce()
        {
            Array forceModes = Enum.GetValues(typeof(ForceMode));
            ForceMode randomForceMode = (ForceMode)forceModes.GetValue(UnityEngine.Random.Range(0, forceModes.Length));

            rigidbody.AddForce(transform.forward * UnityEngine.Random.Range(4, 10), randomForceMode);
            rigidbody.AddForce(transform.up * UnityEngine.Random.Range(4, 10), randomForceMode);
        }
    }
}
