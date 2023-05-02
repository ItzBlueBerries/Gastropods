using Il2Cpp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Attackers
{
    internal class HarpoonAttacker : MonoBehaviour
    {
        private TimeDirector timeDir;
        private List<GameObject> shotObjects = new List<GameObject>();
        private List<Collider> collidersDetected = new List<Collider>();
        private int totalHarpoonsLeft;
        private double timeTillRefill;
        private bool refilling;
        private bool shotTarget;

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

            InvokeRepeating("ShootNearestSlime", 3, 1);
        }

        void Update()
        {
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

        public int CheckHarpoonCount() => totalHarpoonsLeft;

        void ActivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(true);

        void DeactivateShellHarpoon() => transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.SetActive(false);

        IEnumerator SetShotTarget()
        {
            shotTarget = true;
            yield return new WaitForSeconds(1.0f);
            shotTarget = false;
        }

        public void ShootHarpoon(Collider collider)
        {
            if (collider.gameObject.GetComponent<IdentifiableActor>().identType == Get<IdentifiableType>("ToxinGastropod"))
                return;

            GameObject instantiatedHarpoon = Instantiate(harpoonPrefab, transform.Find("GastroParts/GastroDeco/ToxinShellHarpoon").gameObject.transform.position, Quaternion.identity);
            instantiatedHarpoon.transform.LookAt(collider.transform);

            Vector3 direction = (collider.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 10 * Time.deltaTime));
            instantiatedHarpoon.GetComponent<Rigidbody>().velocity = direction * 20;
            instantiatedHarpoon.GetComponent<Rigidbody>().MovePosition(collider.transform.position);

            Destroy(instantiatedHarpoon, 1);
            totalHarpoonsLeft -= 1;
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

        void ShootNearestSlime()
        {
            if (refilling)
                return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 5, LayerMask.GetMask("Actor"));
            foreach (Collider collider in colliders)
            {
                if (collidersDetected.Count >= maxColliders)
                    break;

                if (!collider.GetComponent<Rigidbody>())
                    continue;

                if (!collider.GetComponent<IdentifiableActor>())
                    continue;

                if (!Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(collider.GetComponent<IdentifiableActor>().identType))
                    continue;

                if (!collidersDetected.Contains(collider))
                    collidersDetected.Add(collider);
            }

            for (int i = collidersDetected.Count - 1; i >= 0; i--)
            {
                if (!colliders.Contains(collidersDetected[i]))
                    collidersDetected.RemoveAt(i);
            }

            foreach (Collider collider in collidersDetected)
            {
                if (shotObjects.Contains(collider.gameObject))
                    continue;

                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget <= 5 && totalHarpoonsLeft > 0 && !shotTarget)
                {
                    ShootHarpoon(collider);
                    shotObjects.Add(collider.gameObject);
                    StartCoroutine("SetShotTarget");
                }
            }
        }
    }
}
