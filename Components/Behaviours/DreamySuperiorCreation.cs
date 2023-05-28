using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class DreamySuperiorCreation : GastroBehaviour
    {
        private TimeDirector timeDir;
        private double timeTillNextSpawn;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            timeTillNextSpawn = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(24, 48));
        }

        void Update()
        {
            if (timeDir.HasReached(timeTillNextSpawn))
            {
                if (IsNighttime())
                    SpawnSuperiors();
                timeTillNextSpawn = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(24, 48));
            }
        }

        bool IsNighttime() => timeDir.CurrHourOrStart() >= 18 || timeDir.CurrHourOrStart() < 6;

        void SpawnSuperiors()
        {
            GameObject queenSuperior = Get<IdentifiableType>("DreamyQueenGastropod").prefab;
            GameObject kingSuperior = Get<IdentifiableType>("DreamyKingGastropod").prefab;

            float rand = UnityEngine.Random.Range(0f, 1f);
            GameObject instantiatedQueenGastropod = null;
            GameObject instantiatedKingGastropod = null;

            if (rand >= 0.5f)
            {
                instantiatedQueenGastropod = InstantiateActor(
                    queenSuperior,
                    SceneContext.Instance.RegionRegistry.CurrentSceneGroup,
                    transform.position + new Vector3(5 * Mathf.Cos(UnityEngine.Random.Range(0f, Mathf.PI * 2)), 0f, 5 * Mathf.Sin(UnityEngine.Random.Range(0f, Mathf.PI * 2))),
                    Quaternion.identity
                );
            }
            else if (rand < 0.5f)
            {
                instantiatedKingGastropod = InstantiateActor(
                    kingSuperior,
                    SceneContext.Instance.RegionRegistry.CurrentSceneGroup,
                    transform.position + new Vector3(5 * Mathf.Cos(UnityEngine.Random.Range(0f, Mathf.PI * 2)), 0f, 5 * Mathf.Sin(UnityEngine.Random.Range(0f, Mathf.PI * 2))),
                    Quaternion.identity
                );
            }

            SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().ProduceFX, instantiatedQueenGastropod.transform.position, Quaternion.identity);
            SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().ProduceFX, instantiatedKingGastropod.transform.position, Quaternion.identity);
        }
    }
}
