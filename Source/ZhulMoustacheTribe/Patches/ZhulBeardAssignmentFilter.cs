using System;
using HarmonyLib;
using RimWorld;
using Verse;
using AlienRace;

namespace ZhulTribe.Patches
{
    /// <summary>
    /// Prevents Zhul beards from being assigned to non-male or non-ZhulXenoType pawns.
    /// </summary>
    [HarmonyPatch(typeof(AlienPartGenerator), "GenerateBeard")]
    public static class ZhulBeardAssignmentFilter
    {
        static bool Prefix(Pawn pawn, ref string __result)
        {
            // Only allow beard assignment for male ZhulXenoType pawns
            if (pawn == null || pawn.gender != Gender.Male)
            {
                __result = null;
                return false;
            }
            var xenotype = pawn.genes?.Xenotype;
            if (xenotype == null || !string.Equals(xenotype.defName, "ZhulXenoType", StringComparison.Ordinal))
            {
                __result = null;
                return false;
            }
            // Allow normal beard assignment for eligible pawns
            return true;
        }
    }
}
