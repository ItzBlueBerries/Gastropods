﻿using Il2Cpp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;

namespace Gastropods.Components
{
    internal class HungryAttacker : MonoBehaviour
    {
        private TimeDirector timeDir;
        private Rigidbody gastroBody;
        private List<GameObject> eatenObjects = new List<GameObject>();
        private Transform target;
        private double fullTime;
        private double searchTime;
        private int amountLeft;
        private bool isFull;
        private bool eatenTarget;

        public float moveSpeed = 5;
        public float rotationSpeed = 5;
        public float timeWhileFull = 2.5f;
        public float searchRadius;
        public int amountTillFull = 3;
        public int timeTillSearch = UnityEngine.Random.Range(1, 3);
        public bool isNotSuperior = false;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            gastroBody = GetComponent<Rigidbody>();
            searchTime = timeDir.HoursFromNowOrStart(timeTillSearch);

            if (isNotSuperior)
                amountTillFull = 6;
            amountLeft = amountTillFull;

            if (searchRadius == default)
                searchRadius = UnityEngine.Random.Range(20, 60);

            if (target == null)
                FindFoodInScene();
        }

        void Update()
        {
            if (timeDir.HasReached(searchTime) && target == null)
                FindFoodInScene(); searchTime = timeDir.HoursFromNowOrStart(UnityEngine.Random.Range(1, 3));

            if (timeDir.HasReached(fullTime) && isFull && fullTime != default)
            {
                amountLeft = amountTillFull;
                fullTime = default;
                isFull = false;
            }

            if (amountLeft == default && !isFull && fullTime == default)
            {
                fullTime = timeDir.HoursFromNowOrStart(timeWhileFull);
                isFull = true;
            }
        }

        void FixedUpdate() => MoveToFood();

        IEnumerator SetEatenTarget()
        {
            eatenTarget = true;
            yield return new WaitForSeconds(1.0f);
            eatenTarget = false;
        }

        void FindFoodInScene()
        {
            foreach (GameObject gameObject in FindObjectsOfType<GameObject>())
            {
                if (Get<IdentifiableTypeGroup>("MeatGroup").IsMember(gameObject.GetComponent<IdentifiableActor>().identType) || Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(gameObject.GetComponent<IdentifiableActor>().identType))
                {
                    float distance = Vector3.Distance(transform.position, gameObject.transform.position);
                    if (distance <= searchRadius)
                        target = gameObject.transform; break;
                }
            }
        }

        void MoveToFood()
        {
            if (target != null)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                gastroBody.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
                gastroBody.MoveRotation(Quaternion.Slerp(gastroBody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
            }
        }

        void EatFoodTarget(GameObject obj)
        {
            if (Get<IdentifiableTypeGroup>("MeatGroup").IsMember(gameObject.GetComponent<IdentifiableActor>().identType) || Get<IdentifiableTypeGroup>("BaseSlimeGroup").IsMember(obj.GetComponent<IdentifiableActor>().identType))
            {
                Vector3 direction = (obj.transform.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 10 * Time.deltaTime));
                SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFX, transform.position, transform.rotation);
                Destroyer.DestroyActor(obj, "HungryAttacker.OnCollisionEnter");
                amountLeft -= 1;

                eatenObjects.Add(obj);
                StartCoroutine("SetEatenTarget");
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (isFull)
                return;

            GameObject obj = collision.gameObject;

            if (eatenObjects.Contains(obj))
                return;

            if (eatenTarget)
                return;

            if (!obj.GetComponent<Rigidbody>())
                return;

            if (!obj.GetComponent<IdentifiableActor>())
                return;

            foreach (IdentifiableType gastropod in GastroEntry.GASTROPODS)
            {
                if (!obj.GetComponent<IdentifiableActor>().identType == gastropod)
                    return;
            }

            EatFoodTarget(obj);
        }
    }
}
