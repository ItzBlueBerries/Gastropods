using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.Behaviours
{
    internal class NoMoreCloudsAtDaylight : GastroBehaviour
    {
        private TimeDirector timeDir;

        void Start() => timeDir = SceneContext.Instance.TimeDirector;
        
        void Update()
        {
            if (!IsNighttime() && transform.Find("GastroParts/GastroDeco/DreamyClouds").gameObject.active)
                DisableClouds();
            if (IsNighttime() && !transform.Find("GastroParts/GastroDeco/DreamyClouds").gameObject.active)
                EnableClouds();
        }

        bool IsNighttime() => timeDir.CurrHourOrStart() >= 18 || timeDir.CurrHourOrStart() < 6;

        void DisableClouds() => transform.Find("GastroParts/GastroDeco/DreamyClouds").gameObject.SetActive(false);

        void EnableClouds() => transform.Find("GastroParts/GastroDeco/DreamyClouds").gameObject.SetActive(true);
    }
}
