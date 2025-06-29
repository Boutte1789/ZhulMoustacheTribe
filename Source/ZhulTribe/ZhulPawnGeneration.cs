using System;
using RimWorld;
using Verse;
using HarmonyLib;
using UnityEngine;

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

    /// <summary>
    /// Harmony patch disabled - moustaches are now built into head graphics
    /// </summary>
    /*
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn")]
    public static class ZhulFacialHairPatch
    {
        [HarmonyPostfix]
        public static void ForceMoustacheOnZhulMales(ref Pawn __result)
        {
            // DISABLED: Moustaches are now part of the head graphics
        }
    }
    */
}