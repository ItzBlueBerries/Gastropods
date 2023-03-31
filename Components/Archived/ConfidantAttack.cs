using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class ConfidantAttack : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Rigidbody>())
            {
                if (collision.gameObject.GetComponent<IdentifiableActor>().identType.Cast<SlimeDefinition>())
                {
                    collision.gameObject.GetComponent<SlimeHealth>().currHealth -= 10;
                    Destroyer.Destroy(gameObject, "ConfidantController.Update");
                }
            }
        }
    }
}
