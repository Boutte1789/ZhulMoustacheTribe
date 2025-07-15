using Verse;
using RimWorld;
using UnityEngine;
using System;

namespace ZhulTribe
{
    public class Gene_NightRaider : Gene
    {
        private static readonly float nightCombatBoost = 0.2f;
        private static readonly float nightFatigueSuppression = 0.3f;

        public override void Tick()
        {
            base.Tick();

            if (pawn?.Needs != null && pawn.Spawned && IsNightTime)
            {
                // Suppress rest decay slightly during night
                var restNeed = pawn.needs?.rest;
                if (restNeed != null && restNeed.CurLevel < 1f)
                {
                    restNeed.CurLevel += 0.0001f * nightFatigueSuppression; // Tweakable
                }

                // Disable some mood debuffs during night
                var mood = pawn.needs?.mood?.thoughts?.memories;
                if (mood != null)
                {
                    foreach (var mem in mood.MemoriesListForReading)
                    {
                        if (mem.def.durationDays > 0 && mem.Age < 60000) // short-term memory
                        {
                            mem.SetForcedStage(0); // temporarily nullify intensity
                        }
                    }
                }
            }
        }

        public override void PostAdd()
        {
            base.PostAdd();
            if (pawn != null && !pawn.Dead && pawn.Spawned)
            {
                ApplyNightCombatBuffs();
            }
        }

        private void ApplyNightCombatBuffs()
        {
            if (!IsNightTime) return;

            var stats = pawn?.stats;
            if (stats == null) return;

            stats.GetStatValue(StatDefOf.MeleeHitChance, true);
            stats.GetStatValue(StatDefOf.ShootingAccuracyPawn, true);
        }

        private bool IsNightTime
        {
            get
            {
                int hour = GenLocalDate.HourOfDay(pawn);
                return hour < 6 || hour >= 20; // 8 PM to 6 AM
            }
        }
    }
}
