using Gastropods.Assist;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Il2Cpp;
using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            brineGastropod = GastroUtility.CreateIdentifiable("Brine", false, false, Color.cyan);
            brineQueenGastropod = GastroUtility.CreateIdentifiable("Brine", true, false, Color.cyan);
            brineKingGastropod = GastroUtility.CreateIdentifiable("Brine", false, true, Color.cyan);
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
