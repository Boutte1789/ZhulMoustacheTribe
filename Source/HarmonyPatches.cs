using HarmonyLib;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AlienRace;
using RimWorld;

namespace ZhulTribe.Patches
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
        
        // Auto-apply beard to male Zhul colonists (FIXED)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new System.Type[] { typeof(PawnGenerationRequest) })]
        public static void GeneratePawn_Postfix(Pawn __result, PawnGenerationRequest request)
        {
            // Force beard for male Zhuls from any faction
            if (__result != null && __result.RaceProps.Humanlike 
                && (__result.def?.defName == "ZHUL_AlienHumanoid" || 
                    __result.Faction?.def?.defName == "ZHUL_Tribe"))
            {
                if (__result.gender == Gender.Male)
                {
                    // Force beard overlay for males
                    var overlayDef = DefDatabase<HeadOverlayDef>.GetNamedSilentFail("Overlay_ZHUL_Beard");
                    if (overlayDef != null && __result.story?.headOverlays != null)
                    {
                        // Remove any existing beard first
                        __result.story.headOverlays.RemoveAll(h => h.def.defName.Contains("Beard"));
                        
                        // Add the proper Zhul beard
                        var overlay = new HeadOverlay(overlayDef, Color.white);
                        __result.story.headOverlays.Add(overlay);
                    }
                }
                else if (__result.gender == Gender.Female)
                {
                    // Ensure clean heads for female bone-ash tattoos
                    if (__result.story?.headOverlays != null)
                    {
                        __result.story.headOverlays.RemoveAll(h => h.def.defName.Contains("Beard"));
                    }
                }
            }
            
            // Add custom eye overlay for Zhul xenotype pawns (applies to all ZhulXenoType, not gender-based)
            if (__result != null && __result.genes != null && __result.genes.Xenotype != null && __result.genes.Xenotype.defName == "ZhulXenoType")
            {
                // Only render overlay in South direction (RimWorld convention: _south)
                var eyeOverlayGraphic = GraphicDatabase.Get<Graphic_Multi>(
                    "Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south",
                    ShaderDatabase.Cutout,
                    Vector2.one,
                    Color.white);
                if (__result.story?.headOverlays != null)
                {
                    var overlayDef = DefDatabase<HeadOverlayDef>.GetNamedSilentFail("Zhul_EyeOverlay");
                    if (overlayDef != null)
                    {
                        var overlay = new HeadOverlay(overlayDef, Color.white);
                        overlay.graphic = eyeOverlayGraphic;
                        __result.story.headOverlays.Add(overlay);
                    }
                }
            }
            
            // Note: Leadership challenges handled by VME_BloodCourt meme ritual duels
            // No automatic reassignment - players must initiate blood court ritual
        }
        
        // Enhanced prisoner recruitment system
        [HarmonyPostfix]
        [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "DoRecruit")]
        public static void DoRecruit_Postfix(Pawn recruiter, Pawn recruitee, bool __result)
        {
            if (__result && recruiter?.def?.defName == "ZHUL_AlienHumanoid")
            {
                // Apply Zhul recruitment success bonus thoughts
                ApplyZhulRecruitmentBonus(recruiter, recruitee);
            }
        }

        // Modify recruitment resistance for Zhul wardens
        [HarmonyPostfix]
        [HarmonyPatch(typeof(InteractionWorker_RecruitAttempt), "Interacted")]
        public static void RecruitAttempt_Interacted_Postfix(Pawn initiator, Pawn recipient, InteractionDef intDef)
        {
            if (initiator?.def?.defName == "ZHUL_AlienHumanoid" && recipient?.IsPrisoner == true)
            {
                ApplyZhulRecruitmentModifiers(initiator, recipient);
            }
        }

        // Apply Zhul-specific recruitment bonuses
        private static void ApplyZhulRecruitmentBonus(Pawn recruiter, Pawn recruitee)
        {
            var settings = LoadedModManager.GetMod<ZhulTribeMod>()?.GetSettings<ZhulModSettings>();
            if (settings == null) return;

            // Apply recruitment success thoughts
            recruiter.needs?.mood?.thoughts?.memories?.TryGainMemory(
                DefDatabase<ThoughtDef>.GetNamedSilentFail("ZT_SuccessfulRitual"));
        }

        // Calculate and apply recruitment modifiers
        private static void ApplyZhulRecruitmentModifiers(Pawn warden, Pawn prisoner)
        {
            if (warden?.needs?.mood?.thoughts?.memories == null || prisoner?.needs?.mood?.thoughts?.memories == null)
                return;

            var settings = LoadedModManager.GetMod<ZhulTribeMod>()?.GetSettings<ZhulModSettings>();
            if (settings == null) return;

            // Trait compatibility bonuses
            if (prisoner.story?.traits?.HasTrait(TraitDefOf.Cannibal) == true)
            {
                prisoner.needs.mood.thoughts.memories.TryGainMemory(
                    DefDatabase<ThoughtDef>.GetNamedSilentFail("ZT_SharedCannibalHunger"));
            }
            
            if (prisoner.story?.traits?.HasTrait(TraitDefOf.Brawler) == true ||
                prisoner.story?.traits?.HasTrait(TraitDefOf.Bloodlust) == true)
            {
                prisoner.needs.mood.thoughts.memories.TryGainMemory(
                    DefDatabase<ThoughtDef>.GetNamedSilentFail("ZT_ImpressedByZhulStrength"));
            }

            // Warden social bonus
            if (warden.skills?.GetSkill(SkillDefOf.Social)?.Level >= 8)
            {
                prisoner.needs.mood.thoughts.memories.TryGainMemory(
                    DefDatabase<ThoughtDef>.GetNamedSilentFail("ZT_RespectedByZhulWarden"));
            }

            // Terror raid survivor bonus
            if (prisoner.records?.GetValue(RecordDefOf.TimeAsColonistOrColonyAnimal) < 1f)
            {
                prisoner.needs.mood.thoughts.memories.TryGainMemory(
                    DefDatabase<ThoughtDef>.GetNamedSilentFail("ZT_TerrifiedIntoSubmission"));
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