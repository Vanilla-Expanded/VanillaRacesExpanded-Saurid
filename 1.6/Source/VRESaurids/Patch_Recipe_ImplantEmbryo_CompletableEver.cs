﻿using RimWorld;
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
    [HarmonyPatch(typeof(Recipe_ImplantEmbryo), "CompletableEver")]
    public static class Patch_Recipe_ImplantEmbryo_CompletableEver
	{
		[HarmonyPostfix]
		public static void Postfix(Recipe_ImplantEmbryo __instance, Pawn surgeryTarget, ref bool __result)
		{
            if (__result && (surgeryTarget.genes?.HasGene(VRESauridsDefOf.VRESaurids_Oviparous) ?? false))
            {
				__result = false;
				return;
            }
		}
	}
}
