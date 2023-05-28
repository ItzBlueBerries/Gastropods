using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class UnidentifiedTransform : MonoBehaviour
    {
        void SpawnActorAndDestroy(GameObject actor)
        {
            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().TransformFX, transform.position, transform.rotation);
            SRBehaviour.InstantiateActor(actor, SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, transform.position, transform.rotation);
            SceneContext.Instance.PediaDirector.MaybeShowPopup(Get<IdentifiableType>("UnidentifiedGastropod"));
            Destroyer.DestroyActor(gameObject, "UnidentifiedProduce.ProduceRareItem.SpawnActorAndDestroy");
        }

        void TransformIntoActor()
        {
            float rand = UnityEngine.Random.Range(0, 1);
            List<IdentifiableType> rareItems = new List<IdentifiableType>();
            List<SlimeDefinition> rareSlimes = new List<SlimeDefinition>();

            foreach (IdentifiableType item in Gastro.Resource.RARE_RESOURCES)
                rareItems.Add(item);
            rareItems.Add(Get<IdentifiableType>("MoondewNectar"));

            rareSlimes.Add(Get<SlimeDefinition>("Lucky"));
            rareSlimes.Add(Get<SlimeDefinition>("Gold"));

            if (rand >= 0.5f)
            {
                int index = new System.Random().Next(rareItems.Count);
                SpawnActorAndDestroy(rareItems[index].prefab);
            }
            else if (rand <= 0.5f)
            {
                int index = new System.Random().Next(rareSlimes.Count);
                SpawnActorAndDestroy(rareSlimes[index].prefab);
            }
        }

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

            if (!Get<IdentifiableTypeGroup>("CraftGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
                return;

            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);
            Destroyer.DestroyActor(obj, "UnidentifiedProduce.OnCollisionEnter");
            TransformIntoActor();
        }
    }
}
