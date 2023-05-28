using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.Attackers;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gastropods.HarmonyPatches;

namespace Gastropods.Data.Gastropods
{
    internal class Prickly
    {
        private static IdentifiableType pricklyGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#60a44f"), LoadHex("#8B0000"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#8B0000"), LoadHex("#60a44f") };

        public static void Initialize()
        {
            pricklyGastropod = GastroUtility.CreateIdentifiable("Prickly", false, false, true, false, false, LoadHex("#60a44f"));
            Gastro.DO_SOMETHING_GASTROPODS.Add(pricklyGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        GastroUtility.CreateGastropod("Prickly", false, true, true, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodPrickly")), pricklyGastropod, gastroPalette, gastroShellPalette,
                            GBundle.models.LoadFromObject<MeshFilter>("prickly_eyes").sharedMesh,
                            GBundle.models.LoadFromObject<MeshFilter>("prickly_shell").sharedMesh, null, CreateAccessories())
                            .Item1.AddComponent<SpineAttacker>();
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(pricklyGastropod, "Prickly",
                "Would you have thought this was a plant?",

                "Prickly Gastropods are one of the <b>eigth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "This specific type of gastropod is defensive and that includes hurting you. " +
                "They'll damage slimes (not largos) and players with their spines if they get too close. This could be an issue when feeding them to other slimes or trying to handle them. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "This gastropod type cannot reproduce and has to be found spawning on its own.\n" +
                "This means it is incapable of being mass produced, have fun saving them up!"
            );
            PatchPediaDirector.AddIdentifiablePage("Prickly", 2,
                "Prickly does not have a confidant. You could say their confidant is their spines or top flower though if you'd like!\n" +
                "Another known fact about Prickly gastropods is that all of them are of the female gender."
            );
            PatchPediaDirector.AddIdentifiablePage("Prickly", 3,
                "This is a type of gastropod that doesn't have kings & queens.\n" +
                "They do spawn on their own and require to be found. They cannot reproduce whatsoever.\n" +
                "This gastropod type specifically spawns everywhere but Rainbow Fields."
            );
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
