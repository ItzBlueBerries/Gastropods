using Gastropods.Assist;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;

namespace Gastropods.Data.Gastropods
{
    internal class Crepe
    {
        private static IdentifiableType crepeGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#ffd886"), LoadHex("#eba30f"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#eba30f"), LoadHex("#ffd886") };

        public static void Initialize() => crepeGastropod = GastroUtility.CreateIdentifiable("Crepe", false, false, false, false, LoadHex("#ffd886"));

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Crepe", true, false, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodCrepe")), crepeGastropod, gastroPalette, gastroShellPalette, null,
                            GBundle.models.LoadFromObject<MeshFilter>("crepe_shell").sharedMesh, null, CreateAccessories());
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(crepeGastropod, "Crepe",
                "It looks.. delicious.. can you eat it or only slimes?",

                "Crepe Gastropods are one of the <b>seventh gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "This specific type of gastropod is not defensive but also doesn't have many special traits. " +
                "Although they could be prioritized as all slimes that eat meat favor them and will produce <b>6 plorts</b> each feed! They're quite delicious indeed for them. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "This gastropod type cannot reproduce and has to be found spawning on its own.\n" +
                "This means it is incapable of being mass produced, have fun saving them up!"
            );
            PatchPediaDirector.AddIdentifiablePage("Crepe", 2,
                "Another part of the Crepe Gastropod is their confidant. Crepe has a confidant who is a Donut.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround."
            );
            PatchPediaDirector.AddIdentifiablePage("Crepe", 3,
                "This is a type of gastropod that doesn't have kings & queens.\n" +
                "They do spawn on their own and require to be found. They cannot reproduce whatsoever.\n" +
                "This gastropod type specifically spawns in the Starlight Strand."
            );
        }

        private static GameObject[] CreateAccessories()
        {
            Material cuberryMaterial = UnityEngine.Object.Instantiate(Get<IdentifiableType>("CuberryFruit").prefab.gameObject.GetComponentInChildren<MeshRenderer>().material);
            Material pogofruitMaterial = UnityEngine.Object.Instantiate(Get<IdentifiableType>("PogoFruit").prefab.gameObject.GetComponentInChildren<MeshRenderer>().material);
            Material mangoMaterial = UnityEngine.Object.Instantiate(Get<IdentifiableType>("MangoFruit").prefab.gameObject.GetComponentInChildren<MeshRenderer>().material);

            cuberryMaterial.SetFloat("_LeafSwayStrength", 0);
            pogofruitMaterial.SetFloat("_LeafSwayStrength", 0);
            mangoMaterial.SetFloat("_LeafSwayStrength", 0);

            Material creamMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            creamMaterial.SetSlimeColor(Color.white, Color.grey, Color.white);

            Material donutMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            donutMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            GameObject crepeShellCuberry;
            GameObject crepeShellPogofruit;
            GameObject crepeShellMango;
            GameObject crepeShellCream;
            GameObject crepeDonut;
            GameObject crepeDonutCream;
            GameObject crepeDonutEyes;

            crepeShellCuberry = new GameObject("CrepeShellCuberry");
            crepeShellCuberry.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_shell_cuberry").sharedMesh;
            crepeShellCuberry.AddComponent<MeshRenderer>().sharedMaterial = cuberryMaterial;

            crepeShellPogofruit = new GameObject("CrepeShellPogofruit");
            crepeShellPogofruit.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_shell_pogofruit").sharedMesh;
            crepeShellPogofruit.AddComponent<MeshRenderer>().sharedMaterial = pogofruitMaterial;

            crepeShellMango = new GameObject("CrepeShellMango");
            crepeShellMango.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_shell_mango").sharedMesh;
            crepeShellMango.AddComponent<MeshRenderer>().sharedMaterial = mangoMaterial;

            crepeShellCream = new GameObject("CrepeShellCream");
            crepeShellCream.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_shell_cream").sharedMesh;
            crepeShellCream.AddComponent<MeshRenderer>().sharedMaterial = creamMaterial;

            crepeDonut = new GameObject("CrepeDonut");
            crepeDonut.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_donut").sharedMesh;
            crepeDonut.AddComponent<MeshRenderer>().sharedMaterial = donutMaterial;

            crepeDonutCream = new GameObject("CrepeDonutCream");
            crepeDonutCream.transform.parent = crepeDonut.transform;
            crepeDonutCream.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_donut_cream").sharedMesh;
            crepeDonutCream.AddComponent<MeshRenderer>().sharedMaterial = creamMaterial;

            crepeDonutEyes = new GameObject("CrepeDonutEyes");
            crepeDonutEyes.transform.parent = crepeDonut.transform;
            crepeDonutEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("crepe_donut_eyes").sharedMesh;
            crepeDonutEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

            return new GameObject[] { crepeShellCuberry, crepeShellPogofruit, crepeShellMango, crepeShellCream, crepeDonut };
        }
    }
}
