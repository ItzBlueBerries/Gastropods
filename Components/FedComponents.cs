using Gastropods.Components.Behaviours;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.FedVaccables
{
    internal class BrineFedVaccable : FedVaccable
    { 
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup") };
        }
    }

    internal class SunlightFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("FruitGroup") };
        }
    }

    internal class PrimalFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("MeatGroup") };
        }
    }

    internal class PowderFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup"), Get<IdentifiableTypeGroup>("FruitGroup") };
        }
    }

    internal class HareFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup") };
        }
    }

    internal class BubblyFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("FruitGroup") };
        }
    }

    internal class TraditionalFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("VeggieGroup"), Get<IdentifiableTypeGroup>("FruitGroup"), Get<IdentifiableTypeGroup>("MeatGroup") };
        }
    }

    internal class DreamyFedVaccable : FedVaccable
    {
        public override void Start()
        {
            base.Start();
            feedTypes = new IdentifiableTypeGroup[] { Get<IdentifiableTypeGroup>("CraftGroup"), Get<IdentifiableTypeGroup>("FruitGroup") };
        }
    }
}
