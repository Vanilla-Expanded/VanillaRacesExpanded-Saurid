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
    public class Thought_PheromoneAttraction : Thought_SituationalSocial
	{
		public override float OpinionOffset()
		{
			return UtilSaurid.SameXenotype(pawn, OtherPawn()) ? (OtherPawn().genes.HasGene(VRESauridsDefOf.VRESaurids_Pheromones) ? 20 : 0) : 0;
		}
	}
}
