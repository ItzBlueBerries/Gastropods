using Gastropods.Components;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Data.Confidants
{
    internal class Fish
    {
        public static GameObject GeneratePrefab()
        {
            Material fishMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            fishMaterial.SetSlimeColor(Color.cyan, Color.blue, Color.white);

            Material blackEyesMaterial = UnityEngine.Object.Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            blackEyesMaterial.SetSlimeColor(Color.black, Color.black, Color.black);

            GameObject confidantPrefab = new GameObject("FishConfidant");
            confidantPrefab.Prefabitize();
            confidantPrefab.AddComponent<Rigidbody>();
            confidantPrefab.AddComponent<KeepUpright>();
            confidantPrefab.AddComponent<ConfidantAttack>();
            confidantPrefab.layer = LayerMask.NameToLayer("ActorIgnorePlayer");

            GameObject confidantBody = new GameObject("ConfidantBody");
            confidantBody.transform.parent = confidantPrefab.transform;
            confidantBody.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("confidant_fish_body").sharedMesh;
            confidantBody.AddComponent<MeshRenderer>().sharedMaterial = fishMaterial;

            GameObject confidantEyes = new GameObject("ConfidantEyes");
            confidantEyes.transform.parent = confidantBody.transform;
            confidantEyes.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("confidant_fish_eyes").sharedMesh;
            confidantEyes.AddComponent<MeshRenderer>().sharedMaterial = blackEyesMaterial;

            return confidantPrefab;
        }
    }
}
