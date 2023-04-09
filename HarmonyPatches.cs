﻿using HarmonyLib;
using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Script.Util;
using Il2CppMonomiPark.SlimeRancher.UI.Localization;
using MelonLoader;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using System.Collections;
using System.Linq;

namespace Gastropods
{
    internal class HarmonyPatches
    {
        [HarmonyPatch(typeof(AutoSaveDirector), "Awake")]
        internal static class PatchAutoSaveDirectorAwake
        {
            public static void Prefix(AutoSaveDirector __instance)
            {
                foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
                {
                    Get<IdentifiableTypeGroup>("VaccableNonLiquids").memberTypes.Add(gastropod);
                    Get<IdentifiableTypeGroup>("MeatGroup").memberTypes.Add(gastropod);
                    __instance.identifiableTypes.memberTypes.Add(gastropod);
                }
            }
        }

        [HarmonyPatch(typeof(LocalizationDirector), "LoadTables")]
        internal static class LocalizationDirectorLoadTablePatch
        {
            public static void Postfix(LocalizationDirector __instance)
            {
                MelonCoroutines.Start(LoadTable(__instance));
            }

            private static IEnumerator LoadTable(LocalizationDirector director)
            {
                WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.01f);
                yield return waitForSecondsRealtime;
                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<string, StringTable> keyValuePair in director.Tables)
                {
                    if (addedTranslations.TryGetValue(keyValuePair.Key, out var dictionary))
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, string> keyValuePair2 in dictionary)
                        {
                            keyValuePair.Value.AddEntry(keyValuePair2.Key, keyValuePair2.Value);
                        }
                    }
                }
                yield break;
            }

            public static LocalizedString AddTranslation(string table, string key, string localized)
            {
                System.Collections.Generic.Dictionary<string, string> dictionary;
                if (!addedTranslations.TryGetValue(table, out dictionary))
                {
                    dictionary = new System.Collections.Generic.Dictionary<string, string>(); ;
                    addedTranslations.Add(table, dictionary);
                }
                dictionary.Add(key, localized);
                StringTable table2 = LocalizationUtil.GetTable(table);
                StringTableEntry stringTableEntry = table2.AddEntry(key, localized);
                return new LocalizedString(table2.SharedData.TableCollectionName, stringTableEntry.SharedEntry.Id);
            }

            public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> addedTranslations = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>>();
        }
    }

    internal class OtherHarmonyPatches
    {
        [HarmonyPatch(typeof(SlimeEat), "EatAndProduce")]
        internal class PatchSlimeEatProduce
        {
            private static bool Prefix(SlimeEat __instance, SlimeDiet.EatMapEntry em)
            {
                foreach (IdentifiableType definition in Get<IdentifiableTypeGroup>("BaseSlimeGroup").memberTypes)
                {
                    SlimeDefinition slimeDefinition = definition.Cast<SlimeDefinition>();
                    if (slimeDefinition == null || slimeDefinition.Diet == null)
                        continue;
                    if (__instance.slimeDefinition == slimeDefinition)
                    {
                        if (em.eatsIdent == Get<IdentifiableType>("CrepeGastropod"))
                        {
                            em.isFavorite = true;
                            em.favoriteProductionCount = 6;
                        }
                    }
                }

                return true;
            }
        }
    }
}