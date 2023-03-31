using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Assist
{
    internal class ModSpawner
    {
        public static void AddToFields(string sceneName, IdentifiableType identifiableType, float weight)
        {
            switch (sceneName.Contains("zoneFields"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(new SlimeSet.Member
                                {
                                    prefab = identifiableType.prefab,
                                    identType = identifiableType,
                                    weight = weight
                                }).ToArray();
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToStrand(string sceneName, IdentifiableType identifiableType, float weight)
        {
            switch (sceneName.Contains("zoneStrand"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(new SlimeSet.Member
                                {
                                    prefab = identifiableType.prefab,
                                    identType = identifiableType,
                                    weight = weight
                                }).ToArray();
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToGorge(string sceneName, IdentifiableType identifiableType, float weight)
        {
            switch (sceneName.Contains("zoneGorge"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(new SlimeSet.Member
                                {
                                    prefab = identifiableType.prefab,
                                    identType = identifiableType,
                                    weight = weight
                                }).ToArray();
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToBluffs(string sceneName, IdentifiableType identifiableType, float weight)
        {
            switch (sceneName.Contains("zoneBluffs"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(new SlimeSet.Member
                                {
                                    prefab = identifiableType.prefab,
                                    identType = identifiableType,
                                    weight = weight
                                }).ToArray();
                            }
                        }
                        break;
                    }
            }
        }
    }
}
