using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;
using Verse.AI.Group;

namespace VRESaurids
{
    [HarmonyPatch(typeof(HediffGiver_Hypothermia), "OnIntervalPassed")]
    public class Patch_HediffGiver_Hypothermia_OnIntervalPassed
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
			float ambientTemperature = pawn.AmbientTemperature;
			FloatRange floatRange = pawn.ComfortableTemperatureRange();
			FloatRange floatRange2 = pawn.SafeTemperatureRange();
			Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(VRESauridsDefOf.VRESaurids_HyperthermicSlowdown);
			if (ambientTemperature > floatRange2.max)
			{
				float x = ambientTemperature - floatRange2.max;
				x = HediffGiver_Heat.TemperatureOverageAdjustmentCurve.Evaluate(x);
				float a = x * 6.45E-05f;
				a = Mathf.Max(a, 0.000375f);
				HealthUtility.AdjustSeverity(pawn, VRESauridsDefOf.VRESaurids_HyperthermicSlowdown, a);
			}
			else if (firstHediffOfDef != null && ambientTemperature < floatRange.max)
			{
				float value = firstHediffOfDef.Severity * 0.027f;
				value = Mathf.Clamp(value, 0.0015f, 0.015f);
				firstHediffOfDef.Severity -= value;
			}
			if (pawn.Dead || !pawn.IsNestedHashIntervalTick(60, 420))
			{
				return;
			}
			float num = floatRange.max + 150f;
			if (!(ambientTemperature > num))
			{
				return;
			}
			float x2 = ambientTemperature - num;
			x2 = HediffGiver_Heat.TemperatureOverageAdjustmentCurve.Evaluate(x2);
			int num2 = Mathf.Max(GenMath.RoundRandom(x2 * 0.06f), 3);
			DamageInfo dinfo = new DamageInfo(DamageDefOf.Burn, num2);
			dinfo.SetBodyRegion(BodyPartHeight.Undefined, BodyPartDepth.Outside);
			pawn.TakeDamage(dinfo);
			if (pawn.Faction == Faction.OfPlayer)
			{
				Find.TickManager.slower.SignalForceNormalSpeed();
				if (MessagesRepeatAvoider.MessageShowAllowed("PawnBeingBurned", 60f))
				{
					Messages.Message("MessagePawnBeingBurned".Translate(pawn.LabelShort, pawn), pawn, MessageTypeDefOf.ThreatSmall);
				}
			}
			pawn.GetLord()?.ReceiveMemo(HediffGiver_Heat.MemoPawnBurnedByAir);
		}
	}
}
