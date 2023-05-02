using Gastropods.Components.Behaviours;
using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Resource
{
    internal class JuicyProduction : EatenBehaviour
    {
        void Awake() => useFavoriteFX = true;

        public override void WhenEaten(Collision collision)
        {
            GameObject obj = collision.gameObject;

            MelonLoader.MelonLogger.Msg("did not get past gastrpod check if statement");

            MelonLoader.MelonLogger.Msg(GetIdentType(obj).name);
            MelonLoader.MelonLogger.Msg(Gastro.IsQueenGastropod(GetIdentType(obj)));
            if (!Gastro.IsQueenGastropod(GetIdentType(obj)))
                return;

            MelonLoader.MelonLogger.Msg("did not get past reproduce on nearby if statement");

            if (!obj.GetComponent<ReproduceOnNearby>())
                return;

            MelonLoader.MelonLogger.Msg("got past if statement");

            // Generate a random angle in radians
            float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

            // Calculate the random position within the radius
            Vector3 spawnPosition = transform.position + new Vector3(obj.GetComponent<ReproduceOnNearby>().spawnRadius * Mathf.Cos(randomAngle), 0f, obj.GetComponent<ReproduceOnNearby>().spawnRadius * Mathf.Sin(randomAngle));

            // Spawn the object at the random position
            GameObject instantiatedGastropod = InstantiateActor(obj.GetComponent<ReproduceOnNearby>().gastropodPrefab, SceneContext.Instance.RegionRegistry.CurrentSceneGroup, spawnPosition, Quaternion.identity);
            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().ProduceFX, instantiatedGastropod.transform.position, Quaternion.identity);
        }
    }
}
