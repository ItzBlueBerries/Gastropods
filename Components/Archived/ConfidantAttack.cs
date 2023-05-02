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
            GameObject obj = collision.gameObject;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            if (Gastro.IsGastropod(obj.GetComponent<IdentifiableActor>().identType))
                return;

            if (!Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
                return;

            collision.gameObject.GetComponent<SlimeHealth>().currHealth -= 10;
            Destroyer.Destroy(gameObject, "ConfidantAttack.OnCollisionEnter");
        }
    }
}
