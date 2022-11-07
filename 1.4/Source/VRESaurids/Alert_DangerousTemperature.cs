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
    public class Alert_DangerousTemperature : Alert_Critical
    {
        public List<Pawn> culpritsResult = new List<Pawn>();

        public List<string> culpritsNames = new List<string>();

        public List<Pawn> Culprits
        {
            get
            {
                culpritsResult.Clear();
                culpritsNames.Clear();
                foreach (Pawn item in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists_NoSuspended)
                {
                    if (item.genes.HasGene(VRESauridsDefOf.VRESaurids_ColdBlooded) && (item.health.hediffSet.HasHediff(VRESauridsDefOf.VRESaurids_HyperthermicSlowdown) || item.health.hediffSet.HasHediff(VRESauridsDefOf.VRESaurids_HypothermicSlowdown)))
                    {
                        culpritsResult.Add(item);
                        culpritsNames.Add(item.Name.ToStringShort);
                    }
                }
                return culpritsResult;
            }
        }

        public override string GetLabel()
        {
            return "VRESaurids.ColdBloodedDanger".Translate();
        }

        public override TaggedString GetExplanation()
        {
            return "VRESaurids.ColdBloodedDangerExplanation".Translate() + ":\n" + culpritsNames.ToLineList("  - ");
        }

        public override AlertReport GetReport()
        {
            return AlertReport.CulpritsAre(Culprits);
        }
    }
}
