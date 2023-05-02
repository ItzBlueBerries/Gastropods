using Gastropods.Assist;
using Gastropods.Components.Resource;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Data.Resources
{
    internal class SuperiorsFavJuice
    {
        private static IdentifiableType superiorsFavoriteJuice;
        private static Color[] liquidPalette = new Color[] { Color.cyan, Color.yellow, LoadHex("#964B00") };

        public static void Initialize() => superiorsFavoriteJuice = ResourceUtility.CreateIdentifiable("Superiors Favorite Juice", true, false, LoadHex("#964B00"));

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        ResourceUtility.CreateLiquidResource("Superiors Favorite Juice", null, superiorsFavoriteJuice, Color.magenta, liquidPalette, null)
                            .Item1.AddComponent<JuicyProduction>();
                        superiorsFavoriteJuice.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", superiorsFavoriteJuice.LocalizationSuffix, "Superior's Favorite Juice");
                        break;
                    }
            }
        }
    }
}
