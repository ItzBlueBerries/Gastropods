﻿using Gastropods.Assist.Extra;
using Gastropods.Components;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Gastropods.Data.Gastropods;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            ClassInjector.RegisterTypeInIl2Cpp<HourlyMaterialChange>();

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
        }

        public static void LoadGastros(string sceneName)
        {
            foreach (IdentifiableType gastropod in GastroEntry.GASTROPODS)
            {
                if (GastroEntry.QUEEN_GASTROPODS.Contains(gastropod) || GastroEntry.KING_GASTROPODS.Contains(gastropod))
                    continue;
                // Get<SlimeDefinition>("Angler").Diet.AdditionalFoodIdents = Get<SlimeDefinition>("Angler").Diet.AdditionalFoodIdents.ToArray().AddToArray(gastropod);
                Get<SlimeDefinition>("Angler").Diet.FavoriteIdents = Get<SlimeDefinition>("Angler").Diet.FavoriteIdents.ToArray().AddToArray(gastropod);
            }

            Brine.Load(sceneName);
            Sunlight.Load(sceneName);
            Toxin.Load(sceneName);
            Primal.Load(sceneName);
            Powder.Load(sceneName);
        }

        public static void LoadSpawners(string sceneName)
        {
            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("BrineQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("BrineKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.Other.AddToPinkCanyon(sceneName, Get<IdentifiableType>("BrineQueenGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));
            ModSpawner.Other.AddToPinkCanyon(sceneName, Get<IdentifiableType>("BrineKingGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));

            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("SunlightQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("SunlightKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));

            ModSpawner.AddToFields(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.08f, 0.09f));
            ModSpawner.AddToStrand(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.05f, 0.08f));
            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("ToxinGastropod"), UnityEngine.Random.Range(0.08f, 0.09f));

            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("PrimalQueenGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
            ModSpawner.AddToGorge(sceneName, Get<IdentifiableType>("PrimalKingGastropod"), UnityEngine.Random.Range(0.03f, 0.05f));
        }

        public static void LoadFears()
        {
            foreach (IdentifiableType defensiveGastropod in GastroEntry.DEFENSIVE_GASTROPODS)
                ModFears.AddToAllProfiles(defensiveGastropod, 15, 9);
            ModFears.RemoveFromProfile(Get<IdentifiableType>("ToxinGastropod"), Get<GameObject>("slimeAngler"));
        }
    }
}
