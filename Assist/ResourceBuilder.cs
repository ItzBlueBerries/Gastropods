using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Assist
{
    public static class ResourceUtility
    {
        public static IdentifiableType CreateIdentifiable(string resourceName, bool isCommon, bool isRare, Color resourceColor)
        {
            IdentifiableType resourceIdent = ScriptableObject.CreateInstance<IdentifiableType>();

            resourceIdent.hideFlags |= HideFlags.HideAndDontSave;
            resourceIdent.name = resourceName.Replace(" ", "") + "Craft";
            resourceIdent.localizationSuffix = resourceIdent.name.ToLower().Replace(" ", "_") + "_craft";
            resourceIdent.color = resourceColor;

            Gastro.Resource.RESOURCES.Add(resourceIdent);

            if (isCommon)
                Gastro.Resource.COMMON_RESOURCES.Add(resourceIdent);
            if (isRare)
                Gastro.Resource.RARE_RESOURCES.Add(resourceIdent);

            return resourceIdent;
        }

        public static (GameObject, (Material, Material)) CreateLiquidResource(string resourceName, Sprite resourceIcon, IdentifiableType resourceIdent, Color resourceColor, Color[] liquidPalette, [Optional] GameObject[] resourceAccessories)
        {
            resourceIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", resourceIdent.localizationSuffix, resourceName);

            GameObject resourcePrefab = PrefabUtils.CopyPrefab(Get<GameObject>("craftPrimordyOil"));
            resourcePrefab.name = "craft" + resourceName.Replace(" ", "");
            resourcePrefab.hideFlags |= HideFlags.HideAndDontSave;
            resourceIdent.prefab = resourcePrefab;
            resourceIdent.icon = resourceIcon;

            resourcePrefab.GetComponent<IdentifiableActor>().identType = resourceIdent;

            Material baseMaterial = resourcePrefab.transform.Find("extractPump").GetComponent<MeshRenderer>().material;
            baseMaterial.mainTexture = LoadImage("Files.Textures.texSFJCraft");

            Material liquidMaterial = resourcePrefab.transform.Find("extractPump/pivot/liquid").GetComponent<MeshRenderer>().material;
            liquidMaterial.SetColor("_Col1", liquidPalette[0]);
            liquidMaterial.SetColor("_Col2", liquidPalette[1]);
            liquidMaterial.SetColor("_Col3", liquidPalette[2]);

            GameObject resourceDeco = new GameObject("ResourceDeco");
            resourceDeco.transform.parent = resourcePrefab.transform.Find("extractPump").transform;
            if (resourceAccessories != null)
            {
                if (resourceAccessories.Length > 0)
                {
                    foreach (GameObject gameObject in resourceAccessories)
                    {
                        gameObject.transform.parent = resourceDeco.transform;
                        gameObject.transform.localScale = resourcePrefab.transform.Find("extractPump").transform.localScale;
                    }
                }
            }
            return (resourcePrefab, (baseMaterial, liquidMaterial));
        }
    }
}
