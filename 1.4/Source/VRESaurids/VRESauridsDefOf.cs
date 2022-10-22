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
    [DefOf]
    public static class VRESauridsDefOf
    {
        static VRESauridsDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(VRESauridsDefOf));
        }

        public static XenotypeDef VRESaurids_Saurid;

        public static GeneDef VRESaurids_ColdBlooded;
        public static GeneDef VRESaurids_Oviparous;
        public static GeneDef VRESaurids_Pheromones;
        public static GeneDef VRESaurids_SauridClaws;

        public static HediffDef VRESaurids_HypothermicSlowdown;
        public static HediffDef VRESaurids_HyperthermicSlowdown;
        public static HediffDef VRESaurids_EggFatigue;

        public static ThingDef VRESaurids_HumanEgg;
    }
}
