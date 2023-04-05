using Gastropods.Components;
using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Regions;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gastropods.Assist
{
    //public class GastroBuilder
    //{
    //    public virtual string gastroType { get; set; }
    //    public virtual bool isQueen { get; set; }
    //    public virtual Color gastroColor { get; set; }
    //    public virtual Sprite gastroIcon { get; set; }
    //    public virtual Color[] gastroPalette { get; set; }
    //    public virtual Color[] gastroShellPalette { get; set; }
    //    public virtual GameObject[] gastroAccessories { get; set; } = new GameObject[0];

    //    private IdentifiableType gastroIdent;
    //    private GameObject gastroPrefab;

    //    public IdentifiableType GetIdentifiable() => gastroIdent;
    //    public GameObject GetPrefab() => gastroPrefab;
    //    public (Material, Material, Material) GetMaterial(SlimeDefinition definition1, SlimeDefinition definition2)
    //    {
    //        Material gastroBodyMaterial = UnityEngine.Object.Instantiate(definition1.GetSlimeMat(0));
    //        gastroBodyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

    //        Material gastroShellMaterial = UnityEngine.Object.Instantiate(definition2.GetSlimeMat(0));
    //        gastroShellMaterial.SetSlimeColor(gastroShellPalette[0], gastroShellPalette[1], gastroShellPalette[2]);

    //        Material gastroEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
    //        gastroEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

    //        return (gastroBodyMaterial, gastroShellMaterial, gastroEyesMaterial);
    //    }

    //    public virtual void Initialize()
    //    {
    //        // gastroIdent = ScriptableObject.CreateInstance<IdentifiableType>();
    //        gastroIdent.hideFlags |= HideFlags.HideAndDontSave;
    //        if (isQueen)
    //            gastroIdent.name = gastroType + "QueenGastropod";
    //        else
    //            gastroIdent.name = gastroType + "Gastropod";
    //        gastroIdent.color = gastroColor;

    //        GastroEntry.GASTROPODS.Add(gastroIdent);

    //        if (isQueen)
    //            GastroEntry.QUEEN_GASTROPODS.Add(gastroIdent);
    //    }

    //    public virtual void Load(string sceneName) 
    //    {
    //        switch (sceneName)
    //        {
    //            case "GameCore":
    //                {
    //                    (Material, Material, Material) gastroMaterial = GetMaterial(Get<SlimeDefinition>("Pink"), Get<SlimeDefinition>("Pink"));
    //                    Material gastroCrownMaterial = null;

    //                    if (isQueen)
    //                    {
    //                        gastroCrownMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
    //                        gastroCrownMaterial.SetSlimeColor(Color.yellow, Color.red, Color.yellow);
    //                    }

    //                    if (isQueen)
    //                        gastroIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l." + gastroType.ToLower() + "_queen_gastropod", gastroType + " Queen Gastropod");
    //                    else
    //                        gastroIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l." + gastroType.ToLower() + "_gastropod", gastroType + " Gastropod");

    //                    if (isQueen)
    //                        gastroPrefab = new GameObject(gastroType.ToLower() + "QueenGastropod");
    //                    else
    //                        gastroPrefab = new GameObject(gastroType.ToLower() + "Gastropod");
    //                    gastroPrefab.Prefabitize();
    //                    gastroPrefab.hideFlags |= HideFlags.HideAndDontSave;
    //                    gastroIdent.prefab = gastroPrefab;
    //                    gastroIdent.icon = gastroIcon;

    //                    gastroPrefab.AddComponent<Rigidbody>();
    //                    gastroPrefab.AddComponent<RegionMember>();
    //                    // gastroPrefab.AddComponent<GastropodRandomMoveV3>();
    //                    // gastroPrefab.AddComponent<GastropodUpright>();
    //                    gastroPrefab.AddComponent<KeepUpright>();
    //                    gastroPrefab.AddComponent<CompanionController>();
    //                    gastroPrefab.AddComponent<IdentifiableActor>().identType = gastroIdent;

    //                    if (isQueen)
    //                        gastroPrefab.AddComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
    //                    else
    //                        gastroPrefab.AddComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

    //                    gastroPrefab.AddComponent<SphereCollider>();
    //                    gastroPrefab.layer = LayerMask.NameToLayer("Actor");

    //                    GameObject gastroParts = new GameObject("GastroParts");
    //                    gastroParts.transform.parent = gastroPrefab.transform;

    //                    GameObject gastropodBody = new GameObject("GastropodBody");
    //                    gastropodBody.transform.parent = gastroParts.transform;

    //                    if (isQueen)
    //                    {
    //                        gastropodBody.transform.localScale *= 0.6f;
    //                        gastropodBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_body").sharedMesh;
    //                    }
    //                    else
    //                    {
    //                        gastropodBody.transform.localScale *= 0.3f;
    //                        gastropodBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_body").sharedMesh;
    //                    }

    //                    gastropodBody.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item1;
    //                    // gastropodBody.AddComponent<Animator>().runtimeAnimatorController = GBundle.anims.LoadAsset("gastropod_body").Cast<RuntimeAnimatorController>();

    //                    GameObject gastropodShell = new GameObject("GastropodShell");
    //                    gastropodShell.transform.parent = gastropodBody.transform;

    //                    if (isQueen)
    //                    {
    //                        gastropodShell.transform.localScale *= 0.6f;
    //                        gastropodShell.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_shell").sharedMesh;
    //                    }
    //                    else
    //                    {
    //                        gastropodShell.transform.localScale *= 0.3f;
    //                        gastropodShell.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_shell").sharedMesh;
    //                    }

    //                    gastropodShell.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item2;

    //                    GameObject gastropodEyes = new GameObject("GastropodEyes");
    //                    gastropodEyes.transform.parent = gastropodBody.transform;

    //                    if (isQueen)
    //                    {
    //                        gastropodEyes.transform.localScale *= 0.6f;
    //                        gastropodEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_eyes").sharedMesh;
    //                    }
    //                    else
    //                    {
    //                        gastropodEyes.transform.localScale *= 0.3f;
    //                        gastropodEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_eyes").sharedMesh;
    //                    }

    //                    gastropodEyes.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item3;

    //                    if (isQueen)
    //                    {
    //                        GameObject gastropodQueenCrowns = new GameObject("GastropodCrowns");
    //                        gastropodQueenCrowns.transform.parent = gastropodBody.transform;
    //                        gastropodQueenCrowns.transform.localScale *= 0.6f;
    //                        gastropodQueenCrowns.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_crown").sharedMesh;
    //                        gastropodQueenCrowns.AddComponent<MeshRenderer>().sharedMaterial = gastroCrownMaterial;
    //                    }

    //                    GameObject gastroDeco = new GameObject("GastroDeco");
    //                    gastroDeco.transform.parent = gastroParts.transform;
    //                    if (gastroAccessories.Length > 0)
    //                    {
    //                        foreach (GameObject gameObject in gastroAccessories)
    //                        {
    //                            gameObject.transform.parent = gastroDeco.transform;
    //                            if (isQueen)
    //                                gameObject.transform.localScale *= 0.6f;
    //                            else
    //                                gameObject.transform.localScale *= 0.3f;
    //                        }
    //                    }
    //                    break;
    //                }
    //        }
    //    }
    //}

    public static class GastroUtility
    {
        public static IdentifiableType CreateIdentifiable(string gastroType, bool isQueen, bool isKing, Color gastroColor)
        {
            IdentifiableType gastroIdent = ScriptableObject.CreateInstance<IdentifiableType>();
            gastroIdent.hideFlags |= HideFlags.HideAndDontSave;
            if (isQueen)
                gastroIdent.name = gastroType + "QueenGastropod";
            else if (isKing && !isQueen)
                gastroIdent.name = gastroType + "KingGastropod";
            else
                gastroIdent.name = gastroType + "Gastropod";
            gastroIdent.IsAnimal = true;
            gastroIdent.foodGroup = Get<IdentifiableTypeGroup>("MeatGroup");
            gastroIdent.color = gastroColor;

            Gastro.GASTROPODS.Add(gastroIdent);

            if (isQueen)
                Gastro.QUEEN_GASTROPODS.Add(gastroIdent);
            else if (isKing && !isQueen)
                Gastro.KING_GASTROPODS.Add(gastroIdent);

            return gastroIdent;
        }

        public static (Material, Material, Material) CreateMaterials(SlimeDefinition definition1, SlimeDefinition definition2, Color[] gastroPalette, Color[] gastroShellPalette)
        {
            Material gastroBodyMaterial = UnityEngine.Object.Instantiate(definition1.GetSlimeMat(0));
            gastroBodyMaterial.SetSlimeColor(gastroPalette[0], gastroPalette[1], gastroPalette[2]);

            Material gastroShellMaterial = UnityEngine.Object.Instantiate(definition2.GetSlimeMat(0));
            gastroShellMaterial.SetSlimeColor(gastroShellPalette[0], gastroShellPalette[1], gastroShellPalette[2]);

            Material gastroEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            gastroEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            return (gastroBodyMaterial, gastroShellMaterial, gastroEyesMaterial);
        }

        public static (GameObject, (Material, Material, Material)) CreateGastropod(string gastroType, bool shouldGotoSuperior, bool hasCustomEyes, bool hasCustomShell, bool hasCustomBody, Sprite gastroIcon, IdentifiableType gastroIdent, Color[] gastroPalette, Color[] gastroShellPalette, [Optional] Mesh gastroEyes, [Optional] Mesh gastroShell, [Optional] Mesh gastroBody, [Optional] GameObject[] gastroAccessories)
        {
            (Material, Material, Material) gastroMaterial = CreateMaterials(Get<SlimeDefinition>("Pink"), Get<SlimeDefinition>("Pink"), gastroPalette, gastroShellPalette);
            gastroIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l." + gastroType.ToLower() + "_gastropod", gastroType + " Gastropod");

            GameObject gastroPrefab = new GameObject(gastroType.ToLower() + "Gastropod");
            gastroPrefab.Prefabitize();
            gastroPrefab.hideFlags |= HideFlags.HideAndDontSave;
            gastroIdent.prefab = gastroPrefab;
            gastroIdent.icon = gastroIcon;

            gastroPrefab.AddComponent<Rigidbody>();
            gastroPrefab.AddComponent<RegionMember>();
            gastroPrefab.AddComponent<KeepUpright>();

            if (shouldGotoSuperior)
            {
                gastroPrefab.AddComponent<GotoSuperior>();
                gastroPrefab.AddComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;
            }
            else
                gastroPrefab.AddComponent<Vacuumable>().size = Vacuumable.Size.NORMAL;

            gastroPrefab.AddComponent<IdentifiableActor>().identType = gastroIdent;
            gastroPrefab.AddComponent<SphereCollider>();
            gastroPrefab.layer = LayerMask.NameToLayer("ActorIgnorePlayer");
            PrefabUtils.CopyPrefab(Get<GameObject>("slimePink")).transform.FindChild("DelaunchTrigger(Clone)").transform.parent = gastroPrefab.transform;

            GameObject gastroParts = new GameObject("GastroParts");
            gastroParts.transform.parent = gastroPrefab.transform;

            GameObject gastropodBody = new GameObject("GastropodBody");
            gastropodBody.transform.parent = gastroParts.transform;
            gastropodBody.transform.localScale *= 0.3f;

            if (!hasCustomBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_body").sharedMesh;
            else if (hasCustomBody && gastroBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = gastroBody;

            gastropodBody.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item1;
            // gastropodBody.AddComponent<Animator>().runtimeAnimatorController = GBundle.anims.LoadAsset("gastropod_body").Cast<RuntimeAnimatorController>();

            GameObject gastropodShell = new GameObject("GastropodShell");
            gastropodShell.transform.parent = gastropodBody.transform;
            gastropodShell.transform.localScale *= 0.3f;

            if (!hasCustomShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_shell").sharedMesh;
            else if (hasCustomShell && gastroShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = gastroShell;

            gastropodShell.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item2;

            GameObject gastropodEyes = new GameObject("GastropodEyes");
            gastropodEyes.transform.parent = gastropodBody.transform;
            gastropodEyes.transform.localScale *= 0.3f;

            if (!hasCustomEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_eyes").sharedMesh;
            else if (hasCustomEyes && gastroEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = gastroEyes;

            gastropodEyes.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item3;

            GameObject gastroDeco = new GameObject("GastroDeco");
            gastroDeco.transform.parent = gastroParts.transform;
            if (gastroAccessories != null)
            {
                if (gastroAccessories.Length > 0)
                {
                    foreach (GameObject gameObject in gastroAccessories)
                    {
                        gameObject.transform.parent = gastroDeco.transform;
                        gameObject.transform.localScale *= 0.3f;
                    }
                }
            }
            return (gastroPrefab, gastroMaterial);
        }
    
        public static (GameObject, (Material, Material, Material)) CreateQueenGastropod(string gastroType, bool hasCustomEyes, bool hasCustomShell, bool hasCustomBody, Sprite gastroIcon, IdentifiableType gastroIdent, Il2CppSystem.Type fedVaccable, Il2CppSystem.Type reproduceOnNearby, Color[] gastroPalette, Color[] gastroShellPalette, [Optional] Mesh gastroEyes, [Optional] Mesh gastroShell, [Optional] Mesh gastroBody, [Optional] GameObject[] gastroAccessories)
        {
            (Material, Material, Material) gastroMaterial = CreateMaterials(Get<SlimeDefinition>("Pink"), Get<SlimeDefinition>("Pink"), gastroPalette, gastroShellPalette);
            gastroIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l." + gastroType.ToLower() + "_queen_gastropod", gastroType + " Queen Gastropod");

            GameObject gastroPrefab = new GameObject(gastroType.ToLower() + "QueenGastropod");
            gastroPrefab.Prefabitize();
            gastroPrefab.hideFlags |= HideFlags.HideAndDontSave;
            gastroIdent.prefab = gastroPrefab;
            gastroIdent.icon = gastroIcon;

            gastroPrefab.AddComponent<Rigidbody>();
            gastroPrefab.AddComponent<RegionMember>();
            gastroPrefab.AddComponent<KeepUpright>();
            gastroPrefab.AddComponent(fedVaccable);
            gastroPrefab.AddComponent(reproduceOnNearby);
            gastroPrefab.AddComponent<IdentifiableActor>().identType = gastroIdent;
            gastroPrefab.AddComponent<SphereCollider>();
            gastroPrefab.layer = LayerMask.NameToLayer("ActorIgnorePlayer");
            PrefabUtils.CopyPrefab(Get<GameObject>("slimePink")).transform.FindChild("DelaunchTrigger(Clone)").transform.parent = gastroPrefab.transform;

            GameObject gastroParts = new GameObject("GastroParts");
            gastroParts.transform.parent = gastroPrefab.transform;

            GameObject gastropodBody = new GameObject("GastropodBody");
            gastropodBody.transform.parent = gastroParts.transform;
            gastropodBody.transform.localScale *= 0.6f;

            if (!hasCustomBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_body").sharedMesh;
            else if (hasCustomBody && gastroBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = gastroBody;

            gastropodBody.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item1;
            // gastropodBody.AddComponent<Animator>().runtimeAnimatorController = GBundle.anims.LoadAsset("gastropod_body").Cast<RuntimeAnimatorController>();

            GameObject gastropodShell = new GameObject("GastropodShell");
            gastropodShell.transform.parent = gastropodBody.transform;
            gastropodShell.transform.localScale *= 0.6f;

            if (!hasCustomShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_shell").sharedMesh;
            else if (hasCustomShell && gastroShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = gastroShell;

            gastropodShell.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item2;

            GameObject gastropodEyes = new GameObject("GastropodEyes");
            gastropodEyes.transform.parent = gastropodBody.transform;
            gastropodEyes.transform.localScale *= 0.6f;

            if (!hasCustomEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_eyes").sharedMesh;
            else if (hasCustomEyes && gastroEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = gastroEyes;

            gastropodEyes.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item3;

            GameObject gastroDeco = new GameObject("GastroDeco");
            gastroDeco.transform.parent = gastroParts.transform;
            if (gastroAccessories != null)
            {
                if (gastroAccessories.Length > 0)
                {
                    foreach (GameObject gameObject in gastroAccessories)
                    {
                        gameObject.transform.parent = gastroDeco.transform;
                        gameObject.transform.localScale *= 0.6f;
                    }
                }
            }
            return (gastroPrefab, gastroMaterial);
        }

        public static (GameObject, (Material, Material, Material)) CreateKingGastropod(string gastroType, bool hasCustomEyes, bool hasCustomShell, bool hasCustomBody, Sprite gastroIcon, IdentifiableType gastroIdent, Color[] gastroPalette, Color[] gastroShellPalette, [Optional] Mesh gastroEyes, [Optional] Mesh gastroShell, [Optional] Mesh gastroBody, [Optional] GameObject[] gastroAccessories)
        {
            (Material, Material, Material) gastroMaterial = CreateMaterials(Get<SlimeDefinition>("Pink"), Get<SlimeDefinition>("Pink"), gastroPalette, gastroShellPalette);
            gastroIdent.localizedName = HarmonyPatches.LocalizationDirectorLoadTablePatch.AddTranslation("Actor", "l." + gastroType.ToLower() + "_king_gastropod", gastroType + " King Gastropod");

            GameObject gastroPrefab = new GameObject(gastroType.ToLower() + "KingGastropod");
            gastroPrefab.Prefabitize();
            gastroPrefab.hideFlags |= HideFlags.HideAndDontSave;
            gastroIdent.prefab = gastroPrefab;
            gastroIdent.icon = gastroIcon;

            gastroPrefab.AddComponent<Rigidbody>();
            gastroPrefab.AddComponent<RegionMember>();
            gastroPrefab.AddComponent<KeepUpright>();
            gastroPrefab.AddComponent<LazyVaccable>();
            gastroPrefab.AddComponent<IdentifiableActor>().identType = gastroIdent;
            gastroPrefab.AddComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
            gastroPrefab.AddComponent<SphereCollider>();
            gastroPrefab.layer = LayerMask.NameToLayer("ActorIgnorePlayer");
            PrefabUtils.CopyPrefab(Get<GameObject>("slimePink")).transform.FindChild("DelaunchTrigger(Clone)").transform.parent = gastroPrefab.transform;

            GameObject gastroParts = new GameObject("GastroParts");
            gastroParts.transform.parent = gastroPrefab.transform;

            GameObject gastropodBody = new GameObject("GastropodBody");
            gastropodBody.transform.parent = gastroParts.transform;
            gastropodBody.transform.localScale *= 0.7f;

            if (!hasCustomBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("gastropod_body").sharedMesh;
            else if (hasCustomBody && gastroBody)
                gastropodBody.AddComponent<MeshFilter>().sharedMesh = gastroBody;

            gastropodBody.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item1;
            // gastropodBody.AddComponent<Animator>().runtimeAnimatorController = GBundle.anims.LoadAsset("gastropod_body").Cast<RuntimeAnimatorController>();

            GameObject gastropodShell = new GameObject("GastropodShell");
            gastropodShell.transform.parent = gastropodBody.transform;
            gastropodShell.transform.localScale *= 0.7f;

            if (!hasCustomShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_shell").sharedMesh;
            else if (hasCustomShell && gastroShell)
                gastropodShell.AddComponent<MeshFilter>().sharedMesh = gastroShell;

            gastropodShell.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item2;

            GameObject gastropodEyes = new GameObject("GastropodEyes");
            gastropodEyes.transform.parent = gastropodBody.transform;
            gastropodEyes.transform.localScale *= 0.7f;

            if (!hasCustomEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("queen_gastropod_eyes").sharedMesh;
            else if (hasCustomEyes && gastroEyes)
                gastropodEyes.AddComponent<MeshFilter>().sharedMesh = gastroEyes;

            gastropodEyes.AddComponent<MeshRenderer>().sharedMaterial = gastroMaterial.Item3;

            GameObject gastroDeco = new GameObject("GastroDeco");
            gastroDeco.transform.parent = gastroParts.transform;
            if (gastroAccessories != null)
            {
                if (gastroAccessories.Length > 0)
                {
                    foreach (GameObject gameObject in gastroAccessories)
                    {
                        gameObject.transform.parent = gastroDeco.transform;
                        gameObject.transform.localScale *= 0.7f;
                    }
                }
            }
            return (gastroPrefab, gastroMaterial);
        }
    }
}
