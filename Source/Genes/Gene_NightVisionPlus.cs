using RimWorld;
using Verse;
using UnityEngine;

namespace ZhulTribe.Genes
{
    public class Gene_NightVisionPlus : Gene
    {
        // Hediff/Buff DefNames (match XML)
        private const string NightBuffDef = "Zhul_Hediff_NightVisionPlusBuff";
        private const string EnjoyingChaosDef = "Zhul_Hediff_EnjoyingChaos";

        public override void Tick()
        {
            base.Tick();

            if (pawn.IsHashIntervalTick(200))
            {
                bool isEclipseOrFlare = IsEclipseOrSolarFlare();
                bool isNightOrDark = IsNightOrLowLight();

                // Apply/remove the core night vision buff
                if (isNightOrDark)
                {
                    ApplyOrRefreshHediff(NightBuffDef);
                    // Optionally, add eye glow code here for visual effect
                }
                else
                {
                    RemoveHediff(NightBuffDef);
                    // Optionally, remove eye glow here
                }

                // Apply/remove "Enjoying Chaos" moodlet during event
                if (isEclipseOrFlare)
                {
                    ApplyOrRefreshHediff(EnjoyingChaosDef);
                    GrantDoubleVision();
                }
                else
                {
                    RemoveHediff(EnjoyingChaosDef);
                }
            }
        }

        private bool IsNightOrLowLight()
        {
            // Consider "night" as 22:00–05:59, and/or map lighting below 0.5
            float mapGlow = pawn.Map?.glowGrid?.GameGlowAt(pawn.Position, false) ?? 1f;
            int hour = GenLocalDate.HourOfDay(pawn);
            return (mapGlow < 0.5f) || (hour >= 22 || hour < 6);
        }

        private bool IsEclipseOrSolarFlare()
        {
            // Check map condition for eclipse or solar flare
            if (pawn.Map == null) return false;
            foreach (var cond in pawn.Map.gameConditionManager.ActiveConditions)
            {
                if (cond.def == GameConditionDefOf.Eclipse || cond.def == GameConditionDefOf.SolarFlare)
                    return true;
            }
            return false;
        }

        private void ApplyOrRefreshHediff(string hediffName)
        {
            var def = HediffDef.Named(hediffName);
            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(def);
            if (hediff == null)
                pawn.health.AddHediff(def);
            else
                hediff.Severity = def.initialSeverity; // refresh
        }

        private void RemoveHediff(string hediffName)
        {
            var def = HediffDef.Named(hediffName);
            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(def);
            if (hediff != null)
                pawn.health.RemoveHediff(hediff);
        }

        private void GrantDoubleVision()
        {
            // Optionally, add a temporary stat buff for vision range here
            // If you want a visible hediff for double sight, define and apply another HediffDef as above.
            // This stub is here for expansion.
        }

        // Optional: Visual eye glow (requires sprite/overlay code, not included here)
        /*
        private void SetEyeGlow()
        {
            // Use HAR or RimWorld custom overlay code to show glow if facing South at night.
        }
        */

        public override void ExposeData()
        {
            base.ExposeData();
            // No custom data to persist yet.
        }
    }
}
