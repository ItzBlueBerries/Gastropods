using Gastropods;
using Il2Cpp;
using System;
using System.Linq;
using System.Reflection;

#nullable disable
internal class Utility
{
    public static class PrefabUtils
    {
        public static Transform DisabledParent;
        static PrefabUtils()
        {
            DisabledParent = new GameObject("DeactivedObject").transform;
            DisabledParent.gameObject.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(DisabledParent.gameObject);
            DisabledParent.gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        public static GameObject CopyPrefab(GameObject prefab)
        {
            var newG = UnityEngine.Object.Instantiate(prefab, DisabledParent);
            return newG;
        }
    }

    public static class Gastro
    {
        public static class Items
        {
            internal static HashSet<IdentifiableType> COMMON_ITEMS = new HashSet<IdentifiableType>();
            internal static HashSet<IdentifiableType> RARE_ITEMS = new HashSet<IdentifiableType>();
        }

        public static class Pedia
        {
            internal static HashSet<PediaEntry> GASTROPOD_ENTRIES = new HashSet<PediaEntry>();
        }

        internal static HashSet<IdentifiableType> GASTROPODS = new HashSet<IdentifiableType>();
        internal static HashSet<IdentifiableType> QUEEN_GASTROPODS = new HashSet<IdentifiableType>();
        internal static HashSet<IdentifiableType> KING_GASTROPODS = new HashSet<IdentifiableType>();

        internal static HashSet<IdentifiableType> DEFENSIVE_GASTROPODS = new HashSet<IdentifiableType>();
        internal static HashSet<IdentifiableType> RARE_GASTROPODS = new HashSet<IdentifiableType>();
        internal static HashSet<IdentifiableType> DO_SOMETHING_GASTROPODS = new HashSet<IdentifiableType>();

        public static bool IsGastropod(IdentifiableType ident)
        {
            if (GASTROPODS.Contains(ident))
                return true;
            return false;
        }

        public static bool IsQueenGastropod(IdentifiableType ident)
        {
            if (QUEEN_GASTROPODS.Contains(ident))
                return true;
            return false;
        }

        public static bool IsKingGastropod(IdentifiableType ident)
        {
            if (KING_GASTROPODS.Contains(ident))
                return true;
            return false;
        }

        public static bool IsDefensiveGastropod(IdentifiableType ident)
        {
            if (DEFENSIVE_GASTROPODS.Contains(ident))
                return true;
            return false;
        }

        public static bool IsRareGastropod(IdentifiableType ident)
        {
            if (RARE_GASTROPODS.Contains(ident))
                return true;
            return false;
        }

        public static bool IsDoSomethingGastropod(IdentifiableType ident)
        {
            if (DO_SOMETHING_GASTROPODS.Contains(ident))
                return true;
            return false;
        }
    }

    public static Texture2D LoadImage(string filename)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(executingAssembly.GetName().Name + "." + filename + ".png");
        byte[] array = new byte[manifestResourceStream.Length];
        manifestResourceStream.Read(array, 0, array.Length);
        Texture2D texture2D = new Texture2D(1, 1);
        ImageConversion.LoadImage(texture2D, array);
        texture2D.filterMode = FilterMode.Bilinear;
        return texture2D;
    }

    public static Sprite CreateSprite(Texture2D texture) => Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f), 1f);

    public static class Spawner
    {
        public static void ToSpawn(string name) => SRBehaviour.InstantiateActor(Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(x => x.name == name), SRSingleton<SceneContext>.Instance.RegionRegistry.CurrentSceneGroup, SRSingleton<SceneContext>.Instance.Player.transform.position, Quaternion.identity);
    }

    public static T Get<T>(string name) where T : UnityEngine.Object
    {
        return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(found => found.name.Equals(name));
    }

    public static Color LoadHex(string hexCode)
    {
        ColorUtility.TryParseHtmlString(hexCode, out var returnedColor);
        return returnedColor;
    }
}

internal static class Extensions
{
    public static string GetPath(this GameObject obj)
    {
        string path = obj.name;
        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }
        return path;
    }


    public static SlimeAppearance.SlimeBone[] AddDefaultBones(this SlimeAppearance.SlimeBone[] slimeBones)
    {
        slimeBones = new SlimeAppearance.SlimeBone[]
        {
            SlimeAppearance.SlimeBone.JiggleBack,
            SlimeAppearance.SlimeBone.JiggleBottom,
            SlimeAppearance.SlimeBone.JiggleFront,
            SlimeAppearance.SlimeBone.JiggleLeft,
            SlimeAppearance.SlimeBone.JiggleRight,
            SlimeAppearance.SlimeBone.JiggleTop
        };

        return slimeBones;
    }

    public static SlimeAppearanceStructure Clone(this SlimeAppearanceStructure structure)
    {
        SlimeAppearanceStructure slimeAppearanceStructure = new SlimeAppearanceStructure(structure);
        slimeAppearanceStructure.Element.name = slimeAppearanceStructure.Element.name.Replace("(Clone)", string.Empty);
        return slimeAppearanceStructure;
    }

    public static Material GetSlimeMat(this SlimeDefinition slimeDefinition, int structureIndex)
    { return slimeDefinition.AppearancesDefault[0].Structures[structureIndex].DefaultMaterials[0]; }

    public static void SetSlimeColor(this Material material, Color color1, Color color2, Color color3)
    { material.SetColor("_TopColor", color1); material.SetColor("_MiddleColor", color2); material.SetColor("_BottomColor", color3); }

    public static void Prefabitize(this GameObject obj)
    { obj.transform.SetParent(PrefabUtils.DisabledParent, false); }

    public static T LoadFromObject<T>(this AssetBundle bundle, string name) where T : UnityEngine.Object
    { return bundle.LoadAsset(name).Cast<GameObject>().GetComponentInChildren<T>(); }
}
#nullable restore