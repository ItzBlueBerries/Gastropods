using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Behaviours
{
    internal class ReproduceOnNearby : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Transform target;
        private double searchCooldown;
        private double reproduceCooldown;

        public GameObject gastropodPrefab;
        public float searchRadius;
        public float reproducersRadius = 20;
        public float spawnRadius = 3;
        public int minSpawnCount = 1;
        public int maxSpawnCount = 2;
        public float minReproduceTime = 3;
        public float maxReproduceTime = 6 * 2;

        public virtual void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            searchCooldown = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));

            if (searchRadius == default)
                searchRadius = UnityEngine.Random.Range(30, 80);

            if (target == null)
                FindKingInScene();
        }

        void Update()
        {
            if (timeDir.HasReached(searchCooldown) && target == null && searchCooldown != default)
            {
                FindKingInScene();
                searchCooldown = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3 * 2));
            }

            if (timeDir.HasReached(reproduceCooldown) && target != null && reproduceCooldown != default)
            {
                ReproduceWhenNearby();
                reproduceCooldown = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(minReproduceTime, maxReproduceTime));
            }
        }

        void FindKingInScene()
        {
            foreach (GameObject gameObject in FindObjectsOfType<GameObject>())
            {
                if (gameObject.name.Contains(GetComponent<IdentifiableActor>().identType.name.Replace("QueenGastropod", "").ToLower() + "KingGastropod"))
                {
                    float distance = Vector3.Distance(transform.position, gameObject.transform.position);
                    if (distance <= searchRadius)
                    {
                        target = gameObject.transform;
                        reproduceCooldown = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(minReproduceTime, maxReproduceTime));
                        break;
                    }
                }
            }
        }

        void ReproduceWhenNearby()
        {
            if (target != null)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= reproducersRadius)
                {
                    int spawnCount = UnityEngine.Random.Range(minSpawnCount, maxSpawnCount);
                    for (int i = 0; i < spawnCount; i++)
                    {
                        // Generate a random angle in radians
                        float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

                        // Calculate the random position within the radius
                        Vector3 spawnPosition = transform.position + new Vector3(spawnRadius * Mathf.Cos(randomAngle), 0f, spawnRadius * Mathf.Sin(randomAngle));

                        // Spawn the object at the random position
                        GameObject instantiatedGastropod = SRBehaviour.InstantiateActor(gastropodPrefab, SceneContext.Instance.RegionRegistry.CurrentSceneGroup, spawnPosition, Quaternion.identity);
                        SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().ProduceFX, instantiatedGastropod.transform.position, Quaternion.identity);
                    }
                }
                else
                    return;
            }
        }
    }
}
