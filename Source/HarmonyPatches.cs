using HarmonyLib;
using Verse;
using System.Reflection;
using System.Collections.Generic;
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
        
        // Auto-apply moustache to male Zhul colonists
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new System.Type[] { typeof(PawnGenerationRequest) })]
        public static void GeneratePawn_Postfix(Pawn __result, PawnGenerationRequest request)
        {
            if (__result != null && __result.RaceProps.Humanlike 
                && __result.def?.defName == "ZHUL_AlienHumanoid" 
                && __result.gender == Gender.Male)
            {
                // Apply moustache overlay to male Zhuls
                var overlayDef = DefDatabase<HeadOverlayDef>.GetNamed("Zhul_Moustache", errorOnFail: false);
                if (overlayDef != null && __result.story?.headType != null)
                {
                    // Add moustache to head overlays if not already present
                    var alienComp = __result.TryGetComp<AlienPartGenerator.AlienComp>();
                    if (alienComp?.addonVariants != null)
                    {
                        // Check if moustache overlay already exists
                        bool hasMoustache = false;
                        foreach (var variant in alienComp.addonVariants)
                        {
                            if (variant.Key.Contains("Moustache"))
                            {
                                hasMoustache = true;
                                break;
                            }
                        }
                        
                        // Add moustache if not present
                        if (!hasMoustache)
                        {
                            alienComp.addonVariants.Add("Zhul_Moustache", 0);
                        }
                    }
                }
            }
            
            // Ensure no moustache for female Zhuls (clean heads for bone-ash tattoos)
            if (__result != null && __result.RaceProps.Humanlike 
                && __result.def?.defName == "ZHUL_AlienHumanoid" 
                && __result.gender == Gender.Female)
            {
                var alienComp = __result.TryGetComp<AlienPartGenerator.AlienComp>();
                if (alienComp?.addonVariants != null)
                {
                    // Remove any moustache overlays for females
                    var keysToRemove = new List<string>();
                    foreach (var variant in alienComp.addonVariants)
                    {
                        if (variant.Key.Contains("Moustache"))
                        {
                            keysToRemove.Add(variant.Key);
                        }
                    }
                    
                    foreach (var key in keysToRemove)
                    {
                        alienComp.addonVariants.Remove(key);
                    }
                }
            }
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