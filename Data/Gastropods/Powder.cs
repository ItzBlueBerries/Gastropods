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
            powderGastropod = GastroUtility.CreateIdentifiable("Powder", false, false, false, false, false, Color.white);
            powderQueenGastropod = GastroUtility.CreateIdentifiable("Powder", true, false, false, false, false, Color.white);
            powderKingGastropod = GastroUtility.CreateIdentifiable("Powder", false, true, false, false, false, LoadHex("#d0eceb"));
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { powderGastropod, powderQueenGastropod, powderKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup"), Get<IdentifiableTypeGroup>("FruitGroup") }
                        );

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

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(powderGastropod, "Powder",
                "I wonder if they can summon snowstorms.",

                "Powder Gastropods are one of the <b>fifth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "Their shell is like a tower of snow with a little snowball on top! " +
                "When its day vs. night, you may see a slight change in their shell color due to the shading. This could be a cool fact? " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Powder", 2,
                "Another part of the Powder Gastropod is their confidant. Powder has a confidant who is a Snowball.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "It is also known that their superiors do not have more than one confidant. They all share the same number of confidants."
            );
            PatchPediaDirector.AddIdentifiablePage("Powder", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in the Powderfall Bluffs."
            );
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
