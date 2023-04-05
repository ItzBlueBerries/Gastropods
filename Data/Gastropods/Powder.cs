using Gastropods.Assist;
using Gastropods.Components;
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
    internal class Powder
    {
        private static IdentifiableType powderGastropod;
        private static IdentifiableType powderQueenGastropod;
        private static IdentifiableType powderKingGastropod;
        private static Color[] gastroPalette = new Color[] { Color.white, LoadHex("#a0e6ec"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#a0e6ec"), Color.white };
        private static Color[] gastroDiffPalette = new Color[] { LoadHex("#d0eceb"), LoadHex("#a0e6ec"), Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, LoadHex("#a0e6ec"), LoadHex("#d0eceb") };

        public static void Initialize()
        {
            powderGastropod = GastroUtility.CreateIdentifiable("Powder", false, false, Color.white);
            powderQueenGastropod = GastroUtility.CreateIdentifiable("Powder", true, false, Color.white);
            powderKingGastropod = GastroUtility.CreateIdentifiable("Powder", false, true, Color.white);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Powder", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodPowder")), powderGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("powder_gastropod_shell").sharedMesh, null, CreateAccessories(false));
                        GastroUtility.CreateQueenGastropod("Powder", false, true, false, null, powderQueenGastropod, Il2CppType.Of<PowderFedVaccable>(), Il2CppType.Of<PowderReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("powder_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true));
                        GastroUtility.CreateKingGastropod("Powder", false, true, false, null, powderKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("powder_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true));
                        break;
                    }
            }
        }

        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material snowballMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            snowballMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject powderSnowball;
                GameObject powderSnowballEyes;

                powderSnowball = new GameObject("PowderSnowball");
                powderSnowball.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("powder_snowball").sharedMesh;
                powderSnowball.AddComponent<MeshRenderer>().sharedMaterial = snowballMaterial;
                // powderSnowball.AddComponent<SnowParticleEmitter>();
                powderSnowball.AddComponent<ObjectRotation>();

                powderSnowballEyes = new GameObject("PowderSnowballEyes");
                powderSnowballEyes.transform.parent = powderSnowball.transform;
                powderSnowballEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("powder_snowball_eyes").sharedMesh;
                powderSnowballEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { powderSnowball };
            }
            else if (isSuperior)
            {
                GameObject powderQueenSnowball1;
                GameObject powderQueenSnowball1Eyes;

                powderQueenSnowball1 = new GameObject("PowderSnowballOne");
                powderQueenSnowball1.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("powder_queen_snowball_one").sharedMesh;
                powderQueenSnowball1.AddComponent<MeshRenderer>().sharedMaterial = snowballMaterial;
                powderQueenSnowball1.AddComponent<ObjectRotation>();

                powderQueenSnowball1Eyes = new GameObject("PowderSnowballOneEyes");
                powderQueenSnowball1Eyes.transform.parent = powderQueenSnowball1.transform;
                powderQueenSnowball1Eyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("powder_queen_snowball_one_eyes").sharedMesh;
                powderQueenSnowball1Eyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { powderQueenSnowball1 };
            }

            return new GameObject[] { };
        }
    }
}
