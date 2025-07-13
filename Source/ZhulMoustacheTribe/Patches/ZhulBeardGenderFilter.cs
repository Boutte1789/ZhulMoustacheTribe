using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;
using AlienRace;

namespace ZhulTribe.Patches
{
    /// <summary>
    /// Harmony patch for beard overlay logic (DISABLED: AlienPartGenerator.BodyAddon.DrawAddon does not exist in HAR 2.0+ for RW 1.5).
    /// TODO: Re-implement using the correct modern HAR hook for beard overlays.
    /// </summary>
    /*
    [HarmonyPatch(typeof(AlienPartGenerator.BodyAddon), nameof(AlienPartGenerator.BodyAddon.DrawAddon))]
    public static class ZhulBeardGenderFilter
    {
        static bool Prefix(AlienPartGenerator.BodyAddon __instance, Pawn pawn, ref Vector3 drawLoc, Quaternion quat, Rot4 bodyFacing, bool portrait, bool head, ref bool __result)
        {
            // Null safety
            if (pawn == null || pawn.def == null || pawn.story == null)
                return true;

            // Only filter for beards (by label/tag/defName)
            // You may need to adjust this check depending on how beards are tagged in your mod
            if (__instance == null || string.IsNullOrEmpty(__instance.path) || !__instance.path.ToLower().Contains("beard"))
                return true;

            // Check for ZhulXenoType xenotype
            var xenotype = pawn.genes?.Xenotype;
            if (xenotype == null || !string.Equals(xenotype.defName, "ZhulXenoType", StringComparison.Ordinal))
            {
                // Suppress beard rendering for non-ZhulXenoType
                __result = false;
                return false;
            }

            // Only allow beards for males
            if (pawn.gender != Gender.Male)
            {
                __result = false;
                return false;
            }

            // Big & Small Framework: adjust beard overlay for custom head types
            var headType = pawn.story?.headType?.defName;
            if (headType == "ZhulBigMale")
            {
                drawLoc += new Vector3(0f, -0.02f, 0f);
            }
            else if (headType == "ZhulSmallMale")
            {
                drawLoc += new Vector3(0f, -0.02f, 0f);
            }

            // Allow normal rendering
            return true;
        }
    }
    */
}
