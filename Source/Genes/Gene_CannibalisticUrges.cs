using RimWorld;
using Verse;
using System.Linq;

namespace ZhulTribe
{
    public class Gene_CannibalisticUrges : Gene
    {
        // Number of ticks after eating humanlike meat before debuff starts (3 in-game days = 180,000 ticks)
        private const int TicksWithoutMeatThreshold = 180000;
        private const int TicksFoodPoisonImmunity = 120000; // 2 in-game days = 120,000 ticks
        private int ticksSinceLastMeat = 0;

        // Hediff/moodlet DefNames
        private const string HediffDefName = "Zhul_Hediff_CannibalisticUrges";
        private const string FoodPoisonImmunityDefName = "FoodPoisoningImmunity";

        public override void Tick()
        {
            base.Tick();
            if (pawn.IsHashIntervalTick(200))
            {
                if (PawnAteHumanlikeRecently())
                {
                    // Remove debuff & apply buff if needed
                    RemoveCannibalUrges();
                    GrantFoodPoisonImmunity();
                    ticksSinceLastMeat = 0;
                }
                else
                {
                    ticksSinceLastMeat += 200;
                    if (ticksSinceLastMeat > TicksWithoutMeatThreshold)
                        ApplyCannibalUrges();
                }
            }
        }

        private bool PawnAteHumanlikeRecently()
        {
            // Check recent ingestion memories for humanlike meat
            var memories = pawn.needs?.mood?.thoughts?.memories;
            if (memories == null) return false;

            return memories.MemoriesListForReading.Any(thought =>
                (thought.def.defName == "AteHumanlikeMeatDirect" || thought.def.defName == "AteCorpseHumanlike") &&
                Find.TickManager.TicksGame - thought.age < 3000); // less than 0.5 day ago
        }

        private void ApplyCannibalUrges()
        {
            if (pawn.health.hediffSet.HasHediff(HediffDef.Named(HediffDefName))) return;
            pawn.health.AddHediff(HediffDef.Named(HediffDefName));
        }

        private void RemoveCannibalUrges()
        {
            var urges = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(HediffDefName));
            if (urges != null)
                pawn.health.RemoveHediff(urges);
        }

        private void GrantFoodPoisonImmunity()
        {
            // Check if already immune
            if (pawn.health.hediffSet.HasHediff(HediffDef.Named(FoodPoisonImmunityDefName))) return;

            var hediff = HediffMaker.MakeHediff(HediffDef.Named(FoodPoisonImmunityDefName), pawn);
            hediff.TryGetComp<HediffComp_Disappears>()?.SetDisappearsIn(TicksFoodPoisonImmunity);
            pawn.health.AddHediff(hediff);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksSinceLastMeat, "ticksSinceLastMeat", 0);
        }
    }
}
