using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace VRESaurids
{
    [HarmonyPatch(typeof(Hediff_Pregnant), "Tick")]
    public static class Patch_Hediff_Pregnant_Tick
	{
		[HarmonyPrefix]
		public static bool Prefix(Hediff_Pregnant __instance, Pawn ___father)
		{
            if ((Find.TickManager.TicksAbs % 200 == 0) && (__instance?.pawn?.genes?.HasGene(VRESauridsDefOf.VRESaurids_Oviparous) ?? false))
            {
				try
				{
					// Lay the egg.
					Thing egg = LayHumanEgg(__instance.pawn, ___father, __instance.geneSet);
					GenSpawn.Spawn(egg, __instance.pawn.Position, __instance.pawn.Map);
					// Make temporarily sterile.
					Hediff hediff = HediffMaker.MakeHediff(VRESauridsDefOf.VRESaurids_EggFatigue, __instance.pawn);
					__instance.pawn.health.AddHediff(hediff);
					// Tell the player.
					Find.LetterStack.ReceiveLetter("VRESaurids.EggLaidLabel".Translate(__instance.pawn.NameShortColored), "VRESaurids.EggLaidDesc".Translate(__instance.pawn.NameShortColored), LetterDefOf.PositiveEvent, (TargetInfo)__instance.pawn);
					__instance.pawn.health.RemoveHediff(__instance);
				}
				catch (Exception ex)
                {
					Log.Message("Error Suppressed: " + ex.ToString());
                }
				return false;
            }
			return true;
		}

		public static Thing LayHumanEgg(Pawn mother, Pawn father, GeneSet geneSet)
        {
			Thing thing;
			thing = ThingMaker.MakeThing(VRESauridsDefOf.VRESaurids_HumanEgg);
			Comp_HumanHatcher comp = thing.TryGetComp<Comp_HumanHatcher>();
			comp.mother = mother;
			if(mother?.genes?.Xenotype == father?.genes?.Xenotype)
            {
				comp.xenotype = mother.genes.Xenotype;
            }
            else
            {
				comp.xenotype = XenotypeDefOf.Baseliner;
            }
			if(father != null)
            {
				comp.father = father;
			}
			comp.geneSet = geneSet;

			return thing;
        }
	}
}
