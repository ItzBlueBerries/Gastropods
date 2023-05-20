using Gastropods.Assist;
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
    internal class Bubbly
    {
        private static IdentifiableType bubblyGastropod;
        private static IdentifiableType bubblyQueenGastropod;
        private static IdentifiableType bubblyKingGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#ADD8E6"), LoadHex("#FFC0CB"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#FFC0CB"), LoadHex("#ADD8E6") };
        private static Color[] gastroDiffPalette = new Color[] { LoadHex("#FFC0CB"), LoadHex("#FFC0CB"), Color.white };
        private static Color[] gastroDiffShellPalette = new Color[] { Color.white, LoadHex("#FFC0CB"), LoadHex("#FFC0CB") };

        public static void Initialize()
        {
            bubblyGastropod = GastroUtility.CreateIdentifiable("Bubbly", false, false, false, false, LoadHex("#ADD8E6"));
            bubblyQueenGastropod = GastroUtility.CreateIdentifiable("Bubbly", true, false, false, false, LoadHex("#ADD8E6"));
            bubblyKingGastropod = GastroUtility.CreateIdentifiable("Bubbly", false, true, false, false, LoadHex("#FFC0CB"));
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Material translucentMaterial = new Material(GBundle.shaders.LoadAsset("Translucent").Cast<Shader>());
                        translucentMaterial.SetColor("_TintColor", LoadHex("#ADD8E6"));
                        Material diffTranslucentMaterial = new Material(GBundle.shaders.LoadAsset("Translucent").Cast<Shader>());
                        translucentMaterial.SetColor("_TintColor", LoadHex("#FFC0CB"));

                        GastroUtility.CreateGastropod("Bubbly", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodBubbly")), bubblyGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("bubbly_shell").sharedMesh, null, CreateAccessories(false)).Item1.transform.Find("GastroParts/GastropodBody/GastropodShell").gameObject.GetComponent<MeshRenderer>().sharedMaterial = translucentMaterial;
                        GastroUtility.CreateQueenGastropod("Bubbly", false, true, false, null, bubblyQueenGastropod, Il2CppType.Of<BubblyFedVaccable>(), Il2CppType.Of<BubblyReproduce>(), gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("bubbly_queen_shell").sharedMesh, null, CreateAccessories(true)).Item1.transform.Find("GastroParts/GastropodBody/GastropodShell").gameObject.GetComponent<MeshRenderer>().sharedMaterial = translucentMaterial;
                        GastroUtility.CreateKingGastropod("Bubbly", false, true, false, null, bubblyKingGastropod, gastroDiffPalette, gastroDiffShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("bubbly_queen_shell").sharedMesh, null, CreateAccessories(true)).Item1.transform.Find("GastroParts/GastropodBody/GastropodShell").gameObject.GetComponent<MeshRenderer>().sharedMaterial = diffTranslucentMaterial;
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(bubblyGastropod, "Bubbly",
                "You can see what's under their shell..",

                "Bubbly Gastropods are one of the <b>tenth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "Their shell is translucent! You can see what's under their shell, is that a good or bad thing? Within that shell though, you can also see their confidants living inside. " +
                "Touching the shell may not go so well though, who knows what its made of? It all depends because the bubbles have a chance of almost impossible to ever pop. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Bubbly", 2,
                "Another part of the Bubbly Gastropod is their confidant. Brine has a confidant who is a Bar of Soap.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "Another known fact about this type of gastropod is the superiors only have 2 confidants."
            );
            PatchPediaDirector.AddIdentifiablePage("Bubbly", 3,
                "This is a type of gastropod that'll have kings & queens.\n" +
                "They do not spawn on their own and require to be reproduced by a superior. Superiors spawn on their own.\n" +
                "This gastropod type specifically spawns in Rainbow Fields and Starlight Strand."
            );
        }

        private static GameObject[] CreateAccessories(bool isSuperior)
        {
            Material soapMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            soapMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject bubblySoap;
                GameObject bubblySoapEyes;

                bubblySoap = new GameObject("BubblySoap");
                bubblySoap.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("bubbly_soap").sharedMesh;
                bubblySoap.AddComponent<MeshRenderer>().sharedMaterial = soapMaterial;

                bubblySoapEyes = new GameObject("BubblySoapEyes");
                bubblySoapEyes.transform.parent = bubblySoap.transform;
                bubblySoapEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("bubbly_soap_eyes").sharedMesh;
                bubblySoapEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { bubblySoap };
            }
            else if (isSuperior)
            {
                GameObject bubblyQueenSoaps;
                GameObject bubblyQueenSoapsEyes;

                bubblyQueenSoaps = new GameObject("BubblySoaps");
                bubblyQueenSoaps.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("bubbly_queen_soaps").sharedMesh;
                bubblyQueenSoaps.AddComponent<MeshRenderer>().sharedMaterial = soapMaterial;

                bubblyQueenSoapsEyes = new GameObject("BubblySoapsEyes");
                bubblyQueenSoapsEyes.transform.parent = bubblyQueenSoaps.transform;
                bubblyQueenSoapsEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("bubbly_queen_soaps_eyes").sharedMesh;
                bubblyQueenSoapsEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { bubblyQueenSoaps };
            }

            return new GameObject[] { };
        }
    }
}
