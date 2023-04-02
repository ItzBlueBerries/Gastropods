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
    internal class Toxin
    {
        private static IdentifiableType toxinGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#A020F0"), Color.black, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.black, LoadHex("#A020F0") };

        public static void Initialize()
        {
            toxinGastropod = GastroUtility.CreateIdentifiable("Toxin", false, false, LoadHex("#A020F0"));
            GastroEntry.DEFENSIVE_GASTROPODS.Add(toxinGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Toxin", false, true, true, false, null, toxinGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("toxin_gastropod_eyes").sharedMesh,
                            GBundle.models.LoadFromObject<MeshFilter>("toxin_gastropod_shell").sharedMesh, null, CreateAccessories())
                            .Item1.AddComponent<HarpoonAttacker>();
                        break;
                    }
            }
        }

        private static GameObject[] CreateAccessories()
        {
            Material harpoonMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            harpoonMaterial.SetSlimeColor(Color.white, Color.grey, Color.white);

            GameObject toxinShellHarpoon;

            toxinShellHarpoon = new GameObject("ToxinShellHarpoon");
            toxinShellHarpoon.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("toxin_gastropod_shell_harpoon").sharedMesh;
            toxinShellHarpoon.AddComponent<MeshRenderer>().sharedMaterial = harpoonMaterial;

            return new GameObject[] { toxinShellHarpoon };
        }
    }
}
