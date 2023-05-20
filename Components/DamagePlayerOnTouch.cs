using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class DamagePlayerOnTouch : MonoBehaviour
    {
        private DamageSourceDefinition damageSource;

        public int damagePerTouch = 5;

        void Start() => damageSource = Get<DamageSourceDefinition>("GastropodDamageSource");

        void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;

            if (obj.transform == SceneContext.Instance.Player.transform)
                SceneContext.Instance.Player.GetComponent<PlayerDamageable>().Damage(new Damage() { amount = damagePerTouch, sourceObject = gameObject, damageSource = damageSource });
        }
    }
}
