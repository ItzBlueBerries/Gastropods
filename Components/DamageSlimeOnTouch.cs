using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class DamageSlimeOnTouch : GastroBehaviour
    {
        private DamageSourceDefinition damageSource;

        public int damagePerTouch = 2;

        void Start() => damageSource = Get<DamageSourceDefinition>("GastropodDamageSource");

        void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (GetIdentType(obj) == gastropod)
                    return;
            }

            if (Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(GetIdentType(obj)))
            {
                if (obj.GetComponent<SlimeHealth>())
                    obj.GetComponent<SlimeHealth>().Damage(new Damage() { amount = damagePerTouch, sourceObject = gameObject, damageSource = damageSource });
            }
        }
    }
}
