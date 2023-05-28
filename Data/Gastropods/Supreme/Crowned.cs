using Gastropods.Assist;
using Gastropods.Components.Behaviours;
using Gastropods.Components.FedVaccables;
using Gastropods.Components.ReproduceOnNearbys;
using Gastropods.Components;
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
    internal class Crowned
    {
        private static IdentifiableType crownedGastropod;
        private static Color[] gastroPalette = new Color[] { LoadHex("#F6BE00"), LoadHex("#7851A9"), Color.white };
        private static Color[] gastroShellPalette = new Color[] { Color.white, LoadHex("#7851A9"), LoadHex("#F6BE00") };

        public static void Initialize() => crownedGastropod = GastroUtility.CreateIdentifiable("Crowned", false, false, false, false, true, LoadHex("#F6BE00"));

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        Material whiteEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
                        whiteEyesMaterial.SetSlimeColor(Color.white, Color.white, Color.white);

                        // too lazy to do fields and such
                        GameObject crownedPrefab = GastroUtility.CreateGastropod("Dreamy", false, false, false, false, null, crownedGastropod, gastroPalette, gastroShellPalette, null, null, null, CreateAccessories()).Item1;
                        crownedPrefab.AddComponents(Il2CppType.Of<DreamySuperiorCreation>(), Il2CppType.Of<DreamyGSearcher>(), Il2CppType.Of<NoMoreCloudsAtDaylight>());
                        UnityEngine.Object.Destroy(crownedPrefab.transform.Find("GastroParts/GastropodBody/GastropodShell").gameObject);
                        crownedPrefab.transform.Find("GastroParts/GastropodBody/GastropodEyes").GetComponent<MeshRenderer>().material = whiteEyesMaterial;
                        break;
                    }
            }
        }

        public static void CreatePedia()
        {
            GastroUtility.CreatePediaEntry(crownedGastropod, "Crowned",
                "Pure royalty. Higher than brine? EhhHhhHhhhhh",

                "Crowned Gastropods are one of the <b>first supreme gastropods</b> to set.. foot (?) on Rainbow Island.\n" +
                "While being the first supreme, they have power. Though this power is hidden for some mysterious reason which is still being researched on.. " +
                "...While the powers are still being discovered, once discovered this entry will be updated.. " +
                "You know what though? There is much more to discover with these guys and their other types that came along with them.",

                "For you to reproduce this specific gastropod type, they require <b>2 superior gastropods. Queen and King of their corresponding type.</b>\n" +
                "First you must obtain a Queen & King of their typing <i>(ex: Brine King Gastropod)</i>. Afterwards, place them somewhere nearby to eachother. " +
                "Every few hours, the queen will search for a king to reproduce with as long as they're nearby. Once they find their king, they will reproduce within a certain interval!\n" +
                "This is how you get more gastropods on your ranch, it doesn't take long for them to mass produce so make sure to empty them out when you need to!"
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 2,
                "<b>    Origin   </b>\n" +
                "Only Supreme's get this kind of descriptive addition to their pedia entries, which will be explained here.\n" +
                "Dreamy may have took a while to arrive on Rainbow Island but they were had some of the best abilities. " +
                "Technically, Dreamy can be found on its own as <b>their superiors do not actually exist</b>. How're they reproduced then? " +
                "Dreamy has the power to <b>create their own superiors</b>, though without the best visual qualities. At randomized times (long intervals) they'll create <b>one of them</b> in order to feel company but also give them the power of superiors.\n" +
                "Now that there is information on them being powerful and what they do for the ability of reproduction with their typing, what about the Supreme part? " +
                "Well Dreamy was technically going to be a supreme member but has denied of this. As an almost and worthy of being a supreme member, this is added to the entry."
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 3,
                "Another part of the Dreamy Gastropod is their confidant. Dreamy has a confidant who is a Star.\n" +
                "This confidant protects them from potential threats when they can. " +
                "This can make them more difficult to reproduce and feed to slimes but you'll have to find a workaround.\n" +
                "Dreamy is also a powerful one, defensive of course and will not allow slimes to touch them as long as their clouds are visible during the night.\n" +
                "Another known fact of the Dreamy Gastropod is that they love wondering around. They'll search for nearby gastropods for fun and is more active than other gastropods. " +
                "Once daylight hits, they'll stop and possibly return to their superiors. Overall, Dreamy is a handful."
            );
            PatchPediaDirector.AddIdentifiablePage("Dreamy", 4,
                "This is a type of gastropod that'll somewhat have kings & queens.\n" +
                "They do spawn on their own and could be reproduced by a superior. Superiors do not spawn on their own.\n" +
                "This gastropod type specifically spawns everywhere at around nighttime."
            );
        }


        private static GameObject[] CreateAccessories()
        {
            Material dreamyMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            dreamyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material starMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            starMaterial.SetSlimeColor(Color.yellow, Color.yellow, Color.yellow);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            if (!isSuperior)
            {
                GameObject dreamyClouds;
                GameObject dreamyCloudsStars;
                GameObject dreamyStar;
                GameObject dreamyStarEyes;

                dreamyClouds = new GameObject("DreamyClouds");
                dreamyClouds.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_clouds").sharedMesh;
                dreamyClouds.AddComponent<MeshRenderer>().sharedMaterial = dreamyMaterial;
                dreamyClouds.AddComponent<ObjectRotation>();
                dreamyClouds.AddComponent<BounceActorOnCollision>();

                dreamyCloudsStars = new GameObject("DreamyCloudsStars");
                dreamyCloudsStars.transform.parent = dreamyClouds.transform;
                dreamyCloudsStars.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_clouds_stars").sharedMesh;
                dreamyCloudsStars.AddComponent<MeshRenderer>().sharedMaterial = starMaterial;

                dreamyStar = new GameObject("DreamyStar");
                dreamyStar.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_star").sharedMesh;
                dreamyStar.AddComponent<MeshRenderer>().sharedMaterial = starMaterial;

                dreamyStarEyes = new GameObject("DreamyStarEyes");
                dreamyStarEyes.transform.parent = dreamyStar.transform;
                dreamyStarEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("dreamy_star_eyes").sharedMesh;
                dreamyStarEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

                return new GameObject[] { dreamyClouds, dreamyStar };
            }
            else if (isSuperior)
                return new GameObject[] { };

            return new GameObject[] { };
        }
    }
}
