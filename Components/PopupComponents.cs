using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components
{
    internal class PopupBase : MonoBehaviour
    {
        public static PediaDirector pediaDir;
        public static PediaEntry pediaEntry;

        public virtual void Start() => pediaDir = SRSingleton<SceneContext>.Instance.PediaDirector;
    }

    internal class VaccedPopup : PopupBase
    {
        bool hasShownPopup = false;

        void Update()
        {
            TryGetComponent(out Vacuumable vacuumable);
            if (vacuumable && vacuumable.isHeld() && !hasShownPopup)
            {
                pediaDir.MaybeShowPopup(pediaEntry);
                hasShownPopup = true;
            }
        }
    }
}

namespace Gastropods.Components.Popups
{
    internal class BrineVaccedPopup : VaccedPopup
    {
        public override void Start()
        {
            base.Start();
            pediaEntry = Get<IdentifiablePediaEntry>("Brine");
        }
    }

    internal class SunlightVaccedPopup : VaccedPopup
    {
        public override void Start()
        {
            base.Start();
            pediaEntry = Get<IdentifiablePediaEntry>("Sunlight");
        }
    }

    internal class PrimalVaccedPopup : VaccedPopup
    {
        public override void Start()
        {
            base.Start();
            pediaEntry = Get<IdentifiablePediaEntry>("Primal");
        }
    }

    internal class PowderVaccedPopup : VaccedPopup
    {
        public override void Start()
        {
            base.Start();
            pediaEntry = Get<IdentifiablePediaEntry>("Powder");
        }
    }
}