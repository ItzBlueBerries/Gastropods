using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class DestroyAfterHours : MonoBehaviour
    {
        private TimeDirector timeDir;
        private double time;

        public float hoursTillDestroy = 1.5f;

        void Start()
        {
            timeDir = SceneContext.Instance.TimeDirector;
            time = timeDir.HoursFromNowOrStart(hoursTillDestroy);
        }

        void Update()
        {
            if (timeDir.HasReached(time))
            {
                SRBehaviour.SpawnAndPlayFX(Get<SlimeDefinition>("Phosphor").prefab.GetComponent<DestroyOutsideHoursOfDay>().destroyFX, transform.position, transform.rotation);
                Destroyer.DestroyActor(gameObject, "DestroyAfterHours.Update");
            }
        }
    }
}
