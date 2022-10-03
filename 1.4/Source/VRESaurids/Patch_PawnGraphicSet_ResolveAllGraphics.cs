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
    [HarmonyPatch(typeof(PawnGraphicSet), "ResolveAllGraphics")]
    public static class Patch_PawnGraphicSet_ResolveAllGraphics
    {
        [HarmonyPostfix]
        public static void PostFix(PawnGraphicSet __instance)
        {
            Pawn pawn = __instance.pawn;
            if (pawn.RaceProps.Humanlike && pawn.genes.HasGene(VRESauridsDefOf.VRESaurids_ScaleSkin))
            {
                __instance.furCoveredGraphic = GraphicDatabase.Get<Graphic_Multi>(pawn.story.furDef.GetFurBodyGraphicPath(pawn), ShaderUtility.GetSkinShader(pawn.story.SkinColorOverriden), Vector2.one, pawn.story.SkinColor);
            }
        }
    }
}
