using Verse;
using RimWorld;
using UnityEngine;
using System.Collections.Generic;

namespace ZhulTribe
{
    /// <summary>
    /// Gene that gives night-based buffs: suppresses fatigue, nullifies short-term mood debuffs,
    /// and grants real stat bonuses via a hidden hediff.
    /// </summary>
    public class Gene_NightRaider : Gene
    {
        private static readonly float nightFatigueSuppression = 0.3f;
        private static readonly string HediffDefName = "Zhul_Hediff_NightCombatBuff";
        private bool nightBuffApplied = false;

        public override void Tick()
        {
            base.Tick();

            if (pawn == null || pawn.Dead || !pawn.Spawned || pawn.Map == null)
                return;

            if (IsNightTime)
            {
                TrySuppressFatigue();
                TryNullifyShortMoodDebuffs();
                ApplyNightCombatBuff();
            }
            else
            {
                RemoveNightCombatBuff();
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn != null && pawn.Spawned && IsNightTime)
            {
                ApplyNightCombatBuff();
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            RemoveNightCombatBuff();
        }

        private void TrySuppressFatigue()
        {
            var rest = pawn.needs?.rest;
            if (rest != null && rest.CurLevel < 1f)
            {
                rest.CurLevel += 0.0001f * nightFatigueSuppression;
            }
        }

        private void TryNullifyShortMoodDebuffs()
        {
            var memories = pawn.needs?.mood?.thoughts?.memories;
            if (memories == null) return;

            foreach (var mem in memories.MemoriesListForReading)
            {
                if (mem.def.durationDays > 0 && mem.Age < 60000)
                {
                    mem.SetForcedStage(0);
                }
            }
        }

        private void ApplyNightCombatBuff()
        {
            if (nightBuffApplied) return;

            if (!pawn.health.hediffSet.HasHediff(HediffDef.Named(HediffDefName)))
            {
                var hediff = HediffMaker.MakeHediff(HediffDef.Named(HediffDefName), pawn);
                pawn.health.AddHediff(hediff);
            }

            nightBuffApplied = true;
        }

        private void RemoveNightCombatBuff()
        {
            if (!nightBuffApplied) return;

            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(HediffDefName));
            if (hediff != null)
            {
                pawn.health.RemoveHediff(hediff);
            }

            nightBuffApplied = false;
        }

        private bool IsNightTime
        {
            get
            {
                int hour = GenLocalDate.HourOfDay(pawn);
                return hour < 6 || hour >= 20;
            }
        }
    }
}
