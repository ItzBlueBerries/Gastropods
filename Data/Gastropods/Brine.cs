using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.FedVaccables;
// using Gastropods.Components.Popups;
using Gastropods.Components.ReproduceOnNearbys;
using Il2Cpp;
using Il2CppInterop.Runtime;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;

namespace Gastropods.Data.Gastropods
{
    internal class Brine
    {
        private static IdentifiableType brineGastropod;
        private static IdentifiableType brineQueenGastropod;
        private static IdentifiableType brineKingGastropod;
        private static Color[] gastroPalette = new Color[] { Color.cyan, Color.blue, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.blue, Color.cyan };
        private static Color[] gastroDiffPalette = new Color[] { Color.blue, Color.blue, Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, Color.blue, Color.blue };

        public static void Initialize()
        {
            brineGastropod = GastroUtility.CreateIdentifiable("Brine", false, false, false, false, Color.cyan);
            brineQueenGastropod = GastroUtility.CreateIdentifiable("Brine", true, false, false, false, Color.cyan);
            brineKingGastropod = GastroUtility.CreateIdentifiable("Brine", false, true, false, false, Color.blue);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Brine", true, false, false, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodBrine")), brineGastropod, gastroPalette, gastroShellPalette, null, null, null, CreateAccessories(false));
                        GastroUtility.CreateQueenGastropod("Brine", false, false, false, null, brineQueenGastropod, Il2CppType.Of<BrineFedVaccable>(), Il2CppType.Of<BrineReproduce>(), gastroPalette, gastroShellPalette, null, null, null, CreateAccessories(true));
                        GastroUtility.CreateKingGastropod("Brine", false, false, false, null, brineKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null, null, null, CreateAccessories(true));
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(brineGastropod, "Brine",
                "Sea Stars, Fishies and Snails! All in one.",

                "Brine Gastropods are one of the <b>first gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "Being raised by their superior gastropods, they will follow them around if they so choose like almost any other gastropod. " +
                "Although being cute and adorable, they're a type of food that is given to the slimes you ranch! So I guess that's a bit of a choice there. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Brine", 2,
                "Another part of the Brine Gastropod is their confidant. Brine has a confidant who is a Fish.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround."
            );
            PatchPediaDirector.AddIdentifiablePage("Brine", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in the Rainbow Fields. Occasionally in the Pink Canyon. (Where Angler slimes spawn)"
            );
        }

        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material seaStarsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            seaStarsMaterial.SetSlimeColor(Color.yellow, Color.red, Color.yellow);

            Material fishMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            fishMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject brineSeaStars;
                GameObject brineFish;
                GameObject brineFishEyes;

                brineSeaStars = new GameObject("BrineSeaStars");
                brineSeaStars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_seastars").sharedMesh;
                brineSeaStars.AddComponent<MeshRenderer>().sharedMaterial = seaStarsMaterial;

                brineFish = new GameObject("BrineFish");
                brineFish.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_fish").sharedMesh;
                brineFish.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

                brineFishEyes = new GameObject("BrineFishEyes");
                brineFishEyes.transform.parent = brineFish.transform;
                brineFishEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_fish_eyes").sharedMesh;
                brineFishEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { brineSeaStars, brineFish };
            }
            else if (isSuperior)
            {
                GameObject brineQueenSeaStars;
                GameObject brineQueenFish;
                GameObject brineQueenFishEyes;
                //GameObject brineQueenFish1;
                //GameObject brineQueenFish2;
                //GameObject brineQueenFish3;
                //GameObject brineQueenFish1Eyes;
                //GameObject brineQueenFish2Eyes;
                //GameObject brineQueenFish3Eyes;

                brineQueenSeaStars = new GameObject("BrineSeaStars");
                brineQueenSeaStars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_seastars").sharedMesh;
                brineQueenSeaStars.AddComponent<MeshRenderer>().sharedMaterial = seaStarsMaterial;

                brineQueenFish = new GameObject("BrineFish");
                brineQueenFish.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish").sharedMesh;
                brineQueenFish.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

                brineQueenFishEyes = new GameObject("BrineFishEyes");
                brineQueenFishEyes.transform.parent = brineQueenFish.transform;
                brineQueenFishEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish_eyes").sharedMesh;
                brineQueenFishEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                //brineQueenFish1 = new GameObject("BrineFish1");
                //brineQueenFish1.transform.parent = brineQueenFish.transform;
                //brineQueenFish1.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish1").sharedMesh;
                //brineQueenFish1.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

                //brineQueenFish1Eyes = new GameObject("BrineFish1_Eyes");
                //brineQueenFish1Eyes.transform.parent = brineQueenFish1.transform;
                //brineQueenFish1Eyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish1_eyes").sharedMesh;
                //brineQueenFish1Eyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                //brineQueenFish2 = new GameObject("BrineFish2");
                //brineQueenFish2.transform.parent = brineQueenFish.transform;
                //brineQueenFish2.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish2").sharedMesh;
                //brineQueenFish2.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

                //brineQueenFish2Eyes = new GameObject("BrineFish2_Eyes");
                //brineQueenFish2Eyes.transform.parent = brineQueenFish2.transform;
                //brineQueenFish2Eyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish2_eyes").sharedMesh;
                //brineQueenFish2Eyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                //brineQueenFish3 = new GameObject("BrineFish3");
                //brineQueenFish3.transform.parent = brineQueenFish.transform;
                //brineQueenFish3.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish3").sharedMesh;
                //brineQueenFish3.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

                //brineQueenFish3Eyes = new GameObject("BrineFish3_Eyes");
                //brineQueenFish3Eyes.transform.parent = brineQueenFish3.transform;
                //brineQueenFish3Eyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("brine_queen_fish3_eyes").sharedMesh;
                //brineQueenFish3Eyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                //return new GameObject[] { brineQueenSeaStars, brineQueenFish1, brineQueenFish2, brineQueenFish3 };
                return new GameObject[] { brineQueenSeaStars, brineQueenFish };
            }

            return new GameObject[] { };
        }
    }
}
