﻿using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Attackers
{
    internal class SpineAttacker : GastroBehaviour
    {
        private TimeDirector timeDir;
        private readonly Il2CppSystem.Collections.Generic.List<GameObject> objectCache = new Il2CppSystem.Collections.Generic.List<GameObject>();
        private int totalSpinesLeft;
        // private int dividedSpineCount;
        private double timeTillRefill;
        private double delayTime;
        private bool refilling;

        public GameObject spinePrefab;
        public int spineCount = 36;
        public float reloadTime = 2;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            totalSpinesLeft = spineCount;
            // dividedSpineCount = spineCount / 3;

            if (spinePrefab == null)
                spinePrefab = GenerateDefaultPrefab();
        }

        void Update()
        {
            FindNearestSlime();

            if (objectCache.Count > 0)
                ShootSpine(objectCache[new System.Random().Next(0, objectCache.Count)]);

            if (Vector3.Distance(transform.position, SceneContext.Instance.Player.transform.position) <= 8)
                ShootSpine(SceneContext.Instance.Player.gameObject);

            if (totalSpinesLeft == default && !refilling && timeTillRefill == default)
            {
                DeactivateShellSpines();
                timeTillRefill = timeDir.HoursFromNowOrStart(reloadTime);
                refilling = true;
            }

            if (timeDir.HasReached(timeTillRefill) && refilling && timeTillRefill != default)
            {
                ActivateShellSpines();
                totalSpinesLeft = spineCount;
                timeTillRefill = default;
                refilling = false;
            }

            if (delayTime != default && timeDir.HasReached(delayTime))
                delayTime = default;
        }

        public int GetSpinesCount() => totalSpinesLeft;

        void FindNearestSlime() { objectCache.Clear(); CellDirector.GetSlimes(GetComponent<RegionMember>(), objectCache); }

        public void ActivateShellSpines() { transform.Find("GastroParts/GastroDeco/PricklyShellSpines").gameObject.SetActive(true); }

        public void DeactivateShellSpines() { transform.Find("GastroParts/GastroDeco/PricklyShellSpines").gameObject.SetActive(false); }

        void SetDelayTime() => delayTime = timeDir.HoursFromNowOrStart(0.01f);

        void ShootSpine(GameObject obj)
        {
            if (refilling)
                return;

            if (delayTime != default)
                return;

            if (Get<IdentifiableTypeGroup>("LargoGroup").IsMember(GetIdentType(obj)))
                return;

            if (!(Vector3.Distance(transform.position, obj.transform.position) <= 8))
                return;

            GameObject instantiatedSpine = Instantiate(spinePrefab, transform.position, Quaternion.identity);
            instantiatedSpine.transform.LookAt(obj.transform);

            Vector3 direction = (obj.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 10 * Time.deltaTime));
            instantiatedSpine.GetComponent<Rigidbody>().velocity = direction * 40;
            instantiatedSpine.GetComponent<Rigidbody>().MovePosition(obj.transform.position);

            Destroy(instantiatedSpine, 1);
            totalSpinesLeft -= 1;
            SetDelayTime();
        }

        GameObject GenerateDefaultPrefab()
        {
            Material spineMaterial = Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            spineMaterial.SetSlimeColor(LoadHex("#8B0000"), LoadHex("#8B0000"), LoadHex("#8B0000"));

            GameObject prefab = new GameObject("Spine");
            prefab.transform.localScale *= 0.3f;
            prefab.AddComponent<Rigidbody>();
            prefab.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("confidant_spine").sharedMesh;
            prefab.AddComponent<MeshRenderer>().sharedMaterial = spineMaterial;
            prefab.AddComponent<BoxCollider>();
            prefab.AddComponent<DamageSlimeOnTouch>();
            prefab.AddComponent<DamagePlayerOnTouch>();

            return prefab;
        }
    }
}
