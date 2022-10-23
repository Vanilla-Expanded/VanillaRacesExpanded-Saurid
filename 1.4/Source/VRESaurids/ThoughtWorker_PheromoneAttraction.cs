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
    public class ThoughtWorker_PheromoneAttraction : ThoughtWorker
	{
		public override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
		{
			if (!UtilSaurid.SameXenotype(p, otherPawn) || !RelationsUtility.PawnsKnowEachOther(p, otherPawn))
			{
				return false;
			}
			if (otherPawn.genes.HasGene(VRESauridsDefOf.VRESaurids_Pheromones))
			{
				return ThoughtState.ActiveAtStage(1);
			}
			return false;
		}
	}
}
