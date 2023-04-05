using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class HarpoonKillOnTouch : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (obj.GetComponent<IdentifiableActor>().identType == gastropod)
                    return;
            }

            if (Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
                Destroyer.DestroyActor(obj, "HarpoonKillOnTouch.OnCollisionEnter");
        }
    }
}
