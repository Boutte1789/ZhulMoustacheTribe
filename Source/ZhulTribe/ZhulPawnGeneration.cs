using System;
using RimWorld;
using Verse;

namespace ZhulTribe
{
    /// <summary>
    /// Simple gender check utility for Zhul pawns
    /// This approach avoids complex Harmony patching
    /// </summary>
    public static class ZhulGenderTraits
    {
        /// <summary>
        /// Check if a trait should be applied based on gender
        /// Called from XML or other game systems
        /// </summary>
        public static bool ShouldApplyMoustacheTrait(Pawn pawn)
        {
            return pawn?.gender == Gender.Male && pawn?.def?.defName == "ZHUL_AlienHumanoid";
        }

        public static bool ShouldApplyTattooTrait(Pawn pawn)
        {
            return pawn?.gender == Gender.Female && pawn?.def?.defName == "ZHUL_AlienHumanoid";
        }
    }
}