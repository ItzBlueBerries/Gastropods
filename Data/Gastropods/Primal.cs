using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppMono.Math.Prime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Data.Gastropods
{
    internal class Primal
    {
        private static IdentifiableType primalGastropod;
        private static IdentifiableType primalQueenGastropod;
        private static IdentifiableType primalKingGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#964B00"), LoadHex("#8b0000"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#8b0000"), LoadHex("#964B00") };
        private static Color[] gastroDiffPalette = new Color[] { LoadHex("#8b0000"), LoadHex("#8b0000"), Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, LoadHex("#8b0000"), LoadHex("#8b0000") };

        public static void Initialize()
        {
            primalGastropod = GastroUtility.CreateIdentifiable("Primal", false, false, LoadHex("#964B00"));
            primalQueenGastropod = GastroUtility.CreateIdentifiable("Primal", true, false, LoadHex("#964B00"));
            primalKingGastropod = GastroUtility.CreateIdentifiable("Primal", false, true, LoadHex("#964B00"));

            Gastro.DEFENSIVE_GASTROPODS.Add(primalGastropod);
            Gastro.DEFENSIVE_GASTROPODS.Add(primalQueenGastropod);
            Gastro.DEFENSIVE_GASTROPODS.Add(primalKingGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(primalGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(primalQueenGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(primalKingGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Primal", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodPrimal")), primalGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_gastropod_shell").sharedMesh, null, CreateAccessories(false))
                            .Item1.AddComponent<HungryAttacker>();
                        GastroUtility.CreateQueenGastropod("Primal", false, true, false, null, primalQueenGastropod, Il2CppType.Of<PrimalFedVaccable>(), Il2CppType.Of<PrimalReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true))
                            .Item1.AddComponent<HungryAttacker>();
                        GastroUtility.CreateKingGastropod("Primal", false, true, false, null, primalKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true))
                            .Item1.AddComponent<HungryAttacker>();
                        break;
                    }
            }
        }

        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material teethMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            teethMaterial.SetSlimeColor(Color.white, Color.grey, Color.white);

            Material snakeMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            snakeMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            GameObject primalTeeth = new GameObject("PrimalSnake");
            primalTeeth.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("primal_teeth").sharedMesh;
            primalTeeth.AddComponent<MeshRenderer>().sharedMaterial = teethMaterial;

            if (!isSuperior)
            {
                GameObject primalSnake;
                GameObject primalSnakeEyes;

                primalSnake = new GameObject("PrimalSnake");
                primalSnake.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("primal_snake").sharedMesh;
                primalSnake.AddComponent<MeshRenderer>().sharedMaterial = snakeMaterial;

                primalSnakeEyes = new GameObject("PrimalSnakeEyes");
                primalSnakeEyes.transform.parent = primalSnake.transform;
                primalSnakeEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("primal_snake_eyes").sharedMesh;
                primalSnakeEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { primalTeeth, primalSnake };
            }
            else if (isSuperior)
            {
                GameObject primalQueenSnakes;
                GameObject primalQueenSnakeEyes;

                primalQueenSnakes = new GameObject("PrimalSnakes");
                primalQueenSnakes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("primal_queen_snakes").sharedMesh;
                primalQueenSnakes.AddComponent<MeshRenderer>().sharedMaterial = snakeMaterial;

                primalQueenSnakeEyes = new GameObject("PrimalSnakesEyes");
                primalQueenSnakeEyes.transform.parent = primalQueenSnakes.transform;
                primalQueenSnakeEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("primal_queen_snakes_eyes").sharedMesh;
                primalQueenSnakeEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { primalTeeth, primalQueenSnakes };
            }

            return new GameObject[] { };
        }
    }
}
