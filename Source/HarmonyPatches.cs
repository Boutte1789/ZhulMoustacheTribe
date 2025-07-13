using HarmonyLib;
using Verse;
using RimWorld;

namespace ZhulTribe.Patches
{
    // Main patch class for Zhul tribe
    [HarmonyPatch]
    [HarmonyAfter("harmony.bigsmall.core")]
    public static class ZhulTribeHarmonyPatches
    {
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
            if (prisoner.story?.traits?.HasTrait(DefDatabase<TraitDef>.GetNamedSilentFail("Cannibal")) == true)
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

        // Zhul-specific spawn setup (currently a placeholder)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Pawn), "SpawnSetup")]
        public static void Pawn_SpawnSetup_Postfix(Pawn __instance)
        {
            // Add any additional custom initialization for Zhul pawns on spawn here if needed
        }
    }
}
