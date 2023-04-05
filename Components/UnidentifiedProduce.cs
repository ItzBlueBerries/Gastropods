using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;

namespace Gastropods.Components
{
    internal class UnidentifiedProduce : MonoBehaviour
    {
        void ProduceRareActor()
        {
            void SpawnActorAndDestroy(GameObject actor)
            {
                SRBehaviour.InstantiateActor(actor, SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, transform.position, transform.rotation);

                GetComponent<Rigidbody>().AddForce(new Vector3(Randoms.SHARED.GetFloat(225f), 450f, Randoms.SHARED.GetFloat(225f)));
                SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Phosphor").prefab.GetComponent<DestroyOutsideHoursOfDay>().destroyFX, transform.position, transform.rotation);

                Destroyer.DestroyActor(gameObject, "UnidentifiedProduce.ProduceRareItem.SpawnActorAndDestroy");
            }

            float rand = UnityEngine.Random.Range(0, 1);
            List<IdentifiableType> rareItems = new List<IdentifiableType>();
            List<SlimeDefinition> rareSlimes = new List<SlimeDefinition>();

            foreach (IdentifiableType item in Gastro.Items.RARE_ITEMS)
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

            if (Gastro.IsGastropod(obj.GetComponent<IdentifiableActor>().identType))
                return;

            foreach (IdentifiableTypeGroup identifiableTypeGroup in Resources.FindObjectsOfTypeAll<IdentifiableTypeGroup>())
            {
                if (!identifiableTypeGroup.IsMember(obj.GetComponent<IdentifiableActor>().identType))
                    return;
            }

            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);
            ProduceRareActor();
        }
    }
}
