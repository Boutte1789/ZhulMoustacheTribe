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
    /// Harmony patch to force brown curly moustaches on male Zhul pawns
    /// </summary>
    [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn")]
    public static class ZhulFacialHairPatch
    {
        [HarmonyPostfix]
        public static void ForceMoustacheOnZhulMales(ref Pawn __result)
        {
            try
            {
                // Only process Zhul males
                if (__result?.def?.defName == "ZHUL_AlienHumanoid" && 
                    __result.gender == Gender.Male && 
                    __result.story != null)
                {
                    // Force the Zhul curly moustache using style system
                    var moustacheDef = DefDatabase<BeardDef>.GetNamedSilentFail("ZHUL_CurlyMoustache");
                    if (moustacheDef != null)
                    {
                        // Use the styling system (RimWorld 1.3+)
                        __result.style = __result.style ?? new Pawn_StyleTracker(__result);
                        __result.style.beardDef = moustacheDef;
                        
                        // Set beard color to brown
                        __result.story.HairColor = new Color(0.4f, 0.2f, 0.1f);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"ZhulTribe: Error applying facial hair - {ex.Message}");
            }
        }
    }
}