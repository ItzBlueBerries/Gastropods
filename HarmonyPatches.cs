using HarmonyLib;
using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Script.Util;
using Il2CppMonomiPark.SlimeRancher.UI.Localization;
using MelonLoader;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using System.Collections;
using Il2CppMonomiPark.SlimeRancher.UI.Pedia;
using Gastropods.Assist;
using Il2CppMonomiPark.SlimeRancher;
using Il2CppMonomiPark.SlimeRancher.DataModel;
using System.Dynamic;
using Gastropods.Data.Gastropods;
using System.Linq.Expressions;
using Il2CppMonomiPark.SlimeRancher.UI;

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
                    gastropod.foodGroup = Get<IdentifiableTypeGroup>("MeatGroup");
                    __instance.identifiableTypes.memberTypes.Add(gastropod);
                }

                foreach (IdentifiableType resource in Gastro.Resource.RESOURCES)
                {
                    Get<IdentifiableTypeGroup>("VaccableNonLiquids").memberTypes.Add(resource);
                    Get<IdentifiableTypeGroup>("CraftGroup").memberTypes.Add(resource);
                    // __instance.identifiableTypes.memberTypes.Add(resource);
                }
            }
        }

        [HarmonyPatch(typeof(SavedGame))]
        internal static class SavedGamePushPatch
        {
            [HarmonyPrefix]
            [HarmonyPatch(nameof(SavedGame.Push), typeof(GameModel))]
            public static void PushGameModel(SavedGame __instance)
            {
                foreach (PediaEntry pediaEntry in PatchPediaDirector.addedPedias)
                {
                    if (!__instance.pediaEntryLookup.ContainsKey(pediaEntry.GetPersistenceId()))
                        __instance.pediaEntryLookup.Add(pediaEntry.GetPersistenceId(), pediaEntry);
                }
            }
        }

        [HarmonyPatch(typeof(PediaDirector), "Awake")]
        internal static class PatchPediaDirector
        {
            internal static HashSet<PediaEntry> addedPedias = new HashSet<PediaEntry>();

            public static void Prefix(PediaDirector __instance)
            {
                List<Sprite> gastropodCategoryIcons = new List<Sprite>()
                {
                    CreateSprite(LoadImage("Files.Icons.Categories.iconCategoryGastropods")),
                    CreateSprite(LoadImage("Files.Icons.Categories.iconCategoryGastropodsToxin")),
                    CreateSprite(LoadImage("Files.Icons.Categories.iconCategoryGastropodsHare"))
                };

                if (Get<PediaEntryCategory>("Gastropods"))
                    Get<PediaEntryCategory>("Gastropods").icon = gastropodCategoryIcons[new System.Random().Next(gastropodCategoryIcons.Count)];

                PediaEntryCategory gastropodsCategory = null;
                PediaEntryCategory pediaEntryCategory = SRSingleton<SceneContext>.Instance.PediaDirector.entryCategories.items.ToArray().First(x => x.name == "Resources");

                if (!Get<PediaEntryCategory>("Gastropods"))
                    gastropodsCategory = ScriptableObject.CreateInstance<PediaEntryCategory>();

                if (gastropodsCategory != null)
                {
                    gastropodsCategory.hideFlags |= HideFlags.HideAndDontSave;
                    gastropodsCategory.name = "Gastropods";
                    gastropodsCategory.title = LocalizationDirectorLoadTablePatch.AddTranslation("Pedia", "m.gastropods", "Gastropods");
                    gastropodsCategory.icon = gastropodCategoryIcons[new System.Random().Next(gastropodCategoryIcons.Count)];
                    gastropodsCategory.menuImage = gastropodsCategory.icon;
                    gastropodsCategory.itemAdapterPrefab = pediaEntryCategory.itemAdapterPrefab;
                    gastropodsCategory.detailsPrefab = pediaEntryCategory.detailsPrefab;
                    gastropodsCategory.detailsInfo = pediaEntryCategory.detailsInfo;
                    gastropodsCategory.detailsButton = pediaEntryCategory.detailsButton;
                    gastropodsCategory.altDetailsButton = pediaEntryCategory.altDetailsButton;
                }

                if (!__instance.entryCategories.items.Contains(Get<PediaEntryCategory>("Gastropods")))
                    __instance.entryCategories.items.Add(Get<PediaEntryCategory>("Gastropods"));
                if (!Get<PediaMenuConfiguration>("PediaMenu").categories.Contains(Get<PediaEntryCategory>("Gastropods")))
                    Get<PediaMenuConfiguration>("PediaMenu").categories.Add(Get<PediaEntryCategory>("Gastropods"));

                ModRegistry.LoadPedias();

                foreach (var pediaEntry in addedPedias)
                {
                    var identPediaEntry = pediaEntry.TryCast<IdentifiablePediaEntry>();
                    if (identPediaEntry && !__instance.identDict.ContainsKey(identPediaEntry.identifiableType))
                        __instance.identDict.Add(identPediaEntry.identifiableType, pediaEntry);
                }
            }

            public static IdentifiablePediaEntry CreateIdentifiableEntry(IdentifiableType identifiableType, string pediaEntryName, PediaTemplate pediaTemplate,
                LocalizedString pediaTitle, LocalizedString pediaIntro, LocalizedString actionButtonLabel, LocalizedString infoButtonLabel, bool unlockedInitially = false)
            {
                if (Get<IdentifiablePediaEntry>(pediaEntryName))
                    return null;

                IdentifiablePediaEntry identifiablePediaEntry = ScriptableObject.CreateInstance<IdentifiablePediaEntry>();

                identifiablePediaEntry.hideFlags |= HideFlags.HideAndDontSave;
                identifiablePediaEntry.name = pediaEntryName;
                identifiablePediaEntry.identifiableType = identifiableType;
                identifiablePediaEntry.template = pediaTemplate;
                identifiablePediaEntry.title = pediaTitle;
                identifiablePediaEntry.description = pediaIntro;
                identifiablePediaEntry.isUnlockedInitially = unlockedInitially;
                identifiablePediaEntry.actionButtonLabel = actionButtonLabel;
                identifiablePediaEntry.infoButtonLabel = infoButtonLabel;

                return identifiablePediaEntry;
            }

            public static void AddIdentifiablePage(string pediaEntryName, int pageNumber, string pediaText = "Placeholder Text. (Please set)", bool isHowToUse = false)
            {
                IdentifiablePediaEntry identifiablePediaEntry = Get<IdentifiablePediaEntry>(pediaEntryName);

                string CreatePageKey(string prefix)
                { return "m." + prefix + "." + identifiablePediaEntry.identifiableType.localizationSuffix + ".page." + pageNumber.ToString(); }

                if (!isHowToUse)
                    LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("desc"), pediaText);
                else if (isHowToUse)
                    LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("how_to_use"), pediaText);
            }

            public static void AddTutorialPage(string pediaEntryName, int pageNumber, string pediaText = "Placeholder Text. (Please set)")
            {
                FixedPediaEntry fixedPediaEntry = Get<FixedPediaEntry>(pediaEntryName);

                string CreatePageKey(string prefix)
                { return "m." + prefix + "." + fixedPediaEntry.textId + ".page." + pageNumber.ToString(); }

                LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("instructions"), pediaText);
            }

            public static PediaEntry AddIdentifiablePedia(IdentifiableType identifiableType, string pediaCategory, string pediaEntryName, string pediaIntro, string pediaDescription, string pediaHowToUse, bool useHighlightedTemplate = false, bool unlockedInitially = false)
            {
                if (Get<IdentifiablePediaEntry>(pediaEntryName))
                    return null;

                PediaEntryCategory pediaEntryCategory = SRSingleton<SceneContext>.Instance.PediaDirector.entryCategories.items.ToArray().First(x => x.name == pediaCategory);
                PediaEntryCategory basePediaEntryCategory = SRSingleton<SceneContext>.Instance.PediaDirector.entryCategories.items.ToArray().First(x => x.name == "Resources");
                PediaEntry pediaEntry = basePediaEntryCategory.items.ToArray().First();
                IdentifiablePediaEntry identifiablePediaEntry = ScriptableObject.CreateInstance<IdentifiablePediaEntry>();

                string CreateKey(string prefix)
                { return "m." + prefix + "." + identifiableType.localizationSuffix; }

                string CreatePageKey(string prefix)
                { return "m." + prefix + "." + identifiableType.localizationSuffix + ".page." + 1.ToString(); }

                LocalizedString intro = LocalizationDirectorLoadTablePatch.AddTranslation("Pedia", CreateKey("intro"), pediaIntro);
                LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("desc"), pediaDescription);
                LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("how_to_use"), pediaHowToUse);

                identifiablePediaEntry.hideFlags |= HideFlags.HideAndDontSave;
                identifiablePediaEntry.name = pediaEntryName;
                identifiablePediaEntry.identifiableType = identifiableType;
                if (!useHighlightedTemplate)
                    identifiablePediaEntry.template = pediaEntry.template;
                else
                    identifiablePediaEntry.template = UnityEngine.Object.Instantiate(Get<PediaTemplate>("HighlightedResourcePediaTemplate"));
                identifiablePediaEntry.title = identifiableType.localizedName;
                identifiablePediaEntry.description = intro;
                identifiablePediaEntry.isUnlockedInitially = unlockedInitially;
                identifiablePediaEntry.actionButtonLabel = pediaEntry.actionButtonLabel;
                identifiablePediaEntry.infoButtonLabel = pediaEntry.infoButtonLabel;

                if (!pediaEntryCategory.items.Contains(identifiablePediaEntry))
                    pediaEntryCategory.items.Add(identifiablePediaEntry);
                if (!addedPedias.Contains(identifiablePediaEntry))
                    addedPedias.Add(identifiablePediaEntry);

                return identifiablePediaEntry;
            }

            public static PediaEntry AddTutorialPedia(string pediaEntryName, Sprite pediaIcon, string pediaTitle, string pediaDescription, string pediaInstructions, bool unlockedInitially = true)
            {
                if (Get<FixedPediaEntry>(pediaEntryName))
                    return null;

                PediaEntryCategory pediaEntryCategory = SRSingleton<SceneContext>.Instance.PediaDirector.entryCategories.items.ToArray().First(x => x.name == "Tutorials");
                PediaEntryCategory basePediaEntryCategory = SRSingleton<SceneContext>.Instance.PediaDirector.entryCategories.items.ToArray().First(x => x.name == "Tutorials");
                PediaEntry pediaEntry = basePediaEntryCategory.items.ToArray().First();
                FixedPediaEntry tutorialPediaEntry = ScriptableObject.CreateInstance<FixedPediaEntry>();

                string CreateKey(string prefix)
                { return "m." + prefix + "." + pediaEntryName.ToLower().Replace(" ", "_"); }

                string CreatePageKey(string prefix)
                { return "m." + prefix + "." + pediaEntryName.ToLower().Replace(" ", "_") + ".page." + 1.ToString(); }

                LocalizedString title = LocalizationDirectorLoadTablePatch.AddTranslation("Pedia", "m." + pediaEntryName.ToLower().Replace(" ", "_"), pediaTitle);
                LocalizedString desc = LocalizationDirectorLoadTablePatch.AddTranslation("Pedia", CreateKey("desc"), pediaDescription);
                LocalizationDirectorLoadTablePatch.AddTranslation("PediaPage", CreatePageKey("instructions"), pediaInstructions);

                tutorialPediaEntry.hideFlags |= HideFlags.HideAndDontSave;
                tutorialPediaEntry.name = pediaEntryName;
                tutorialPediaEntry.template = pediaEntry.template;
                tutorialPediaEntry.title = title;
                tutorialPediaEntry.description = desc;
                tutorialPediaEntry.icon = pediaIcon;
                tutorialPediaEntry.textId = pediaEntryName.ToLower().Replace(" ", "_");
                tutorialPediaEntry.isUnlockedInitially = unlockedInitially;
                tutorialPediaEntry.actionButtonLabel = pediaEntry.actionButtonLabel;
                tutorialPediaEntry.infoButtonLabel = pediaEntry.infoButtonLabel;

                if (!pediaEntryCategory.items.Contains(tutorialPediaEntry))
                    pediaEntryCategory.items.Add(tutorialPediaEntry);
                if (!addedPedias.Contains(tutorialPediaEntry))
                    addedPedias.Add(tutorialPediaEntry);

                return tutorialPediaEntry;
            }
        }

        [HarmonyPatch(typeof(LocalizationDirector), "LoadTables")]
        internal static class LocalizationDirectorLoadTablePatch
        {
            public static void Postfix(LocalizationDirector __instance) => MelonCoroutines.Start(LoadTable(__instance));

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
                    dictionary = new System.Collections.Generic.Dictionary<string, string>();
                    addedTranslations.Add(table, dictionary);
                }
                dictionary.TryAdd(key, localized);
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
                    if (!slimeDefinition.Diet.MajorFoodIdentifiableTypeGroups.Contains(Get<IdentifiableTypeGroup>("MeatGroup")))
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