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
    public class ScenPart_StartingHumanEggs : ScenPart
    {
        public int count = 1;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref count, "count", 1);
        }

        public override string Summary(Scenario scen)
        {
            return ScenSummaryList.SummaryWithList(scen, "PlayerStartsWith", ScenPart_StartingThing_Defined.PlayerStartWithIntro);
        }

        public override IEnumerable<string> GetSummaryListEntries(string tag)
        {
            if(tag == "PlayerStartsWith")
            {
                yield return VRESauridsDefOf.VRESaurids_HumanEgg.LabelCap + " x" + count;
            }
        }

        public override IEnumerable<Thing> PlayerStartingThings()
        {
            for (int i = 0; i < count; i++)
            {
                Thing thing = ThingMaker.MakeThing(VRESauridsDefOf.VRESaurids_HumanEgg);
                Comp_HumanHatcher comp = thing.TryGetComp<Comp_HumanHatcher>();
                Pawn pawn = Find.GameInitData.startingAndOptionalPawns.Take(Find.GameInitData.startingPawnCount).First();
                if (pawn != null)
                {
                    if (pawn.gender == Gender.Female)
                    {
                        comp.mother = pawn;
                    }
                }
                comp.xenotype = pawn?.genes?.Xenotype ?? VRESauridsDefOf.VRESaurids_Saurid;
                comp.gestateProgress += 0.1f;
                yield return thing;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (VRESauridsDefOf.VRESaurids_HumanEgg.GetHashCode());
        }
    }
}
