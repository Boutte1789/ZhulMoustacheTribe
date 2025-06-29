using System;
using RimWorld;
using Verse;

namespace ZhulTribe
{
    /// <summary>
    /// Zhul pawn generation system for gender-specific traits
    /// </summary>
    public static class ZhulGenderTraits
    {
        /// <summary>
        /// Check if a trait should be applied based on gender
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