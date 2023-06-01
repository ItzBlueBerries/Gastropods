using Il2Cpp;
using Il2CppMonomiPark.SlimeRancher.Regions;
using Il2CppSystem.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Attackers
{
    internal class HarpoonAttackerV2 : GastroBehaviour
    {
        private TimeDirector timeDir;
        private readonly Il2CppSystem.Collections.Generic.List<GameObject> objectCache = new Il2CppSystem.Collections.Generic.List<GameObject>();
        private int totalHarpoonsLeft;
        private double timeTillRefill;
        private bool refilling;

        public GameObject harpoonPrefab;
        public int harpoonCount = 3;
        public float reloadTime = 1.5f;
        public int maxColliders = 6;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            totalHarpoonsLeft = harpoonCount;

            if (harpoonPrefab == null)
                harpoonPrefab = GenerateDefaultPrefab();
        }

        void Update()
        {
            ShootNearestSlime();

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
        }

        public int GetHarpoonCount() => totalHarpoonsLeft;

        private void ActivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(true);

        private void DeactivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(false);

        private void ShootNearestSlime()
        {
            if (refilling)
                return;
            (bool, Transform) nearestSlime = FindNearestSlime();
            if (nearestSlime.Item1)
                ShootHarpoon(nearestSlime.Item2);
        }

        private (bool, Transform) FindNearestSlime()
        {
            objectCache.Clear();
            CellDirector.GetSlimes(GetComponent<RegionMember>(), objectCache);

            return (
                objectCache.Il2CppAny((x) =>
                GetTypeGroup("BaseSlimeGroup").IsMember(GetIdentType(x.gameObject)) &&
                x.gameObject != null &&
                Vector3.Distance(x.gameObject.transform.position, transform.position) < 5),

                objectCache.Il2CppFind((x) =>
                GetTypeGroup("BaseSlimeGroup").IsMember(GetIdentType(x.gameObject)) &&
                x.gameObject != null &&
                Vector3.Distance(x.gameObject.transform.position, transform.position) < 5).transform
            );
        }

        public GameObject GenerateDefaultPrefab()
        {
            Material harpoonMaterial = Instantiate(Get<SlimeDefinition>("Pink").GetSlimeMat(0));
            harpoonMaterial.SetSlimeColor(Color.white, Color.grey, Color.white);

            GameObject prefab = new GameObject("Harpoon");
            prefab.AddComponent<Rigidbody>();
            prefab.AddComponent<MeshFilter>().sharedMesh = GBundle.models.LoadFromObject<MeshFilter>("confidant_harpoon").sharedMesh;
            prefab.AddComponent<MeshRenderer>().sharedMaterial = harpoonMaterial;
            prefab.AddComponent<BoxCollider>();
            prefab.AddComponent<HarpoonKillOnTouch>();

            return prefab;
        }

        private void ShootHarpoon(Transform transform)
        {
            if (transform.gameObject.GetComponent<IdentifiableActor>().identType == Get<IdentifiableType>("ToxinGastropod"))
                return;

            GameObject instantiatedHarpoon = Instantiate(harpoonPrefab, transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.transform.position, Quaternion.identity);
            instantiatedHarpoon.transform.LookAt(transform);

            Vector3 direction = (transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 10 * Time.deltaTime));
            instantiatedHarpoon.GetComponent<Rigidbody>().velocity = direction * 20;
            instantiatedHarpoon.GetComponent<Rigidbody>().MovePosition(transform.position);

            Destroy(instantiatedHarpoon, 1);
            totalHarpoonsLeft -= 1;
        }
    }
}
