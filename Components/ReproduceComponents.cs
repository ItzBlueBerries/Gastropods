﻿using Il2Cpp;
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
}
