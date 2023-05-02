using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Gastropods.Components.Behaviours
{
    internal class FedVaccable : MonoBehaviour
    {
        private TimeDirector timeDir;
        private double time;

        public IdentifiableTypeGroup[] feedTypes;
        public bool hasBeenFed { get; private set; }

        public virtual void Start() => timeDir = SceneContext.Instance.TimeDirector;

        void Update()
        {
            if (timeDir.HasReached(time) && GetComponent<Vacuumable>())
            {
                if (hasBeenFed)
                {
                    if (GetComponent<Vacuumable>().isHeld())
                    {
                        try
                        {
                            GameObject.Find("PlayerCameraKCC/First Person Objects/Items").GetComponent<WeaponVacuum>().ExpelHeld();
                        }
                        catch (Exception)
                        {
                            foreach (WeaponVacuum weaponVacuum in FindObjectsOfType<WeaponVacuum>())
                                weaponVacuum.ExpelHeld();
                        }
                    }

                    Destroy(GetComponent<Vacuumable>());
                    time = default;
                    hasBeenFed = false;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (hasBeenFed)
                return;

            if (!collision.gameObject.GetComponent<Rigidbody>())
                return;

            if (!collision.gameObject.GetComponent<IdentifiableActor>())
                return;

            foreach (IdentifiableType gastropod in Gastro.GASTROPODS)
            {
                if (collision.gameObject.GetComponent<IdentifiableActor>().identType == gastropod)
                    return;
            }

            try
            {
                foreach (IdentifiableTypeGroup identifiableTypeGroup in feedTypes)
                {
                    if (!identifiableTypeGroup.IsMember(collision.gameObject.GetComponent<IdentifiableActor>().identType))
                        return;
                }
            }
            catch (Exception) { return; }

            if (time == default && !hasBeenFed)
            {
                //if (!IsPlayerClose())
                //    return;
                SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Pink").prefab.GetComponent<SlimeEat>().EatFavoriteFX, transform.position, transform.rotation);
                Destroyer.DestroyActor(collision.gameObject, "FedVaccable.OnCollisionEnter");

                gameObject.AddComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
                time = timeDir.HoursFromNowOrStart(3);
                hasBeenFed = true;
            }
        }

        //bool IsPlayerClose()
        //{
        //    float distance = Vector3.Distance(transform.position, SceneContext.Instance.Player.transform.position);
        //    if (distance <= 20)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
