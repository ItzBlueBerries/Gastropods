using Gastropods.Assist;
using Gastropods.Components.Behaviours;
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

namespace Gastropods.Data.Gastropods
{
    internal class Crowned
    {
        private static IdentifiableType crownedGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#F6BE00"), LoadHex("#7851A9"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#7851A9"), LoadHex("#F6BE00") };

        public static void Initialize() => crownedGastropod = GastroUtility.CreateIdentifiable("Crowned", false, false, false, false, true, LoadHex("#F6BE00"));

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        // too lazy to do fields and such
                        GameObject crownedPrefab = GastroUtility.CreateGastropod("Crowned", false, false, false, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodCrowned")), crownedGastropod, gastroPalette, gastroShellPalette, null, null, null, CreateAccessories()).Item1;
                        UnityEngine.Object.Destroy(crownedPrefab.transform.Find("GastroParts/GastropodBody/GastropodShell").gameObject);
                        break;
                    }
            }
        }


        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(crownedGastropod, "Crowned",
                "Pure royalty. Higher than Brine? EhhHhhHhhhhh",

                "Crowned Gastropods are one of the <b>first supreme gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "While being the first supreme, they have power. Though this power is hidden for some mysterious reason which is still being researched on.. " +
                "...While the powers are still being discovered, once discovered this entry will be updated.. so much to find out. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "This gastropod type cannot reproduce and has to be found spawning on its own.\n" +
                "This means it is incapable of being mass produced, have fun saving them up!"
            );
            PatchPediaDirector.AddIdentifiablePage("Crowned", 2,
                "<b>    Origin   </b>\n" +
                "When Crowned arrived they were already unique. A gastropod without a shell but a very large confidant that resembled a crown.\n" +
                "Sooner or later they were considered royalty, their mysterious powers protected the other gastropods which made them loyal. " +
                "Before this though, they were just like any other gastropod. Some grew to be superiors which were pratically considered the higher ups.\n" +
                "Crowned never grew though, as being a singular gastropod.. but soon to be supreme. Once they started to be considered as royalty, that's when the supreme gastropods were formed. " +
                "5 Gastropods (including Crowned) were to be supreme members. What were the requirements? Offering protection, great power and anything else that qualifies to be higher than the usual superiors.\n" +
                "Once there was a gastropod that declined when they were offered, I guess another one took the spot then. Supreme members are hard to find but Crowned could lead you to them."
            );
            PatchPediaDirector.AddIdentifiablePage("Crowned", 3,
                "Another part of the Crowned Gastropod is their confidant. Crowned has a confidant who is a Crown.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "Crowned has a mysterious power that is yet to be revealed."
            );
            PatchPediaDirector.AddIdentifiablePage("Crowned", 4,
                "This is a type of gastropod that doesn't have kings & queens.\n" +
                "They do spawn on their own and require to be found. They cannot reproduce whatsoever.\n" +
                "This gastropod type specifically spawns everywhere."
            );
        }


        private static GameObject[] CreateAccessories()
        {
            Material crownMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            crownMaterial.SetSlimeColor(LoadHex("#F6BE00"), LoadHex("#F6BE00"), LoadHex("#F6BE00"));

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            GameObject crownedCrown;
            GameObject crownedCrownEyes;

            crownedCrown = new GameObject("CrownedCrown");
            crownedCrown.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crowned_crown").sharedMesh;
            crownedCrown.AddComponent<MeshRenderer>().sharedMaterial = crownMaterial;

            crownedCrownEyes = new GameObject("CrownedCrownEyes");
            crownedCrownEyes.transform.parent = crownedCrown.transform;
            crownedCrownEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crowned_crown_eyes").sharedMesh;
            crownedCrownEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

            return new GameObject[] { crownedCrown };
        }
    }
}
