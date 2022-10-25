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
    public class IngestionOutcomeDoer_KilledAChild : IngestionOutcomeDoer
    {
        public override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
        {
            TaleRecorder.RecordTale(VRESauridsDefOf.VRESaurids_KilledEggChild, pawn);
            pawn.needs.mood.thoughts.memories.TryGainMemory(VRESauridsDefOf.VRESaurids_KilledEggChildThought);
        }
    }
}
