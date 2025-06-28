using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;

namespace ZhulTribe
{
    /// <summary>
    /// Zhul Recruitment System - Very Simplified Implementation
    /// Based on Copilot conversation: Focus on Bone-Comb Altar passive conversion
    /// </summary>

    // Bone-Comb Altar - Passive Conversion Building
    public class CompProperties_BoneCombAltar : CompProperties
    {
        public float conversionRadius = 10f;
        public float conversionChance = 0.001f; // Very low per-tick chance
        
        public CompProperties_BoneCombAltar()
        {
            compClass = typeof(Comp_BoneCombAltar);
        }
    }

    public class Comp_BoneCombAltar : ThingComp
    {
        public CompProperties_BoneCombAltar Props => (CompProperties_BoneCombAltar)props;

        public override void CompTick()
        {
            base.CompTick();
            
            if (parent.IsHashIntervalTick(250)) // Check every ~4 seconds
            {
                var pawns = GenRadial.RadialDistinctThingsAround(parent.Position, parent.Map, Props.conversionRadius, true)
                    .OfType<Pawn>()
                    .Where(p => p.Faction?.IsPlayer == true && !p.story.traits.HasTrait(TraitDefOf.Cannibal));

                foreach (var pawn in pawns)
                {
                    if (Rand.Chance(Props.conversionChance))
                    {
                        pawn.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_BoneWhispers);
                        
                        // Very rare chance to gain cannibal trait
                        if (Rand.Chance(0.001f))
                        {
                            pawn.story.traits.GainTrait(new Trait(TraitDefOf.Cannibal));
                            Messages.Message("ZHUL_BoneWhispersConversion".Translate(pawn.Named("PAWN")), 
                                pawn, MessageTypeDefOf.PositiveEvent);
                        }
                    }
                }
            }
        }
    }

    // Enhanced Spirit-Eater Recruitment via simple event handling
    public class SpiritEaterRecruitmentHandler
    {
        public static void ApplyRecruitmentBonus(Pawn initiator, Pawn recipient)
        {
            // Give Spirit-Eaters a recruitment bonus when dealing with cannibals
            if (initiator.kindDef == ZhulDefOf.ZHUL_SpiritEater && 
                recipient.story.traits.HasTrait(TraitDefOf.Cannibal) && 
                recipient.guest?.GuestStatus == GuestStatus.Prisoner)
            {
                // Reduce resistance by additional 25%
                if (recipient.guest != null)
                {
                    recipient.guest.resistance *= 0.75f;
                }
            }
        }
    }
}