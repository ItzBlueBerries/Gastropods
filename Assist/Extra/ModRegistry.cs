using Gastropods.Components;
using Gastropods.Components.Attackers;
using Gastropods.Components.Behaviours;
using Gastropods.Components.FedVaccables;
// using Gastropods.Components.Popups;
using Gastropods.Components.ReproduceOnNearbys;
// using Gastropods.Components.Resource;
using Gastropods.Data.Gastropods;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppMonomiPark.SlimeRancher.Damage;
using UnityEngine;
using static Gastropods.HarmonyPatches;

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
            ClassInjector.RegisterTypeInIl2Cpp<ConfidantAttack>();
            ClassInjector.RegisterTypeInIl2Cpp<GastropodType>();*/

            ClassInjector.RegisterTypeInIl2Cpp<GastroBehaviour>();
            ClassInjector.RegisterTypeInIl2Cpp<EatenBehaviour>();

            ClassInjector.RegisterTypeInIl2Cpp<HarpoonAttacker>();
            // ClassInjector.RegisterTypeInIl2Cpp<HarpoonAttackerV2>();
            ClassInjector.RegisterTypeInIl2Cpp<HungryAttacker>();
            ClassInjector.RegisterTypeInIl2Cpp<SpineAttacker>();

            // ClassInjector.RegisterTypeInIl2Cpp<JuicyProduction>();

            ClassInjector.RegisterTypeInIl2Cpp<GotoSuperior>();
            ClassInjector.RegisterTypeInIl2Cpp<FedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<ReproduceOnNearby>();
            ClassInjector.RegisterTypeInIl2Cpp<LazyVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<KillSlimeOnTouch>();
            ClassInjector.RegisterTypeInIl2Cpp<ObjectRotation>();
            ClassInjector.RegisterTypeInIl2Cpp<UnidentifiedTransform>();
            ClassInjector.RegisterTypeInIl2Cpp<RandomRigidMovement>();
            ClassInjector.RegisterTypeInIl2Cpp<DestroyAfterHours>();
            ClassInjector.RegisterTypeInIl2Cpp<ObjectTwirl>();
            ClassInjector.RegisterTypeInIl2Cpp<BounceActorOnCollision>();
            //ClassInjector.RegisterTypeInIl2Cpp<PopupBase>();
            //ClassInjector.RegisterTypeInIl2Cpp<VaccedPopup>();
            ClassInjector.RegisterTypeInIl2Cpp<Components.DamagePlayerOnTouch>();
            ClassInjector.RegisterTypeInIl2Cpp<DamageSlimeOnTouch>();
            ClassInjector.RegisterTypeInIl2Cpp<AlwaysBeHoppingAround>();
            ClassInjector.RegisterTypeInIl2Cpp<TraditionalReform>();
            ClassInjector.RegisterTypeInIl2Cpp<DreamySuperiorCreation>();
            ClassInjector.RegisterTypeInIl2Cpp<DreamyGSearcher>();
            ClassInjector.RegisterTypeInIl2Cpp<NoMoreCloudsAtDaylight>();

            ClassInjector.RegisterTypeInIl2Cpp<BrineFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<SunlightFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<PrimalFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<PowderFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<HareFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<BubblyFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<TraditionalFedVaccable>();
            ClassInjector.RegisterTypeInIl2Cpp<DreamyFedVaccable>();

            ClassInjector.RegisterTypeInIl2Cpp<BrineReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<SunlightReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<PrimalReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<PowderReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<HareReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<BubblyReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<TraditionalReproduce>();
            ClassInjector.RegisterTypeInIl2Cpp<DreamyReproduce>();

            //ClassInjector.RegisterTypeInIl2Cpp<BrineVaccedPopup>();
            //ClassInjector.RegisterTypeInIl2Cpp<SunlightVaccedPopup>();
            //ClassInjector.RegisterTypeInIl2Cpp<PrimalVaccedPopup>();
            //ClassInjector.RegisterTypeInIl2Cpp<PowderVaccedPopup>();
            //ClassInjector.RegisterTypeInIl2Cpp<HareVaccedPopup>();
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
            Prickly.Initialize();
            Hare.Initialize();
            Bubbly.Initialize();
            Traditional.Initialize();
            Dreamy.Initialize();
            Crowned.Initialize();
        }

        public static void LoadInstances()
        {
            DamageSourceDefinition gastropodDamageSource = ScriptableObject.CreateInstance<DamageSourceDefinition>();
            gastropodDamageSource.name = "GastropodDamageSource";
            gastropodDamageSource.logMessage = "GastropodDamageSource.Damage";
        }

        public static void LoadGastros(string sceneName)
        {
            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (Gastro.QUEEN_GASTROPODS.Contains(gastropod) || Gastro.KING_GASTROPODS.Contains(gastropod))
                    continue;
                if (Gastro.NON_EATABLE_GASTROPODS.Contains(gastropod))
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
            Prickly.Load(sceneName);
            Hare.Load(sceneName);
            Bubbly.Load(sceneName);
            Traditional.Load(sceneName);
            Dreamy.Load(sceneName);
            Crowned.Load(sceneName);
        }

        public static void LoadSpawners(string sceneName)
        {
            #region KINGS_AND_QUEENS
            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("BrineQueenGastropod"), Get<IdentifiableType>("BrineKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));
            ModSpawner.Other.AddToPinkCanyon(sceneName, new IdentifiableType[] { Get<IdentifiableType>("BrineQueenGastropod"), Get<IdentifiableType>("BrineKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("SunlightQueenGastropod"), Get<IdentifiableType>("SunlightKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("PrimalQueenGastropod"), Get<IdentifiableType>("PrimalKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("PowderQueenGastropod"), Get<IdentifiableType>("PowderKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("HareQueenGastropod"), Get<IdentifiableType>("HareKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));
            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("HareQueenGastropod"), Get<IdentifiableType>("HareKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));
            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("HareQueenGastropod"), Get<IdentifiableType>("HareKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));
            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("HareQueenGastropod"), Get<IdentifiableType>("HareKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("BubblyQueenGastropod"), Get<IdentifiableType>("BubblyKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));
            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("BubblyQueenGastropod"), Get<IdentifiableType>("BubblyKingGastropod") }, UnityEngine.Random.Range(0.003f, 0.03f));

            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("TraditionalQueenGastropod"), Get<IdentifiableType>("TraditionalKingGastropod") }, 0.0025f);
            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("TraditionalQueenGastropod"), Get<IdentifiableType>("TraditionalKingGastropod") }, 0.0025f);
            #endregion

            #region WITHOUT_KINGS_AND_QUEENS
            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("ToxinGastropod") }, UnityEngine.Random.Range(0.008f, 0.08f));
            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("ToxinGastropod") }, UnityEngine.Random.Range(0.005f, 0.05f));
            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("ToxinGastropod") }, UnityEngine.Random.Range(0.008f, 0.08f));

            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("UnidentifiedGastropod") }, 0.0025f);
            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("UnidentifiedGastropod") }, 0.0025f);
            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("UnidentifiedGastropod") }, 0.0025f);
            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("UnidentifiedGastropod") }, 0.0025f);

            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("CrepeGastropod") }, UnityEngine.Random.Range(0.008f, 0.08f));

            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("PricklyGastropod") }, UnityEngine.Random.Range(0.008f, 0.08f));
            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("PricklyGastropod") }, UnityEngine.Random.Range(0.005f, 0.05f));
            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("PricklyGastropod") }, UnityEngine.Random.Range(0.005f, 0.05f));
            #endregion

            #region SUPREMES_BECAUSE_YES
            ModSpawner.AddToFields(sceneName, new IdentifiableType[] { Get<IdentifiableType>("CrownedGastropod") }, 0.0005f);
            ModSpawner.AddToStrand(sceneName, new IdentifiableType[] { Get<IdentifiableType>("CrownedGastropod") }, 0.0005f);
            ModSpawner.AddToGorge(sceneName, new IdentifiableType[] { Get<IdentifiableType>("CrownedGastropod") }, 0.0005f);
            ModSpawner.AddToBluffs(sceneName, new IdentifiableType[] { Get<IdentifiableType>("CrownedGastropod") }, 0.0005f);
            #endregion

            #region DREAMY_SPAWNS_BECAUSE_TOO_LAZY_TO_MAKE_METHODS
            SlimeSet.Member[] members = new SlimeSet.Member[]
            {
                new SlimeSet.Member()
                {
                    identType = Get<IdentifiableType>("DreamyGastropod"),
                    prefab = Get<IdentifiableType>("DreamyGastropod").prefab,
                    weight = 0.004f
                }
            };

            switch (sceneName.Contains("zoneFields") || sceneName.Contains("zoneStrand") || sceneName.Contains("zoneGorge") || sceneName.Contains("zoneBluffs"))
            {
                case true:
                    {
                        IEnumerable<DirectedSlimeSpawner> source = UnityEngine.Object.FindObjectsOfType<DirectedSlimeSpawner>();
                        foreach (DirectedSlimeSpawner directedSlimeSpawner in source)
                        {
                            foreach (DirectedActorSpawner.SpawnConstraint spawnConstraint in directedSlimeSpawner.constraints)
                            {
                                if (spawnConstraint.window.timeMode == DirectedActorSpawner.TimeMode.NIGHT)
                                {
                                    foreach (SlimeSet.Member member in members)
                                    {
                                        if (spawnConstraint.slimeset.members.Contains(member))
                                            continue;

                                        spawnConstraint.slimeset.members = spawnConstraint.slimeset.members.AddItem(member).ToArray();
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
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
            #region TUTORIAL_PEDIAS
            #region STUBBORN_TUTORIAL
            PediaEntry stubbornTutorial = PatchPediaDirector.AddTutorialPedia("TutGastropodsStubborn", CreateSprite(LoadImage("Files.Icons.Tutorials.iconTutorialStubbornGastropod")),
                "Stubborn Gastropods",
                "Can't figure out how to get this gastropod to cooperate? Read this!",
                "Naturally, normal gastropods are not stubborn (though some may be defensive). On the other hand for superiors like a Queen or King, they have other needs.\n" +
                "First, lets start off with the queen. Queens do not move whatsoever, you <b><i>cannot</b></i> make them move. The only way to make them move is to feed them.\n" +
                "How would you know what they eat? Well I'm sure you'll find out on your own by testing with different foods and how the gastropod reacts. Once fed, they will let you hold them onto your VacPack. " +
                "Although there is a catch, they will only let you hold them for 3 hours straight. When the time is hit, if you're still holding them.. they'll launch themselves off. " +
                "This could cause trouble as you could accidentally launch them off a cliff.. yikes. So be careful and maybe watch the time if you can!"
            );
            PatchPediaDirector.AddTutorialPage("TutGastropodsStubborn", 2,
                "Now for the king gastropods. They're pretty laid back and don't care if you pick them up. " +
                "Although there is of course a catch, every few hours they go into a \"lazy\" mode. This stops them from moving entirely and you cannot get them to move whatsoever.\n" +
                "You can't even feed them to move. It stops within the next few hours, if they go into lazy mode while attached to your VacPack.. they will launch themselves off like a queen would, yikes.\n\n" +
                "Overall though, that is everything you need to know for handling stubborn gastropods! Handle them well."
            );
            Gastro.Pedia.TUTORIALS.Add(stubbornTutorial);
            #endregion
            #region REPRODUCE_TUTORIAL
            PediaEntry reproduceTutorial = PatchPediaDirector.AddTutorialPedia("TutGastropodsReproduce", CreateSprite(LoadImage("Files.Icons.Tutorials.iconTutorialReproduceGastropod")),
                "Reproducing Gastropods", 
                "Learn the entire background of gastropod reproduction!", 
                "<b>Copied from previous gastropod pedia entries:</b>\n\n" +
                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!\n\n" +
                "Here is how the tutorial will start off! If you would like to know more besides this, please continue to the next page."
            );
            PatchPediaDirector.AddTutorialPage("TutGastropodsReproduce", 2,
                "For a more detailed background on reproduction. The interval of spawning a newly produced gastropod is randomized, this may not be randomized each spawn.\n" +
                "The gastropod will spawn near the queen, this could cause issues if your queen is not positioned in a good position to keep them all in a closed space. <i>(ex: Corral, Coop)</i>\n" +
                "By issues, these issues are spawning outside of the closed area. Besides that, <b>reproduction cannot be done if the king is not near the queen.</b>" +
                "While they may have found each other, they are required to be near each other as well. Not only that but there are some things that newly produced gastropods will do once spawned.\n" +
                "Some may follow either the queen or king (or nobody). Here are the listed chances:\n\n" +
                "<b>Queen : 50% Chance\nKing : 30% Chance\nNobody : 20% Chance</b>"
            );
            PatchPediaDirector.AddTutorialPage("TutGastropodsReproduce", 3,
                "Overall, reproduction is a simple process but requires waiting. Here are some bonus facts:\n\n" +
                "<b>1. One - Two gastropods can be spawned per reproduction. (Primarily)\n\n" +
                "2. There is always the possibility that a gastropod will follow a superior that did not reproduce them specificially.\n\n" +
                "3. Gastropods do not decay (alive), so there is no worries of them disappearing from not being handled although they don't move much.</b>\n\n" +
                "After this you should know a good amount about reproducing gastropods with their corresponding type!"
            );
            Gastro.Pedia.TUTORIALS.Add(reproduceTutorial);
            #endregion
            #region DEFENSIVE_ATTACKERS_TUTORIAL
            PediaEntry defensiveAttackersTutorial = PatchPediaDirector.AddTutorialPedia("TutDefensiveAttackers", CreateSprite(LoadImage("Files.Icons.Tutorials.iconTutorialDefensivePlusAttackers")),
                "Defensive + Attackers",
                "Some information on defensive gastropods & type of attackers.",
                "It has come to this. You figure out what kind of attacks gastropods may perform.\n" +
                "While some gastropods may be friendly and do not attack others whatsoever, some will. " +
                "For instance, the Toxin Gastropod may seem cute but it is not afraid to launch its harpoon at a nearby slime which will instantly.. get rid of them.\n" +
                "In this tutorial, the type of attackers will be explained. If you would like to keep these gastropods, you may need to risk a few slimes or look for a subsitute if there is one."
            );
            PatchPediaDirector.AddTutorialPage("TutDefensiveAttackers", 2,
                "<b>    Attacker : Harpoon   </b>\n" +
                "The harpoon attacker is when a gastropod launches a harpoon out of its shell and towards a nearby slime. " +
                "When this occurs, there is a low chance of the slime surviving as its shot directly towards them. This will cause them to go \"bye bye\". " +
                "A harpoon attacker will have 3 harpoons, most of the time. Once its out of harpoons, it will take a few hours to reload and they also take a bit of time to shoot their harpoon towards the enemy.\n" +
                "There are currently no subsitutes for this attack to not sacrifice slimes as harpoon attackers do not shoot anything else but slimes to prevent them from being eaten."
            );
            PatchPediaDirector.AddTutorialPage("TutDefensiveAttackers", 3,
                "<b>    Attacker : Hungry   </b>\n" +
                "The hungry attacker is when a gastropod will eat almost anything that touches it. Primarily meat and slimes. " +
                "When this occurs, there is a low chance of the slime surviving as because when they collide with the gastropod.. they will be gone just like that. " +
                "Eventually, the hungry attacker will get full and will need to take a few hours to get hungry again. " +
                "Hungry attackers will also attempt to find food around them every few hours <b>(1 - 3)</b>, this is for meat and slimes specifically. When they find something, they will follow it until its gone.\n" +
                "There is currently a subsitute for this to not sacrifice slimes as hungry attackers also eat meat <b>(excluding gastropods)</b> and could eat anything from that diet till they get full."
            );
            PatchPediaDirector.AddTutorialPage("TutDefensiveAttackers", 4,
                "<b>    Attacker : Spine   </b>\n" +
                "The spine attacker is when a gastropod launches a spine out of its shell and towards a nearby slime <b>or player</b>. " +
                "When this occurs, there is a chance of the slime / player surviving, more likely the player. The spine deals damage, small damage but it can become big quickly eventually killing them off. " +
                "A spine attackers usually have around 30+ spines stored. Just like harpoon attackers, once they're out they need to reload and it'll take a bit of time, say a bit longer than harpoon reloads. It'll also take a second between shooting spines, like harpoons.\n" +
                "There are currently no subsitutes for this attack to not sacrifice slimes / players as spine attackers do not shoot anything else but slimes / players to prevent them from being eaten. What could potentially work though is either taking the damage for yourself or the slimes eat them quickly."
            );
            Gastro.Pedia.TUTORIALS.Add(defensiveAttackersTutorial);
            #endregion
            #endregion

            Brine.CreatePedia();
            Sunlight.CreatePedia();
            Toxin.CreatePedia();
            Primal.CreatePedia();
            Powder.CreatePedia();
            Unidentified.CreatePedia();
            Crepe.CreatePedia();
            Prickly.CreatePedia();
            Hare.CreatePedia();
            Bubbly.CreatePedia();
            Traditional.CreatePedia();
            Dreamy.CreatePedia();
            Crowned.CreatePedia();

            //#region SUPERIOR_GASTROPOD_PEDIAS
            //foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            //{
            //    if (Gastro.IsQueenGastropod(gastropod) || Gastro.IsKingGastropod(gastropod))
            //    {
            //        IdentifiablePediaEntry identifiablePediaEntry = PatchPediaDirector.CreateIdentifiableEntry(gastropod, gastropod.name.Replace("Gastropod", ""), Get<PediaTemplate>("HighlightedResourcePediaTemplate"),
            //            gastropod.localizedName, LocalizationDirectorLoadTablePatch.AddTranslation("Pedia", "m.intro." + gastropod.localizationSuffix, "A superior gastropod."),
            //            Get<IdentifiablePediaEntry>("Pink").actionButtonLabel, Get<IdentifiablePediaEntry>("Pink").infoButtonLabel, true);
            //        PatchPediaDirector.addedPedias.Add(identifiablePediaEntry);
            //    }
            //}
            //#endregion

            /*HarmonyPatches.PatchPediaDirector.AddTutorialPedia("HowToDotDotDot", Get<Sprite>("iconSlimeAngler"), "How To ...", "Read the instructions.", "<b>Say ... in discord chat. :]</b>");
            HarmonyPatches.PatchPediaDirector.AddTutorialPage("HowToDotDotDot", 2, "<b>Then say . . .\r\n. . . . . .\r\n. . . . . . . . .\r\n. . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . . . . . . . \r\n. . . . . . . . . . . . . . . . . .\r\n. . . . . . . . . . . . . . . \r\n. . . . . . . . . . . .\r\n. . . . . . . . .\r\n. . . . . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . .\r\n. . . . . .\r\n. . . . . .</b>");*/
        }
    }
}
