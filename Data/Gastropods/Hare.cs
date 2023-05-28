using Gastropods.Assist;
using Gastropods.Components.Behaviours;
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
    internal class Hare
    {
        private static IdentifiableType hareGastropod;
        private static IdentifiableType hareQueenGastropod;
        private static IdentifiableType hareKingGastropod;
        private static Color[] gastroPalette = new Color[] { Color.grey, Color.yellow, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.yellow, Color.grey };
        private static Color[] gastroDiffPalette = new Color[] { Color.yellow, Color.yellow, Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, Color.yellow, Color.yellow };

        public static void Initialize()
        {
            hareGastropod = GastroUtility.CreateIdentifiable("Hare", false, false, false, false, false, Color.grey);
            hareQueenGastropod = GastroUtility.CreateIdentifiable("Hare", true, false, false, false, false, Color.grey);
            hareKingGastropod = GastroUtility.CreateIdentifiable("Hare", false, true, false, false, false, Color.yellow);

            Gastro.DO_SOMETHING_GASTROPODS.Add(hareGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(hareQueenGastropod);
            Gastro.DO_SOMETHING_GASTROPODS.Add(hareKingGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Gastro.GASTROPOD_DIET_DICT.TryAdd(
                            new IdentifiableType[] { hareGastropod, hareQueenGastropod, hareKingGastropod },
                            new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup") }
                        );

                        GastroUtility.CreateGastropod("Hare", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodHare")), hareGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("hare_shell").sharedMesh, null, CreateAccessories(gastroShellPalette, false)).Item1.AddComponent<AlwaysBeHoppingAround>();
                        GastroUtility.CreateQueenGastropod("Hare", false, true, false, null, hareQueenGastropod, Il2CppType.Of<HareFedVaccable>(), Il2CppType.Of<HareReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("hare_queen_shell").sharedMesh, null, CreateAccessories(gastroShellPalette, true)).Item1.AddComponent<AlwaysBeHoppingAround>();
                        GastroUtility.CreateKingGastropod("Hare", false, true, false, null, hareKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("hare_queen_shell").sharedMesh, null, CreateAccessories(gastroDiffShellPalette, true)).Item1.AddComponent<AlwaysBeHoppingAround>();
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(hareGastropod, "Hare",
                "A hop in a skip and oh- wait this sounds familiar.",

                "Hare Gastropods are one of the <b>ninth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "They love hopping around! This can make them a bit more trouble or it could not, depends on how you like to handle it. " +
                "Their superiors are a bit more reproductive than the other superior gastropods and can produce from 2 - 4 gastropods at once. Great for mass producing though can easily take over the ranch. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Hare", 2,
                "Another part of the Hare Gastropod is their confidant. Brine has a confidant who is a Bunny.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "The superiors of this gastropod type may seem to have no confidants although their shells are very large so their confidants are housed and living inside."
            );
            PatchPediaDirector.AddIdentifiablePage("Hare", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns everywhere."
            );
        }

        private static GameObject[] CreateAccessories(Color[] shellPalette, bool isSuperior)
        {
            Material earsMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            earsMaterial.SetSlimeColor(shellPalette[0], shellPalette[1], shellPalette[2]);
            Material tailMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            tailMaterial.SetSlimeColor(Color.white, Color.white, Color.white);

            Material bunnyMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            bunnyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject hareEars;
                GameObject hareTail;
                GameObject hareBunny;
                GameObject hareBunnyEyes;
                GameObject hareBunnyEars;
                GameObject hareBunnyTail;

                hareEars = new GameObject("HareEars");
                hareEars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_ears").sharedMesh;
                hareEars.AddComponent<MeshRenderer>().sharedMaterial = earsMaterial;

                hareTail = new GameObject("HareTail");
                hareTail.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_tail").sharedMesh;
                hareTail.AddComponent<MeshRenderer>().sharedMaterial = tailMaterial;

                hareBunny = new GameObject("HareBunny");
                hareBunny.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_bunny").sharedMesh;
                hareBunny.AddComponent<MeshRenderer>().sharedMaterial = bunnyMaterial;

                hareBunnyEyes = new GameObject("HareBunnyEyes");
                hareBunnyEyes.transform.parent = hareBunny.transform;
                hareBunnyEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_bunny_eyes").sharedMesh;
                hareBunnyEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                hareBunnyEars = new GameObject("HareBunnyEars");
                hareBunnyEars.transform.parent = hareBunny.transform;
                hareBunnyEars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_bunny_ears").sharedMesh;
                hareBunnyEars.AddComponent<MeshRenderer>().sharedMaterial = earsMaterial;

                hareBunnyTail = new GameObject("HareBunnyTail");
                hareBunnyTail.transform.parent = hareBunny.transform;
                hareBunnyTail.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_bunny_tail").sharedMesh;
                hareBunnyTail.AddComponent<MeshRenderer>().sharedMaterial = tailMaterial;

                return new GameObject[] { hareEars, hareTail, hareBunny };
            }
            else if (isSuperior)
            {
                GameObject hareEars;
                GameObject hareTail;

                hareEars = new GameObject("HareEars");
                hareEars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_ears").sharedMesh;
                hareEars.AddComponent<MeshRenderer>().sharedMaterial = earsMaterial;

                hareTail = new GameObject("HareTail");
                hareTail.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("hare_tail").sharedMesh;
                hareTail.AddComponent<MeshRenderer>().sharedMaterial = tailMaterial;

                return new GameObject[] { hareEars, hareTail };
            }

            return new GameObject[] { };
        }
    }
}
