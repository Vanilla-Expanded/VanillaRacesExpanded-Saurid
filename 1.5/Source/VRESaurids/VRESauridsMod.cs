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
        public static VRESauridsSettings settings;

        public VRESauridsMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<VRESauridsSettings>();

            Harmony harmony = new Harmony("VanillaRacesExpanded.Saurids.RimWorld");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "Vanilla Races Expanded - Saurids";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.CheckboxLabeled("Display Cold-Blooded Warnings", ref settings.displayColdBloodedWarnings, "Cold-Blooded warnings are the ones that show up when your Saurid is about to kick the bucket because they put a toe in the freezer. There's no other warning until it's too late to save them so good luck with that.");
            listing.End();
        }
    }

    public class VRESauridsSettings : ModSettings
    {
        public bool displayColdBloodedWarnings = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref displayColdBloodedWarnings, "displayColdBloodedWarnings");
        }
    }
}
