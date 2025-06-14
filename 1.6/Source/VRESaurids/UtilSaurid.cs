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
    public static class UtilSaurid
    {
        public static bool SameXenotype(Pawn pawn, Pawn other)
        {
            if (pawn?.genes?.Xenotype == other?.genes?.Xenotype)
            {
                return true;
            }
            return false;
        }
    }
}
