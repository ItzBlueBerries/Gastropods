using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Il2Cpp.DynamicBoneCollider;
using UnityEngine;

namespace Gastropods.Components
{
    internal class ConfidantController : MonoBehaviour
    {
        private TimeDirector timeDir;
        private GameObject slimeObject;
        private double waitTime;

        public GameObject confidantPrefab;
        public float searchRadius = 10;
        public float confidantSpeed = 3;
        public bool isQueen = false;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            waitTime = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));

            FindNearestSlime();
        }

        void Update()
        {
            if (slimeObject == null && timeDir.HasReached(waitTime))
                FindNearestSlime();
            if (slimeObject != null && timeDir.HasReached(waitTime))
            {
                if (isQueen)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject confidant = Instantiate(confidantPrefab);
                        confidant.GetComponent<Rigidbody>().MovePosition(confidant.transform.position + (slimeObject.transform.position - confidant.transform.position).normalized * confidantSpeed * Time.fixedDeltaTime);
                        confidant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(confidant.GetComponent<Rigidbody>().rotation, Quaternion.LookRotation(slimeObject.transform.position), 5 * Time.fixedDeltaTime));

                        Rigidbody rigidBody = slimeObject.GetComponent<Rigidbody>();
                        Vector3 direction = (slimeObject.transform.position - confidant.transform.position).normalized;
                        rigidBody.MovePosition(transform.position + direction * Time.fixedDeltaTime);
                    }
                    waitTime = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));
                    slimeObject = null;
                }
                else
                {
                    GameObject confidant = Instantiate(confidantPrefab);
                    confidant.GetComponent<Rigidbody>().MovePosition(confidant.transform.position + (slimeObject.transform.position - confidant.transform.position).normalized * confidantSpeed * Time.fixedDeltaTime);
                    confidant.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(confidant.GetComponent<Rigidbody>().rotation, Quaternion.LookRotation(slimeObject.transform.position), 5 * Time.fixedDeltaTime));

                    Rigidbody rigidBody = slimeObject.GetComponent<Rigidbody>();
                    Vector3 direction = (slimeObject.transform.position - confidant.transform.position).normalized;
                    rigidBody.MovePosition(transform.position + direction * Time.fixedDeltaTime);

                    waitTime = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));
                    slimeObject = null;
                }
            }
        }

        void FindNearestSlime()
        {
            foreach (GameObject gameObject in FindObjectsOfType<GameObject>())
            {
                if (gameObject.GetComponent<IdentifiableActor>().identType.Cast<SlimeDefinition>() && gameObject.GetComponent<Rigidbody>())
                {
                    float distance = Vector3.Distance(transform.position, gameObject.transform.position);
                    if (distance <= searchRadius)
                        slimeObject = gameObject; break;
                }
            }
        }
    }
}
