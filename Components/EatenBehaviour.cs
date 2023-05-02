using Il2Cpp;
using Il2CppSystem.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class EatenBehaviour : GastroBehaviour
    {
        public bool useFavoriteFX = false;

        public virtual void WhenEaten(Collision collision) { } // => throw new NotImplementedException("WhenEaten is required to be implemented in " + GetType().Name + ".");

        void OnCollisionEnter(Collision collision)
        {
            GameObject obj = collision.gameObject;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            WhenEaten(collision);
            MelonLoader.MelonLogger.Msg("called when eaten");

            if (useFavoriteFX)
                SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFavoriteFX, transform.position, transform.rotation);
            else
                SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);

            Destroyer.DestroyActor(gameObject, "GastroBehaviour.OnCollisionEnter");
        }
    }
}
