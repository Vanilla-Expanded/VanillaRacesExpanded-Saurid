using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace VRESaurids
{
    public class CompProperties_HumanHatcher : CompProperties
    {
        public CompProperties_HumanHatcher()
        {
            compClass = typeof(Comp_HumanHatcher);
        }

        public float daysToHatch = 18f;
    }
}
