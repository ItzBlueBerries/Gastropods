using Gastropods.Assist;
using Gastropods.Components.Behaviours;
using Gastropods.Components;
using Il2Cpp;
using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;
using Gastropods.Components.ReproduceOnNearbys;
using Gastropods.Components.FedVaccables;

namespace Gastropods.Data.Gastropods
{
    internal class Traditional
    {
        private static IdentifiableType traditionalGastropod;
        private static IdentifiableType traditionalQueenGastropod;
        private static IdentifiableType traditionalKingGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#964B00"), LoadHex("#4E3524"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#4E3524"), LoadHex("#964B00") };
        private static Color[] gastroDiffPalette = new Color[] { LoadHex("#4E3524"), LoadHex("#4E3524"), Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, LoadHex("#4E3524"), LoadHex("#4E3524") };

        public static void Initialize()
        {
            traditionalGastropod = GastroUtility.CreateIdentifiable("Traditional", false, false, false, true, false, LoadHex("#964B00"));
            traditionalQueenGastropod = GastroUtility.CreateIdentifiable("Traditional", true, false, false, true, false, LoadHex("#964B00"));
            traditionalKingGastropod = GastroUtility.CreateIdentifiable("Traditional", false, true, false, true, false, LoadHex("#964B00"));

            Gastro.DO_SOMETHING_GASTROPODS.Add(traditionalGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(traditionalQueenGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(traditionalKingGastropod);
            Gastro.NON_EATABLE_GASTROPODS.Add(traditionalGastropod);
            Gastro.NON_EATABLE_GASTROPODS.Add(traditionalQueenGastropod);
            Gastro.NON_EATABLE_GASTROPODS.Add(traditionalKingGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { traditionalGastropod, traditionalQueenGastropod, traditionalKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup"), Get<IdentifiableTypeGroup>("FruitGroup"), Get<IdentifiableTypeGroup>("MeatGroup") }
                        );

                        GastroUtility.CreateGastropod("Traditional", false, true, true, true, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodTraditional")),
                            traditionalGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_eyes").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_shell").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_body").mesh, CreateAccessories(false)).Item1.AddComponent<TraditionalReform>();
                        GastroUtility.CreateQueenGastropod("Traditional", true, true, true, null,
                            traditionalQueenGastropod, Il2CppType.Of<TraditionalFedVaccable>(), Il2CppType.Of<TraditionalReproduce>(), gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_queen_eyes").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_queen_shell").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_body").mesh, CreateAccessories(true)).Item1.AddComponent<TraditionalReform>();
                        GastroUtility.CreateKingGastropod("Traditional", true, true, true, null,
                            traditionalKingGastropod, gastroDiffPalette, gastroDiffShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_queen_eyes").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_queen_shell").mesh,
                            GBundle.models.LoadFromObject<MeshFilter>("traditional_body").mesh, CreateAccessories(true, true)).Item1.AddComponent<TraditionalReform>();
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(traditionalGastropod, "Traditional",
                "This one.. is a bit different from the others.",

                "Traditional Gastropods are one of the <b>eleventh gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "These gastropods are considered rare and have rare abilities that other gastropods do not have. " +
                "They're a bit different from others though, form-wise. It's almost as if they originated from another world.. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Traditional", 2,
                "Traditional has rare abilities which are going to be explained here.\n" +
                "This gastropod is known for its form changing abilities. When this gastropod is fed / or collides with a food that another gastropod type <b>(with superiors)</b> eats they change form.\n" +
                "What kind of form change you may ask? Well they basically morph into the other gastropod type which you could use for whatever you need. " +
                "However, lots of gastropod types share the same diet. So due to this, they use this ability to instead randomly select a gastropod type that uses something from that diet.\n" +
                "It's interesting really, better to keep them around instead of feed them to slimes but some may say this ability is evolving and may change over time.."
            );
            PatchPediaDirector.AddIdentifiablePage("Traditional", 3,
                "This gastropod does not have any sort of confidant and has never shown signs of obtaining one.\n" +
                "Slimes are not interested in eating this gastropod due to its unknown origin."
            );
            PatchPediaDirector.AddIdentifiablePage("Traditional", 4,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in the Ember Valley and Powderfall Bluffs."
            );
        }

        private static GameObject[] CreateAccessories(bool isSuperior, bool diffPalette = false)
        {
            Material socketsMaterial = null;

            if (!diffPalette)
            {
                socketsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                socketsMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);
            }
            else if (diffPalette)
            {
                socketsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                socketsMaterial.SetSlimeColor(gastroDiffPalette[0], gastroDiffPalette[1], gastroDiffPalette[2]);
            }

            if (!isSuperior)
            {
                GameObject traditionalEyeSockets;

                traditionalEyeSockets = new GameObject("TraditionalEyeSockets");
                traditionalEyeSockets.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("traditional_eye_sockets").sharedMesh;
                traditionalEyeSockets.AddComponent<MeshRenderer>().sharedMaterial = socketsMaterial;

                return new GameObject[] { traditionalEyeSockets };
            }
            else if (isSuperior)
            {
                GameObject traditionalQueenEyeSockets;

                traditionalQueenEyeSockets = new GameObject("TraditionalQueenEyeSockets");
                traditionalQueenEyeSockets.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("traditional_queen_eye_sockets").sharedMesh;
                traditionalQueenEyeSockets.AddComponent<MeshRenderer>().sharedMaterial = socketsMaterial;

                return new GameObject[] { traditionalQueenEyeSockets };
            }

            return new GameObject[] { };
        }
    }
}
