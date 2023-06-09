﻿global using UnityEngine;
global using static Utility;
using Gastropods;
using Gastropods.Assist;
using Gastropods.Components;
using Il2Cpp;
using MelonLoader;

[assembly: MelonInfo(typeof(GastroEntry), "Gastropods", "2.0.1", "FruitsyOG")]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]
[assembly: MelonColor(0, 0, 170, 255)]
namespace Gastropods
{
    internal class GastroEntry : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ModRegistry.InjectTypes();

            //var gastropodsGroup = ScriptableObject.CreateInstance<IdentifiableTypeGroup>();
            //gastropodsGroup.name = "GastropodsGroup";

            ModRegistry.InitializeGastros();

            //foreach (IdentifiableType identType in Gastro.GASTROPODS)
            //{
            //    gastropodsGroup.memberTypes.Add(identType);
            //}
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            switch (sceneName)
            {
                //Here you have SystemContext loaded and here you registering a translations
                case "SystemCore":
                    {
                        break;
                    }
                //Here you have loaded assets like Identifiables and GameContext
                case "GameCore":
                    {
                        ModRegistry.LoadInstances();
                        ModRegistry.LoadGastros(sceneName);
                        ModRegistry.LoadFears();

                        foreach (IdentifiableType definition in Get<IdentifiableTypeGroup>("BaseSlimeGroup").memberTypes)
                            if (definition.Cast<SlimeDefinition>() != null && definition.Cast<SlimeDefinition>().Diet != null)
                                definition.Cast<SlimeDefinition>().Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, definition.Cast<SlimeDefinition>());
                        foreach (IdentifiableType definition in Get<IdentifiableTypeGroup>("LargoGroup").memberTypes)
                            if (definition.Cast<SlimeDefinition>() != null && definition.Cast<SlimeDefinition>().Diet != null)
                                definition.Cast<SlimeDefinition>().Diet.RefreshEatMap(SRSingleton<GameContext>.Instance.SlimeDefinitions, definition.Cast<SlimeDefinition>());
                        break;
                    }
                //Here you have loaded SceneContext
                case "zoneCore":
                    {
                        break;
                    }
            }
            ModRegistry.LoadSpawners(sceneName);
        }
    }
}