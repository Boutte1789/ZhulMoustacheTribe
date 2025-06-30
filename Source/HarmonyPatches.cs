using HarmonyLib;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AlienRace;
using RimWorld;

namespace ZhulTribe
{
    // Main patch class with HarmonyAfter attribute for Big & Small compatibility
    [HarmonyPatch]
    [HarmonyAfter("harmony.bigsmall.core")]
    public static class ZhulTribeHarmonyPatches
    {
        // Static field cache for Graphic_Atlas reflection (optimization)
        private static FieldInfo _atlasFieldInfo;
        
        static ZhulTribeHarmonyPatches()
        {
            // Cache the reflection field for performance
            _atlasFieldInfo = typeof(Graphic_Atlas).GetField("atlas", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        // Auto-apply moustache to male Zhul colonists (FIXED)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new System.Type[] { typeof(PawnGenerationRequest) })]
        public static void GeneratePawn_Postfix(Pawn __result, PawnGenerationRequest request)
        {
            // Force moustache for male Zhuls from any faction
            if (__result != null && __result.RaceProps.Humanlike 
                && (__result.def?.defName == "ZHUL_AlienHumanoid" || 
                    __result.Faction?.def?.defName == "ZHUL_Tribe"))
            {
                if (__result.gender == Gender.Male)
                {
                    // Force moustache overlay for males
                    var overlayDef = DefDatabase<HeadOverlayDef>.GetNamedSilentFail("Zhul_Moustache");
                    if (overlayDef != null && __result.story?.headOverlays != null)
                    {
                        // Remove any existing moustache first
                        __result.story.headOverlays.RemoveAll(h => h.def.defName.Contains("Moustache"));
                        
                        // Add the proper Zhul moustache
                        var overlay = new HeadOverlay(overlayDef, Color.white);
                        __result.story.headOverlays.Add(overlay);
                    }
                }
                else if (__result.gender == Gender.Female)
                {
                    // Ensure clean heads for female bone-ash tattoos
                    if (__result.story?.headOverlays != null)
                    {
                        __result.story.headOverlays.RemoveAll(h => h.def.defName.Contains("Moustache"));
                    }
                }
            }
            
            // Note: Leadership challenges handled by VME_BloodCourt meme ritual duels
            // No automatic reassignment - players must initiate blood court ritual
        }
        
        // Zhul-specific spawn setup
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Pawn), "SpawnSetup")]
        public static void Pawn_SpawnSetup_Postfix(Pawn __instance)
        {
            // Additional Zhul-specific initialization if needed
            if (__instance?.def?.defName == "ZHUL_AlienHumanoid")
            {
                // Custom initialization for Zhul pawns on spawn
            }
        }
    }
}