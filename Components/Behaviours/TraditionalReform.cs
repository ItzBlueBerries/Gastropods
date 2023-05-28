using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class TraditionalReform : GastroBehaviour
    {
        void Reform(IdentifiableType foodType)
        {
            GameObject randomizedForm = GetRandomizedForm(foodType);
            if (randomizedForm == null)
                return;

            SpawnActorAndDestroy(randomizedForm);
        }

        void SpawnActorAndDestroy(GameObject actor)
        {
            SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().TransformFX, transform.position, transform.rotation);
            InstantiateActor(actor, SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, transform.position, transform.rotation);
            Destroyer.DestroyActor(gameObject, "TraditionalReform.Reform.SpawnActorAndDestroy");
        }

        GameObject GetRandomizedForm(IdentifiableType foodType)
        {
            List<IdentifiableType[]> compatibleForms = new List<IdentifiableType[]>();
            foreach (KeyValuePair<IdentifiableType[], IdentifiableTypeGroup[]> keyValuePair in Gastro.GASTROPOD_DIET_DICT)
            {
                if (keyValuePair.Key.Contains(Get<IdentifiableType>("TraditionalGastropod")) || keyValuePair.Key.Contains(Get<IdentifiableType>("TraditionalQueenGastropod")) || keyValuePair.Key.Contains(Get<IdentifiableType>("TraditionalKingGastropod")))
                    continue;
                foreach (IdentifiableTypeGroup group in keyValuePair.Value)
                {
                    if (group.IsMember(foodType))
                        compatibleForms.Add(keyValuePair.Key);
                }
            }
            if (compatibleForms.Count <= 0)
                return null;
            IdentifiableType[] chosenForms = compatibleForms[new System.Random().Next(compatibleForms.Count)];
            return chosenForms[new System.Random().Next(chosenForms.Length)].prefab;
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

            SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);
            Destroyer.DestroyActor(obj, "TraditionalReform.OnCollisionEnter");
            Reform(GetIdentType(obj));
        }
    }
}
