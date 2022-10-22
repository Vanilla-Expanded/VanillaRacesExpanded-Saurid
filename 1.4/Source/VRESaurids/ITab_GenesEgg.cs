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
    public class ITab_GenesEgg : ITab_Genes
	{
		public override bool IsVisible => true;

		public GeneSet UnbornBabyHediffGeneset()
		{
			Thing selEgg = base.SelThing;
			if (selEgg.def == VRESauridsDefOf.VRESaurids_HumanEgg)
			{
				return selEgg.TryGetComp<Comp_HumanHatcher>().geneSet;
			}
			return null;
		}

		public override void FillTab()
		{
			GeneUIUtility.DrawGenesInfo(new Rect(0f, 20f, size.x, size.y - 20f), null, 550f, ref size, ref scrollPosition, UnbornBabyHediffGeneset());
		}
	}
}
