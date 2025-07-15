using Verse;
using RimWorld;

namespace ZhulTribe.Genes
{
    public class Gene_BloodlustMastery : Gene
    {
        private int humanlikeKillCount = 0;
        private bool masteryGranted = false;
        private const int KillsRequired = 100;
        private const string MasteryHediff = "Zhul_Hediff_BloodlustMastery";

        public override void PostAdd()
        {
            base.PostAdd();
            // If the pawn already had 100+ kills before the gene was added
            TryGrantMasteryIfEligible();
        }

        public override void Notify_KilledPawn(Pawn victim, DamageInfo? dinfo)
        {
            base.Notify_KilledPawn(victim, dinfo);
            if (victim?.RaceProps != null && victim.RaceProps.Humanlike && !victim.Dead)
                return; // Can't kill a living pawn

            if (victim?.RaceProps != null && victim.RaceProps.Humanlike)
            {
                humanlikeKillCount++;
                TryGrantMasteryIfEligible();
            }
        }

        private void TryGrantMasteryIfEligible()
        {
            if (!masteryGranted && humanlikeKillCount >= KillsRequired && pawn != null)
            {
                if (!pawn.health.hediffSet.HasHediff(HediffDef.Named(MasteryHediff)))
                {
                    var hediff = HediffMaker.MakeHediff(HediffDef.Named(MasteryHediff), pawn);
                    pawn.health.AddHediff(hediff);
                }
                masteryGranted = true;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref humanlikeKillCount, "humanlikeKillCount", 0);
            Scribe_Values.Look(ref masteryGranted, "masteryGranted", false);
        }
    }
}
