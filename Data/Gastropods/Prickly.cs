using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.Attackers;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Data.Gastropods
{
    internal class Prickly
    {
        private static IdentifiableType pricklyGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#60a44f"), LoadHex("#8B0000"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#8B0000"), LoadHex("#60a44f") };

        public static void Initialize()
        {
            pricklyGastropod = GastroUtility.CreateIdentifiable("Prickly", false, false, true, false, LoadHex("#60a44f"));
            Gastro.DO_SOMETHING_GASTROPODS.Add(pricklyGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Prickly", false, true, true, false, null, pricklyGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("prickly_eyes").sharedMesh,
                            GBundle.models.LoadFromObject<MeshFilter>("prickly_shell").sharedMesh, null, CreateAccessories())
                            .Item1.AddComponent<SpineAttacker>();
                        break;
                    }
            }
        }

        private static GameObject[] CreateAccessories()
        {
            Material spineMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            spineMaterial.SetSlimeColor(LoadHex("#8B0000"), LoadHex("#8B0000"), LoadHex("#8B0000"));

            Material petalsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            petalsMaterial.SetSlimeColor(Color.red, LoadHex("#8B0000"), Color.red);

            Material pistilMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            pistilMaterial.SetSlimeColor(Color.yellow, Color.white, Color.yellow);

            GameObject pricklyShellSpines;
            GameObject pricklyFlower;
            GameObject pricklyFlowerPetals;
            GameObject pricklyFlowerPistil;

            pricklyShellSpines = new GameObject("PricklyShellSpines");
            pricklyShellSpines.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("prickly_spines").sharedMesh;
            pricklyShellSpines.AddComponent<MeshRenderer>().sharedMaterial = spineMaterial;

            pricklyFlower = new GameObject("PricklyFlower");

            pricklyFlowerPetals = new GameObject("PricklyFlowerPetals");
            pricklyFlowerPetals.transform.parent = pricklyFlower.transform;
            pricklyFlowerPetals.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("prickly_flower_petals").sharedMesh;
            pricklyFlowerPetals.AddComponent<MeshRenderer>().sharedMaterial = petalsMaterial;

            pricklyFlowerPistil = new GameObject("PricklyFlowerPistil");
            pricklyFlowerPistil.transform.parent = pricklyFlower.transform;
            pricklyFlowerPistil.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("prickly_flower_pistil").sharedMesh;
            pricklyFlowerPistil.AddComponent<MeshRenderer>().sharedMaterial = pistilMaterial;

            return new GameObject[] { pricklyShellSpines, pricklyFlower };
        }
    }
}
