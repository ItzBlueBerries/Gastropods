using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components
{
    internal class ShellRetreat : MonoBehaviour
    {
        private List<GameObject> nearbySlimes = new List<GameObject>();
        private bool isRetreating = false;

        public float retreatProbability = 0.5f;
        public float repelForce = 10;

        void Update()
        {
            if (nearbySlimes.Count == 0 && isRetreating)
                LooseDefense();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Rigidbody>())
                return;

            if (!other.gameObject.GetComponent<IdentifiableActor>())
                return;

            if (Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(other.gameObject.GetComponent<IdentifiableActor>().identType))
            {
                if (!other.gameObject.GetComponent<IdentifiableActor>().identType.Cast<SlimeDefinition>().Diet.MajorFoodIdentifiableTypeGroups.Contains(Get<IdentifiableTypeGroup>("MeatGroup")))
                    return;
                nearbySlimes.Add(other.gameObject);
                float rand = UnityEngine.Random.Range(0f, 1f);

                if (!isRetreating)
                    GetDefensive();
                if (isRetreating)
                {
                    Vector3 repelDir = (other.transform.position - transform.position).normalized;
                    other.gameObject.GetComponent<Rigidbody>().AddForce(repelDir * repelForce, ForceMode.Impulse);
                }
            }
        }

        void OnTriggerExit(Collider other)
        {            
            if (!other.gameObject.GetComponent<Rigidbody>())
                return;

            if (!other.gameObject.GetComponent<IdentifiableActor>())
                return;

            if (Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(other.gameObject.GetComponent<IdentifiableActor>().identType))
                nearbySlimes.Remove(other.gameObject);
        }

        void GetDefensive()
        {
            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().TransformFX, transform.parent.position, transform.parent.rotation);
            transform.parent.Find("GastroParts/GastropodBody/GastropodShell").gameObject.transform.parent = transform.parent.Find("GastroParts");
            transform.parent.Find("GastroParts/GastropodBody").gameObject.SetActive(false);
            isRetreating = true;
        }

        void LooseDefense()
        {
            SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().TransformFX, transform.parent.position, transform.parent.rotation);
            transform.parent.Find("GastroParts/GastropodShell").gameObject.transform.parent = transform.parent.Find("GastroParts/GastropodBody");
            transform.parent.Find("GastroParts/GastropodBody").gameObject.SetActive(true);
            isRetreating = false;
        }
    }
}
