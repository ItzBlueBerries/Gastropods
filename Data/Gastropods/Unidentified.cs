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
    internal class Unidentified
    {
        private static IdentifiableType unidentifiedGastropod;
        private static (GameObject, (Material, Material, Material)) unidentifiedPrefab;
        private static Color[] gastroPalette = new Color[] { Color.white, Color.white, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.white, Color.white };

        public static void Initialize()
        {
            unidentifiedGastropod = GastroUtility.CreateIdentifiable("Unidentified", false, false, Color.white);
            Gastro.RARE_GASTROPODS.Add(unidentifiedGastropod);
            Gastro.DEFENSIVE_GASTROPODS.Add(unidentifiedGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(unidentifiedGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        unidentifiedPrefab = GastroUtility.CreateGastropod("Unidentified", false, true, false, false, null, unidentifiedGastropod, gastroPalette, gastroShellPalette, null, null, null, CreateAccessories());
                        UnityEngine.Object.Destroy(unidentifiedPrefab.Item1.GetComponent<Vacuumable>());
                        unidentifiedPrefab.Item1.AddComponent<UnidentifiedTransform>();
                        unidentifiedPrefab.Item1.AddComponent<RandomRigidMovement>();
                        unidentifiedPrefab.Item1.AddComponent<DestroyAfterHours>();
                        unidentifiedPrefab.Item1.AddComponent<ObjectTwirl>();
                        unidentifiedPrefab.Item1.AddComponent<BounceActorOnCollision>();
                        break;
                    }
            }
        }

        private static GameObject[] CreateAccessories()
        {
            Material confidantsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Phosphor").GetSlimeMat(0));
            confidantsMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);
            confidantsMaterial.SetColor("_GlowTop", Color.white);

            GameObject unidentifiedConfidants;

            unidentifiedConfidants = new GameObject("UnidentifiedConfidants");
            unidentifiedConfidants.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("unidentified_confidants").sharedMesh;
            unidentifiedConfidants.AddComponent<MeshRenderer>().sharedMaterial = confidantsMaterial;
            unidentifiedConfidants.AddComponent<ObjectRotation>().RotationSpeed = new Vector3(90, 180, 90);

            return new GameObject[] { unidentifiedConfidants };
        }
    }
}
