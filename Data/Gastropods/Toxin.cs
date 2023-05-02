using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.Attackers;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Il2Cpp;
using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;

namespace Gastropods.Data.Gastropods
{
    internal class Toxin
    {
        private static IdentifiableType toxinGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#A020F0"), Color.black, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.black, LoadHex("#A020F0") };

        public static void Initialize()
        {
            toxinGastropod = GastroUtility.CreateIdentifiable("Toxin", false, false, true, false, LoadHex("#A020F0"));
            Gastro.DO_SOMETHING_GASTROPODS.Add(toxinGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Toxin", false, true, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodToxin")), toxinGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("toxin_gastropod_eyes").sharedMesh,
                            GBundle.models.LoadFromObject<MeshFilter>("toxin_gastropod_shell").sharedMesh, null, CreateAccessories())
                            .Item1.AddComponent<HarpoonAttacker>();
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(toxinGastropod, "Toxin",
                "That look in its eyes.. contaminating the area. How wonderful.",

                "Toxin Gastropods are one of the <b>third gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "This specific type of gastropod is defensive but will not hurt you. " +
                "They'll one-shot slimes (not largos) with their harpoons if they get too close. This could be an issue when feeding them to other slimes. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "This gastropod type cannot reproduce and has to be found spawning on its own.\n" +
                "This means it is incapable of being mass produced, have fun saving them up!"
            );
            PatchPediaDirector.AddIdentifiablePage("Toxin", 2,
                "Toxin does not have a confidant. You could say their confidant is their harpoon though if you'd like!"
            );
            PatchPediaDirector.AddIdentifiablePage("Toxin", 3,
                "This is a type of gastropod that doesn't have kings & queens.\n" +
                "They do spawn on their own and require to be found. They cannot reproduce whatsoever.\n" +
                "This gastropod type specifically spawns everywhere but Powderfall Bluffs."
            );
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
