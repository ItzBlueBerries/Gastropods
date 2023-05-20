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
        public static void AddToFields(string sceneName, IdentifiableType[] identifiableTypes, float weight)
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
                                foreach (IdentifiableType identifiableType in identifiableTypes)
                                {
                                    SlimeSet.Member member = new SlimeSet.Member
                                    {
                                        prefab = identifiableType.prefab,
                                        identType = identifiableType,
                                        weight = weight
                                    };

                                    if (spawnConstraint.slimeset.members.Contains(member))
                                        break;

                                    spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                }
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToStrand(string sceneName, IdentifiableType[] identifiableTypes, float weight)
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
                                foreach (IdentifiableType identifiableType in identifiableTypes)
                                {
                                    SlimeSet.Member member = new SlimeSet.Member
                                    {
                                        prefab = identifiableType.prefab,
                                        identType = identifiableType,
                                        weight = weight
                                    };

                                    if (spawnConstraint.slimeset.members.Contains(member))
                                        break;

                                    spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                }
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToGorge(string sceneName, IdentifiableType[] identifiableTypes, float weight)
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
                                foreach (IdentifiableType identifiableType in identifiableTypes)
                                {
                                    SlimeSet.Member member = new SlimeSet.Member
                                    {
                                        prefab = identifiableType.prefab,
                                        identType = identifiableType,
                                        weight = weight
                                    };

                                    if (spawnConstraint.slimeset.members.Contains(member))
                                        break;

                                    spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                }
                            }
                        }
                        break;
                    }
            }
        }

        public static void AddToBluffs(string sceneName, IdentifiableType[] identifiableTypes, float weight)
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
                                foreach (IdentifiableType identifiableType in identifiableTypes)
                                {
                                    SlimeSet.Member member = new SlimeSet.Member
                                    {
                                        prefab = identifiableType.prefab,
                                        identType = identifiableType,
                                        weight = weight
                                    };

                                    if (spawnConstraint.slimeset.members.Contains(member))
                                        break;

                                    spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                }
                            }
                        }
                        break;
                    }
            }
        }

        internal class Other
        {
            public static void AddToPinkCanyon(string sceneName, IdentifiableType[] identifiableTypes, float weight)
            {
                switch (sceneName == "zoneStrand_Area2")
                {
                    case true:
                        {
                            IEnumerable<DirectedSlimeSpawner> source = GameObject.Find("zoneStrand_Area2/cellPinkCanyon/Sector").GetComponentsInChildren<DirectedSlimeSpawner>();
                            foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                            {
                                foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                                {
                                    foreach (IdentifiableType identifiableType in identifiableTypes)
                                    {
                                        SlimeSet.Member member = new SlimeSet.Member
                                        {
                                            prefab = identifiableType.prefab,
                                            identType = identifiableType,
                                            weight = weight
                                        };

                                        if (spawnConstraint.slimeset.members.Contains(member))
                                            break;

                                        spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                    }
                                }
                            }
                            break;
                        }
                }
            }

            public static void AddToSwamp(string sceneName, IdentifiableType[] identifiableTypes, float weight)
            {
                switch (sceneName == "zoneStrand_Area2")
                {
                    case true:
                        {
                            IEnumerable<DirectedSlimeSpawner> source = GameObject.Find("zoneStrand_Area2/cellSwamp/Sector").GetComponentsInChildren<DirectedSlimeSpawner>();
                            foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                            {
                                foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                                {
                                    foreach (IdentifiableType identifiableType in identifiableTypes)
                                    {
                                        SlimeSet.Member member = new SlimeSet.Member
                                        {
                                            prefab = identifiableType.prefab,
                                            identType = identifiableType,
                                            weight = weight
                                        };

                                        if (spawnConstraint.slimeset.members.Contains(member))
                                            break;

                                        spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                    }
                                }
                            }
                            break;
                        }
                }
            }
        }
    }
}
