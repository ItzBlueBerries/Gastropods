using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class ObjectRotation : MonoBehaviour
    {
        private Transform m_Transform;

        public Vector3 RotationSpeed = new Vector3(0, 90, 0);

        protected virtual void Start() => m_Transform = transform;

        protected virtual void Update() => m_Transform.Rotate(RotationSpeed * Time.deltaTime);
    }
}
