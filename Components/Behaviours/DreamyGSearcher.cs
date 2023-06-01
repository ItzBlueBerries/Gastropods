using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Behaviours
{
    internal class DreamyGSearcher : GastroBehaviour
    {
        private TimeDirector timeDir;
        private Transform target;
        private Rigidbody rigidBody;
        private List<GameObject> gameObjects = new List<GameObject>();
        private double delayTime;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            rigidBody = GetComponent<Rigidbody>();

            if (target == null && IsNighttime())
                FindGastropodInScene();
        }

        void Update()
        {
            if (target != null && !IsNighttime())
                target = null;
            if (target != null && IsNighttime() && delayTime != default && timeDir.HasReached(delayTime))
            {
                FindGastropodInScene();
                delayTime = default;
            }
        }

        void FixedUpdate() => MoveToGastropod();

        bool IsNighttime() => timeDir.CurrHourOrStart() >= 18 || timeDir.CurrHourOrStart() < 6;

        void SetDelayTime() => delayTime = timeDir.HoursFromNowOrStart(0.08f);

        void FindGastropodInScene()
        {
            gameObjects.Clear();
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj == gameObject)
                    continue;

                if (!obj.GetComponent<IdentifiableActor>())
                    continue;

                if (Gastro.IsGastropod(GetIdentType(obj)))
                    gameObjects.Add(obj);
            }
            if (gameObjects.Count > 0)
                target = gameObjects[new System.Random().Next(gameObjects.Count)].transform;
        }

        void MoveToGastropod()
        {
            if (target != null && delayTime == default)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget >= 3)
                    rigidBody.MovePosition(transform.position + direction * 7 * Time.deltaTime);
                else
                {
                    rigidBody.velocity = Vector3.zero;
                    SetDelayTime();
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigidBody.MoveRotation(Quaternion.Slerp(rigidBody.rotation, targetRotation, 7 * Time.deltaTime));
            }
        }
    }
}
