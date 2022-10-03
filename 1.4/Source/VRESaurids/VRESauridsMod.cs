using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace VRESaurids
{
    public class VRESauridsMod : Mod
    {
        public static VRESauridsMod mod;

        public VRESauridsMod(ModContentPack content) : base(content)
        {
            mod = this;

            Harmony harmony = new Harmony("VanillaRacesExpanded.Saurids.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
