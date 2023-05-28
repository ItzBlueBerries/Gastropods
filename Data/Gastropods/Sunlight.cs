using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.FedVaccables;
// using Gastropods.Components.Popups;
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
    internal class Sunlight
    {
        private static IdentifiableType sunlightGastropod;
        private static IdentifiableType sunlightQueenGastropod;
        private static IdentifiableType sunlightKingGastropod;
        private static Color[] gastroPalette = new Color[] { Color.yellow, Color.red, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.red, Color.yellow };
        private static Color[] gastroDiffPalette = new Color[] { Color.red, Color.red, Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, Color.red, Color.red };

        public static void Initialize()
        {
            sunlightGastropod = GastroUtility.CreateIdentifiable("Sunlight", false, false, false, false, false, LoadHex("#f2ba49"));
            sunlightQueenGastropod = GastroUtility.CreateIdentifiable("Sunlight", true, false, false, false, false, LoadHex("#f2ba49"));
            sunlightKingGastropod = GastroUtility.CreateIdentifiable("Sunlight", false, true, false, false, false, Color.red);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { sunlightGastropod, sunlightQueenGastropod, sunlightKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("FruitGroup") }
                        );

                        GastroUtility.CreateGastropod("Sunlight", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodSunlight")), sunlightGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_gastropod_shell").sharedMesh, null, CreateAccessories(false));
                        GastroUtility.CreateQueenGastropod("Sunlight", false, true, false, null, sunlightQueenGastropod, Il2CppType.Of<SunlightFedVaccable>(), Il2CppType.Of<SunlightReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true));
                        GastroUtility.CreateKingGastropod("Sunlight", false, true, false, null, sunlightKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true));
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(sunlightGastropod, "Sunlight",
                "Rise and shine! Sunlight is ready to satisfy your eyes.",

                "Sunlight Gastropods are one of the <b>second gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "You'll see blocks of sun sap embedded into their shell as a sign of.. well sun sap. " +
                "They look their best when the sun is out and is rather nice to others. This snail really knows how to show off! " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Sunlight", 2,
                "Another part of the Sunlight Gastropod is their confidant. Sunlight has a confidant who is a Sun.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround."
            );
            PatchPediaDirector.AddIdentifiablePage("Sunlight", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in the Starlight Strand."
            );
        }

        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material sunSapMaterial = UnityEngine.Object.Instantiate(Get<IdentifiableType>("SunSapCraft").prefab.gameObject.GetComponentInChildren<MeshRenderer>().material);

            Material sunMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            sunMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject sunlightSunSaps;
                GameObject sunlightSun;
                GameObject sunlightSunEyes;

                sunlightSunSaps = new GameObject("SunlightSunSaps");
                sunlightSunSaps.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_sunsaps").sharedMesh;
                sunlightSunSaps.AddComponent<MeshRenderer>().sharedMaterial = sunSapMaterial;
                // UnityEngine.Object.Instantiate(Get<IdentifiableType>("SunSapCraft").prefab.gameObject.transform.Find("sunSap_Id").gameObject.transform.Find("Sparkles").gameObject).transform.parent = sunlightSunSaps.transform;

                sunlightSun = new GameObject("SunlightSun");
                sunlightSun.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_sun").sharedMesh;
                sunlightSun.AddComponent<MeshRenderer>().sharedMaterial = sunMaterial;

                sunlightSunEyes = new GameObject("SunlightSunEyes");
                sunlightSunEyes.transform.parent = sunlightSun.transform;
                sunlightSunEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_sun_eyes").sharedMesh;
                sunlightSunEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { sunlightSunSaps, sunlightSun };
            }
            else if (isSuperior)
            {
                GameObject sunlightQueenSunSaps;
                GameObject sunlightQueenSuns;
                GameObject sunlightQueenSunsEyes;

                sunlightQueenSunSaps = new GameObject("SunlightSunSaps");
                sunlightQueenSunSaps.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_sunsaps").sharedMesh;
                sunlightQueenSunSaps.AddComponent<MeshRenderer>().sharedMaterial = sunSapMaterial;
                // UnityEngine.Object.Instantiate(Get<IdentifiableType>("SunSapCraft").prefab.gameObject.transform.Find("sunSap_Id").gameObject.transform.Find("Sparkles").gameObject).transform.parent = sunlightSunSaps.transform;

                sunlightQueenSuns = new GameObject("SunlightSuns");
                sunlightQueenSuns.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_suns").sharedMesh;
                sunlightQueenSuns.AddComponent<MeshRenderer>().sharedMaterial = sunMaterial;

                sunlightQueenSunsEyes = new GameObject("SunlightSunsEyes");
                sunlightQueenSunsEyes.transform.parent = sunlightQueenSuns.transform;
                sunlightQueenSunsEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_suns_eyes").sharedMesh;
                sunlightQueenSunsEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { sunlightQueenSunSaps, sunlightQueenSuns };
            }

            return new GameObject[] { };
        }
    }
}
