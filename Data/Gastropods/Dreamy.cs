using Gastropods.Assist;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Gastropods.Components;
using Il2Cpp;
using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;
using Gastropods.Components.Behaviours;

namespace Gastropods.Data.Gastropods
{
    internal class Dreamy
    {
        private static IdentifiableType dreamyGastropod;
        private static IdentifiableType dreamyQueenGastropod;
        private static IdentifiableType dreamyKingGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#00076f"), LoadHex("#44008b"), LoadHex("#9f45b0") };
        private static Color[] gastroShellPalette = new Color[] { LoadHex("#9f45b0"), LoadHex("#44008b"), LoadHex("#00076f") };
        private static Color[] gastroDiffPalette = new Color[] { LoadHex("#00076f"), LoadHex("#44008b"), LoadHex("#44008b") };
        private static Color[] gastroDiffShellPalette = new Color[] { LoadHex("#44008b"), LoadHex("#44008b"), LoadHex("#00076f") };

        public static void Initialize()
        {
            dreamyGastropod = GastroUtility.CreateIdentifiable("Dreamy", false, false, true, false, false, LoadHex("#44008b"));
            dreamyQueenGastropod = GastroUtility.CreateIdentifiable("Dreamy", true, false, false, false, false, LoadHex("#44008b"));
            dreamyKingGastropod = GastroUtility.CreateIdentifiable("Dreamy", false, true, false, false, false, LoadHex("#00076f"));

            Gastro.DO_SOMETHING_GASTROPODS.Add(dreamyGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(dreamyQueenGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(dreamyKingGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Material dreamyMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                        dreamyMaterial.SetSlimeColor(gastroDiffPalette[0], gastroDiffPalette[1], gastroDiffPalette[2]);

                        Material diffDreamyMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                        diffDreamyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

                        // lol looks better with a switcheroo

                        Material pinkEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                        pinkEyesMaterial.SetSlimeColor(LoadHex("#FFC0CB"), LoadHex("#FFC0CB"), LoadHex("#FFC0CB"));

                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { dreamyGastropod, dreamyQueenGastropod, dreamyKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("CraftGroup"), Get<IdentifiableTypeGroup>("FruitGroup") }
                        );

                        // too lazy to do fields and such
                        GameObject dreamyPrefab = GastroUtility.CreateGastropod("Dreamy", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodDreamy")), dreamyGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("dreamy_shell").sharedMesh, null, CreateAccessories(false)).Item1;
                        dreamyPrefab.AddComponents(Il2CppType.Of<DreamySuperiorCreation>(), Il2CppType.Of<DreamyGSearcher>(), Il2CppType.Of<NoMoreCloudsAtDaylight>());
                        dreamyPrefab.transform.Find("GastroParts/GastropodBody/GastropodShell").GetComponent<MeshRenderer>().material = dreamyMaterial;
                        dreamyPrefab.transform.Find("GastroParts/GastropodBody/GastropodEyes").GetComponent<MeshRenderer>().material = pinkEyesMaterial;

                        GameObject dreamyQueenPrefab = GastroUtility.CreateQueenGastropod("Dreamy", false, true, false, null, dreamyQueenGastropod, Il2CppType.Of<DreamyFedVaccable>(), Il2CppType.Of<DreamyReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("dreamy_queen_shell").sharedMesh, null, CreateAccessories(true)).Item1;
                        dreamyQueenPrefab.transform.Find("GastroParts/GastropodBody/GastropodShell").GetComponent<MeshRenderer>().material = dreamyMaterial;
                        dreamyQueenPrefab.transform.Find("GastroParts/GastropodBody/GastropodEyes").GetComponent<MeshRenderer>().material = pinkEyesMaterial;

                        GameObject dreamyKingPrefab = GastroUtility.CreateKingGastropod("Dreamy", false, true, false, null, dreamyKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("dreamy_queen_shell").sharedMesh, null, CreateAccessories(true)).Item1;
                        dreamyKingPrefab.transform.Find("GastroParts/GastropodBody/GastropodShell").GetComponent<MeshRenderer>().material = diffDreamyMaterial;
                        dreamyKingPrefab.transform.Find("GastroParts/GastropodBody").GetComponent<MeshRenderer>().material = diffDreamyMaterial;
                        dreamyKingPrefab.transform.Find("GastroParts/GastropodBody/GastropodEyes").GetComponent<MeshRenderer>().material = pinkEyesMaterial;
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(dreamyGastropod, "Dreamy",
                "Do they create the good or the bad dreams?",

                "Dreamy Gastropods are one of the <b>twelfth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "There is a lot to this specific gastropod, the power is bestows and it's unique appearance. " +
                "When it's day, these guys aren't very active but when its night they roam freely and you could find them. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 2,
                "<b>    Origin   </b>\n" +
                "Only Supreme's get this kind of descriptive addition to their pedia entries, which will be explained here.\n" +
                "Dreamy may have took a while to arrive on Rainbow Island but they were had some of the best abilities. " +
                "Technically, Dreamy can be found on its own as <b>their superiors do not actually exist</b>. How're they reproduced then? " +
                "Dreamy has the power to <b>create their own superiors</b>, though without the best visual qualities. At randomized times (long intervals) they'll create <b>one of them</b> in order to feel company but also give them the power of superiors.\n" +
                "Now that there is information on them being powerful and what they do for the ability of reproduction with their typing, what about the Supreme part? " +
                "Well Dreamy was technically going to be a supreme member but has denied of this. As an almost and worthy of being a supreme member, this is added to the entry."
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 3,
                "Another part of the Dreamy Gastropod is their confidant. Dreamy has a confidant who is a Star.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "Dreamy is also a powerful one, defensive of course and will not allow slimes to touch them as long as their clouds are visible during the night.\n" +
                "Another known fact of the Dreamy Gastropod is that they love wondering around. They'll search for nearby gastropods for fun and is more active than other gastropods. " +
                "Once daylight hits, they'll stop and possibly return to their superiors. Overall, Dreamy is a handful."
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 4,
                "This is a type of gastropod that'll somewhat have kings & queens.\n" +
                "They do spawn on their own and could be reproduced by a superior. Superiors do not spawn on their own.\n" +
                "This gastropod type specifically spawns everywhere at around nighttime."
            );
        }


        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material dreamyMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            dreamyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material starMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            starMaterial.SetSlimeColor(Color.yellow, Color.yellow, Color.yellow);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject dreamyClouds;
                GameObject dreamyCloudsStars;
                GameObject dreamyStar;
                GameObject dreamyStarEyes;

                dreamyClouds = new GameObject("DreamyClouds");
                dreamyClouds.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_clouds").sharedMesh;
                dreamyClouds.AddComponent<MeshRenderer>().sharedMaterial = dreamyMaterial;
                dreamyClouds.AddComponent<ObjectRotation>();
                dreamyClouds.AddComponent<BounceActorOnCollision>();

                dreamyCloudsStars = new GameObject("DreamyCloudsStars");
                dreamyCloudsStars.transform.parent = dreamyClouds.transform;
                dreamyCloudsStars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_clouds_stars").sharedMesh;
                dreamyCloudsStars.AddComponent<MeshRenderer>().sharedMaterial = starMaterial;

                dreamyStar = new GameObject("DreamyStar");
                dreamyStar.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_star").sharedMesh;
                dreamyStar.AddComponent<MeshRenderer>().sharedMaterial = starMaterial;

                dreamyStarEyes = new GameObject("DreamyStarEyes");
                dreamyStarEyes.transform.parent = dreamyStar.transform;
                dreamyStarEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_star_eyes").sharedMesh;
                dreamyStarEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { dreamyClouds, dreamyStar };
            }
            else if (isSuperior)
                return new GameObject[] { };

            return new GameObject[] { };
        }
    }
}
