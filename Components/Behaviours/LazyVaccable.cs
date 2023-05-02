using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class LazyVaccable : MonoBehaviour
    {
        private TimeDirector timeDir;
        private double tillLazyTime;
        private double whileLazyTime;

        public float tillTime = UnityEngine.Random.Range(3, 6);
        public float lazyTime = UnityEngine.Random.Range(1, 3);
        public bool isLazy { get; private set; }

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            tillLazyTime = timeDir.HoursFromNowOrStart(tillTime);
        }

        void Update()
        {
            if (timeDir.HasReached(tillLazyTime) && whileLazyTime == default && !isLazy)
            {
                SetLaziness();
                whileLazyTime = timeDir.HoursFromNowOrStart(lazyTime);
                tillLazyTime = default;
                isLazy = true;
            }

            if (timeDir.HasReached(whileLazyTime) && tillLazyTime == default && isLazy)
            {
                StopLaziness();
                tillLazyTime = timeDir.HoursFromNowOrStart(lazyTime);
                whileLazyTime = default;
                isLazy = false;
            }
        }

        void SetLaziness()
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
        }

        void StopLaziness() => gameObject.AddComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
    }
}
