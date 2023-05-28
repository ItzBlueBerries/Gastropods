using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.Attackers;
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
            primalGastropod = GastroUtility.CreateIdentifiable("Primal", false, false, true, false, false, LoadHex("#964B00"));
            primalQueenGastropod = GastroUtility.CreateIdentifiable("Primal", true, false, true, false, false, LoadHex("#964B00"));
            primalKingGastropod = GastroUtility.CreateIdentifiable("Primal", false, true, true, false, false, LoadHex("#8b0000"));

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
                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { primalGastropod, primalQueenGastropod, primalKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("MeatGroup") }
                        );

                        GastroUtility.CreateGastropod("Primal", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodPrimal")), primalGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_gastropod_shell").sharedMesh, null, CreateAccessories(false))
                            .Item1.AddComponent<HungryAttacker>();
                        GastroUtility.CreateQueenGastropod("Primal", false, true, false, null, primalQueenGastropod, Il2CppType.Of<PrimalFedVaccable>(), Il2CppType.Of<PrimalReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true))
                            .Item1.AddComponents(Il2CppType.Of<HungryAttacker>());
                        GastroUtility.CreateKingGastropod("Primal", false, true, false, null, primalKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("primal_queen_gastropod_shell").sharedMesh, null, CreateAccessories(true))
                            .Item1.AddComponents(Il2CppType.Of<HungryAttacker>());
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(primalGastropod, "Primal",
                "You've got something stuck in your teeth there bud- Oh its just a piece of meat.",

                "Primal Gastropods are one of the <b>fourth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "This specific type of gastropod is defensive but will not hurt you. " +
                "They'll feast on slimes or meat (excluding gastropods) that collide with them, they may also follow one nearby every now and then. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Primal", 2,
                "Another part of the Primal Gastropod is their confidant. Primal has a confidant who is a Snake.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround."
            );
            PatchPediaDirector.AddIdentifiablePage("Primal", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in the Ember Valley."
            );
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
