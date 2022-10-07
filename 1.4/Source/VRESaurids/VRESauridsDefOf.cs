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
    }
}
