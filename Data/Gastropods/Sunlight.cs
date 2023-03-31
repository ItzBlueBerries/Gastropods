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
            sunlightGastropod = GastroUtility.CreateIdentifiable("Sunlight", false, false, Color.yellow);
            sunlightQueenGastropod = GastroUtility.CreateIdentifiable("Sunlight", true, false, Color.yellow);
            sunlightKingGastropod = GastroUtility.CreateIdentifiable("Sunlight", false, true, Color.yellow);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Sunlight", true, true, null, sunlightGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_gastropod_shell").sharedMesh, CreateAccessories(false));
                        GastroUtility.CreateQueenGastropod("Sunlight", true, null, sunlightQueenGastropod, Il2CppType.Of<SunlightFedVaccable>(), Il2CppType.Of<SunlightReproduce>(), gastroPalette, gastroShellPalette, 
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_gastropod_shell").sharedMesh, CreateAccessories(true));
                        GastroUtility.CreateKingGastropod("Sunlight", true, null, sunlightKingGastropod, gastroDiffPalette, gastroDiffShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("sunlight_queen_gastropod_shell").sharedMesh, CreateAccessories(true));
                        break;
                    }
            }
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
