using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Behaviours
{
    internal class AlwaysBeHoppingAround : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public float repeatTime = 0.5f;
        public float repeatRate = 1;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            InvokeRepeating("DoAHop", repeatTime, repeatRate);
        }

        void DoAHop() => rigidbody.AddForce(transform.up * UnityEngine.Random.Range(5, 6), ForceMode.VelocityChange);
    }
}
