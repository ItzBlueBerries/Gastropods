using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    public class ObjectTwirl : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public float torqueForce = 10;

        void Start() => rigidbody = GetComponent<Rigidbody>();

        void FixedUpdate() => rigidbody.AddTorque(transform.up * torqueForce);
    }
}
