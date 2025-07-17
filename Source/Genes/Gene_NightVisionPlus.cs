using RimWorld;
using Verse;
using UnityEngine;
using AlienRace;

namespace ZhulTribe.Genes
{
    public class Gene_NightVisionPlus : Gene
    {
        // Hediff/Buff DefNames (match XML)
        private const string NightBuffDef = "Zhul_Hediff_NightVisionPlusBuff";
        private const string EnjoyingChaosDef = "Zhul_Hediff_EnjoyingChaos";

        // Eye overlay paths
        private const string GlowOverlayPath = "Things/Pawn/Humanlike/Heads/EyeOverlays/Zhul_NightVisionEyes_south";
        private const string DefaultOverlayPath = "Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south";

        public override void Tick()
        {
            base.Tick();

            if (pawn.IsHashIntervalTick(200))
            {
                bool isEclipseOrFlare = IsEclipseOrSolarFlare();
                bool isNightOrDark = IsNightOrLowLight();

                // Eye overlay logic: show glow when low light & night, or during eclipse/flare
                bool shouldGlow = (isNightOrDark && IsNightHours()) || isEclipseOrFlare;
                UpdateEyeOverlay(shouldGlow);

                // Apply/remove the core night vision buff
                if (isNightOrDark)
                {
                    ApplyOrRefreshHediff(NightBuffDef);
                }
                else
                {
                    RemoveHediff(NightBuffDef);
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
            // Map lighting below 0.5 triggers night vision; both inside and outside
            float mapGlow = pawn.Map?.glowGrid?.GameGlowAt(pawn.Position, false) ?? 1f;
            return mapGlow < 0.5f;
        }

        private bool IsNightHours()
        {
            // "Night" for eye glow: 19:00–4:59 (RimWorld hours)
            int hour = GenLocalDate.HourOfDay(pawn);
            return (hour >= 19 || hour < 5);
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

        private void UpdateEyeOverlay(bool enableGlow)
        {
            var alienComp = pawn.TryGetComp<AlienRace.CompAlienPart>();
            if (alienComp == null) return;

            string currentOverlay = alienComp.eyehighlightPathSouth;
            string desiredOverlay = enableGlow ? GlowOverlayPath : DefaultOverlayPath;

            if (currentOverlay != desiredOverlay)
            {
                alienComp.eyehighlightPathSouth = desiredOverlay;
                pawn.Drawer?.renderer?.graphics?.SetAllGraphicsDirty();
            }
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

        public override void ExposeData()
        {
            base.ExposeData();
            // No custom data to persist yet.
        }
    }
}

