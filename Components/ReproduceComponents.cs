using Gastropods.Components.Behaviours;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastropods.Components.ReproduceOnNearbys
{
    internal class BrineReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("BrineGastropod").prefab;
        }
    }

    internal class SunlightReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("SunlightGastropod").prefab;
        }
    }

    internal class PrimalReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("PrimalGastropod").prefab;
        }
    }

    internal class PowderReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("PowderGastropod").prefab;
        }
    }

    internal class HareReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("HareGastropod").prefab;
            minSpawnCount = 2;
            maxSpawnCount = 4;
        }
    }

    internal class BubblyReproduce : ReproduceOnNearby
    {
        public override void Start()
        {
            base.Start();
            gastropodPrefab = Get<IdentifiableType>("BubblyGastropod").prefab;
        }
    }
}
