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
    [HarmonyPatch(typeof(HediffGiver_Hypothermia), "OnIntervalPassed")]
    public static class Patch_HediffGiver_Hypothermia_OnIntervalPassed
	{
		[HarmonyPrefix]
		public static bool Prefix(Pawn pawn, Hediff cause)
		{
            if (pawn?.genes?.HasGene(VRESauridsDefOf.VRESaurids_ColdBlooded) ?? false)
            {
				OnIntervalPassed(pawn, cause);
				return false;
            }
			return true;
		}

		public static void OnIntervalPassed(Pawn pawn, Hediff cause)
		{
			// Remove that fuckin hypothermia.
			Hediff hypothermia = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Hypothermia);
			if (hypothermia != null)
			{
				pawn.health.RemoveHediff(hypothermia);
			}

			// Hypothermic Shutdown
			float ambientTemperature = pawn.AmbientTemperature;
			FloatRange floatRange = pawn.ComfortableTemperatureRange();
			FloatRange floatRange2 = pawn.SafeTemperatureRange();
			HediffSet hediffSet = pawn.health.hediffSet;
			HediffDef hediffDef = VRESauridsDefOf.VRESaurids_HypothermicSlowdown;
			Hediff firstHediffOfDef = hediffSet.GetFirstHediffOfDef(hediffDef);
			if (ambientTemperature < floatRange2.min)
			{
				float a = Mathf.Abs(ambientTemperature - floatRange2.min) * 6.45E-05f;
				a = Mathf.Max(a, 0.00150f);
				HealthUtility.AdjustSeverity(pawn, hediffDef, a);
				if (pawn.Dead)
				{
					return;
				}
			}
			if (firstHediffOfDef == null)
			{
				return;
			}
			if (ambientTemperature > floatRange.min)
			{
				float value = firstHediffOfDef.Severity * 0.027f;
				value = Mathf.Clamp(value, 0.0015f, 0.015f);
				firstHediffOfDef.Severity -= value;
			}
			else if (pawn.RaceProps.FleshType != FleshTypeDefOf.Insectoid && ambientTemperature < 0f && firstHediffOfDef.Severity > 0.37f)
			{
				float num = 0.025f * firstHediffOfDef.Severity;
				if (Rand.Value < num && pawn.RaceProps.body.AllPartsVulnerableToFrostbite.Where((BodyPartRecord x) => !hediffSet.PartIsMissing(x)).TryRandomElementByWeight((BodyPartRecord x) => x.def.frostbiteVulnerability, out var result))
				{
					int num2 = Mathf.CeilToInt((float)result.def.hitPoints * 0.5f);
					DamageInfo dinfo = new DamageInfo(DamageDefOf.Frostbite, num2, 0f, -1f, null, result);
					pawn.TakeDamage(dinfo);
				}
			}
			// Backup for removing hypothermia applied througgh other means.
		}
	}
}
