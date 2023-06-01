using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Attackers
{
    internal class HarpoonAttacker : GastroBehaviour
    {
        private TimeDirector timeDir; 
        private readonly Il2CppSystem.Collections.Generic.List<GameObject> objectCache = new Il2CppSystem.Collections.Generic.List<GameObject>();
        private int totalHarpoonsLeft;
        private double timeTillRefill;
        private double delayTime;
        private bool refilling;

        public GameObject harpoonPrefab;
        public int harpoonCount = 3;
        public float reloadTime = 1.5f;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            totalHarpoonsLeft = harpoonCount;

            if (harpoonPrefab == null)
                harpoonPrefab = GenerateDefaultPrefab();
        }

        void Update()
        {
            FindNearestSlime();

            if (objectCache.Count > 0)
                ShootHarpoon(objectCache[new System.Random().Next(0, objectCache.Count)]);

            if (totalHarpoonsLeft == default && !refilling && timeTillRefill == default)
            {
                DeactivateShellHarpoon();
                timeTillRefill = timeDir.HoursFromNowOrStart(reloadTime);
                refilling = true;
            }

            if (timeDir.HasReached(timeTillRefill) && refilling && timeTillRefill != default)
            {
                ActivateShellHarpoon();
                totalHarpoonsLeft = harpoonCount;
                timeTillRefill = default;
                refilling = false;
            }

            if (delayTime != default && timeDir.HasReached(delayTime))
                delayTime = default;
        }

        public int CheckHarpoonCount() => totalHarpoonsLeft;

        void FindNearestSlime() { objectCache.Clear(); CellDirector.GetSlimes(GetComponent<RegionMember>(), objectCache); }

        void ActivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(true);

        void DeactivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(false);

        void SetDelayTime() => delayTime = timeDir.HoursFromNowOrStart(0.01f);

        public void ShootHarpoon(GameObject obj)
        {
            if (refilling)
                return;

            if (delayTime != default)
                return;

            if (Get<IdentifiableTypeGroup>("LargoGroup").IsMember(GetIdentType(obj)))
                return;

            if (!(Vector3.Distance(transform.position, obj.transform.position) <= 8))
                return;

            GameObject instantiatedHarpoon = Instantiate(harpoonPrefab, transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.transform.position, Quaternion.identity);
            instantiatedHarpoon.transform.LookAt(obj.transform);

            Vector3 direction = (obj.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 10 * Time.deltaTime));
            instantiatedHarpoon.GetComponent<Rigidbody>().velocity = direction * 20;
            instantiatedHarpoon.GetComponent<Rigidbody>().MovePosition(obj.transform.position);

            Destroy(instantiatedHarpoon, 1);
            totalHarpoonsLeft -= 1;
            SetDelayTime();
        }

        GameObject GenerateDefaultPrefab()
        {
            Material harpoonMaterial = Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            harpoonMaterial.SetSlimeColor(Color.white, Color.grey, Color.white);

            GameObject prefab = new GameObject("Harpoon");
            prefab.AddComponent<Rigidbody>();
            prefab.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("confidant_harpoon").sharedMesh;
            prefab.AddComponent<MeshRenderer>().sharedMaterial = harpoonMaterial;
            prefab.AddComponent<BoxCollider>();
            prefab.AddComponent<KillSlimeOnTouch>();

            return prefab;
        }
    }
}
