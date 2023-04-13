using Gastropods.Components;
using Gastropods.Components.Attackers;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Gastropods.Data.Gastropods;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;

namespace Gastropods.Assist
{
    internal class ModRegistry
    {
        public static void InjectTypes()
        {
            /*ClassInjector.RegisterTypeInIl2Cpp<GastropodRandomMove>();
            ClassInjector.RegisterTypeInIl2Cpp<GastropodRandomMoveV2>();
            ClassInjector.RegisterTypeInIl2Cpp<GastropodRandomMoveV3>();
            ClassInjector.RegisterTypeInIl2Cpp<CompanionController>();
            ClassInjector.RegisterTypeInIl2Cpp<ConfidantController>();
            ClassInjector.RegisterTypeInIl2Cpp<ConfidantAttack>();*/

            ClassInjector.RegisterTypeInIl2Cpp<GotoSuperior>();
            ClassInjector.RegisterTypeInIl2Cpp<FedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<ReproduceOnNearby>();
            ClassInjector.RegisterTypeInIl2Cpp<LazyVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<HarpoonAttacker>();
            ClassInjector.RegisterTypeInIl2Cpp<HarpoonKillOnTouch>();
            ClassInjector.RegisterTypeInIl2Cpp<HungryAttacker>();
            ClassInjector.RegisterTypeInIl2Cpp<ObjectRotation>();
            ClassInjector.RegisterTypeInIl2Cpp<UnidentifiedTransform>();
            ClassInjector.RegisterTypeInIl2Cpp<RandomRigidMovement>();
            ClassInjector.RegisterTypeInIl2Cpp<DestroyAfterHours>();
            ClassInjector.RegisterTypeInIl2Cpp<ObjectTwirl>();
            ClassInjector.RegisterTypeInIl2Cpp<BounceActorOnCollision>();

            ClassInjector.RegisterTypeInIl2Cpp<BrineFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<SunlightFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<PrimalFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<PowderFedVaccable>();

            ClassInjector.RegisterTypeInIl2Cpp<BrineReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<SunlightReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<PrimalReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<PowderReproduce>();
        }

        public static void InitializeGastros()
        {
            Brine.Initialize();
            Sunlight.Initialize();
            Toxin.Initialize();
            Primal.Initialize();
            Powder.Initialize();
            Unidentified.Initialize();
            Crepe.Initialize();
        }

        public static void LoadGastros(string sceneName)
        {
            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (Gastro.QUEEN_GASTROPODS.Contains(gastropod) || Gastro.KING_GASTROPODS.Contains(gastropod))
                    continue;
                // Get<SlimeDefinition>("Angler").Diet.AdditionalFoodIdents = Get<SlimeDefinition>("Angler").Diet.AdditionalFoodIdents.ToArray().AddToArray(gastropod);
                Get<SlimeDefinition>("Angler").Diet.FavoriteIdents = Get<SlimeDefinition>("Angler").Diet.FavoriteIdents.ToArray().AddToArray(gastropod);
            }

            Brine.Load(sceneName);
            Sunlight.Load(sceneName);
            Toxin.Load(sceneName);
            Primal.Load(sceneName);
            Powder.Load(sceneName);
            Unidentified.Load(sceneName);
            Crepe.Load(sceneName);
        }

        public static void LoadSpawners(string sceneName)
        {
            #region KINGS_AND_QUEENS
            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("BrineQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("BrineKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.Other.AddToPinkCanyon(sceneName, Get<IdentifiableType>("BrineQueenGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));
            ModSpawner.Other.AddToPinkCanyon(sceneName, Get<IdentifiableType>("BrineKingGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));

            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("SunlightQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("SunlightKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));

            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("PrimalQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("PrimalKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));

            ModSpawner.AddToBluffs(sceneName, Get<IdentifiableType>("PowderQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToBluffs(sceneName, Get<IdentifiableType>("PowderKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            #endregion

            #region WITHOUT_KINGS_AND_QUEENS
            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.08f, 0.09f));
            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));
            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.08f, 0.09f));

            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("UnidentifiedGastropod"), 0.0025f);
            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("UnidentifiedGastropod"), 0.0025f);
            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("UnidentifiedGastropod"), 0.0025f);
            ModSpawner.AddToBluffs(sceneName, Get<IdentifiableType>("UnidentifiedGastropod"), 0.0025f);

            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("CrepeGastropod"), UnityEngine.Random.Range(0.08f, 0.09f));
            #endregion
        }

        public static void LoadFears()
        {
            foreach (IdentifiableType defensiveGastropod in Gastro.DEFENSIVE_GASTROPODS)
            {
                ModFears.AddToAllProfiles(defensiveGastropod, 15, 9);
                ModFears.RemoveFromProfile(defensiveGastropod, Get<GameObject>("slimeAngler"));
            }
        }

        public static void LoadPedias()
        {
            // BRINE
            GastroUtility.CreatePediaEntry(Get<IdentifiableType>("BrineGastropod"), "Brine", "Based on the sea?", "This is a test pedia for now.", "Reproduce with king & queen.");
            HarmonyPatches.PatchPediaDirector.AddIdentifiablePage("Brine", 2);
            HarmonyPatches.PatchPediaDirector.AddIdentifiablePage("Brine", 2, isHowToUse: true);

            HarmonyPatches.PatchPediaDirector.AddTutorialPedia("HowToDotDotDot", Get<Sprite>("iconSlimeAngler"), "How To ...", "Read the instructions.", "<b>Say ... in discord chat. :]</b>");
            HarmonyPatches.PatchPediaDirector.AddTutorialPage("HowToDotDotDot", 2, "<b>Then say . . .\r\n. . . . . .\r\n. . . . . . . . .\r\n. . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . \r\n. . . . . . . . . . . .\r\n. . . . . . . . .\r\n. . . . . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . . . . .\r\n. . . . . .</b>");
        }
    }
}
