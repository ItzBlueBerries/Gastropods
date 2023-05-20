using Gastropods.Assist;
using Gastropods.Components;
using Gastropods.Components.Behaviours;
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
    internal class Unidentified
    {
        private static IdentifiableType unidentifiedGastropod;
        private static (GameObject, (Material, Material, Material)) unidentifiedPrefab;
        private static Color[] gastroPalette = new Color[] { Color.white, Color.white, Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, Color.white, Color.white };

        public static void Initialize()
        {
            unidentifiedGastropod = GastroUtility.CreateIdentifiable("Unidentified", false, false, true, true, Color.white);
            Gastro.DO_SOMETHING_GASTROPODS.Add(unidentifiedGastropod);
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        unidentifiedPrefab = GastroUtility.CreateGastropod("Unidentified", false, true, false, false, CreateSprite(LoadImage("Files.Icons.Gastropods.iconGastropodUnidentified")), 
                            unidentifiedGastropod, gastroPalette, gastroShellPalette, null, null, null, CreateAccessories());
                        UnityEngine.Object.Destroy(unidentifiedPrefab.Item1.GetComponent<Vacuumable>());
                        unidentifiedPrefab.Item1.AddComponents(Il2CppType.Of<UnidentifiedTransform>(), Il2CppType.Of<RandomRigidMovement>(), Il2CppType.Of<DestroyAfterHours>(), Il2CppType.Of<ObjectTwirl>(), Il2CppType.Of<BounceActorOnCollision>());
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(unidentifiedGastropod, "Unidentified",
                "Was that a hard catch?",

                "Unidentified Gastropods are one of the <b>sixth gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "These gastropods are considered rare and have rare abilities that other gastropods do not have. " +
                "It moves a lot more than other gastropods making it hard to catch! Try to get it before it bounces away.. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "This gastropod type cannot reproduce and has to be found spawning on its own.\n" +
                "This means it is incapable of being mass produced, have fun trying to find and catch this type of gastropod!"
            );
            PatchPediaDirector.AddIdentifiablePage("Unidentified", 2,
                "Unidentified has rare abilities which are going to be explained here.\n" +
                "You cannot store this gastropod in your vacpack nor keep it for long. Although if you catch it by feeding it a craft, you'll get a reward in return.\n" +
                "While this may sound good, they move around a lot and do a lot of bouncing that can make it harder. Another thing is that they'll disappear after a little over an hour. " +
                "Including that when other things collide with it, they will be bounced away. This includes slimes, resources and all of the above that identify as actors.\n" +
                "What is the reward you may ask? Well the reward can either be a rare item (including moondew nectar) or a rare slime (such as gold or lucky)."
            );
            PatchPediaDirector.AddIdentifiablePage("Unidentified", 3,
                "This gastropod is unknown of having a confidant or not. The things that circle around it are presumed to be confidants although this cannot be confirmed to be true.\n" +
                "They could also be the cause of bouncing back other things that collide with the gastropod."
            );
            PatchPediaDirector.AddIdentifiablePage("Unidentified", 4,
                "This is a type of gastropod that doesn't have kings & queens.\n" +
                "They do spawn on their own and require to be found. They cannot reproduce whatsoever.\n" +
                "This gastropod type specifically spawns everywhere."
            );
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
