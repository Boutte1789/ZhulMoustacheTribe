using RimWorld;
using Verse;

namespace ZhulTribe
{
    [DefOf]
    public static class ZhulDefOf
    {
        // Thoughts
        public static ThoughtDef ZHUL_RitualFeastSuccess;
        public static ThoughtDef ZHUL_RitualFeastFailure;
        public static ThoughtDef ZHUL_AteHumanlikeMeatDirect;
        public static ThoughtDef ZHUL_ButcheredHumanlikeCorpse;
        public static ThoughtDef ZHUL_UsedMoustacheOil;
        public static ThoughtDef ZHUL_NeglectedGrooming;

        // Traits
        public static TraitDef ZHUL_Cannibal;
        public static TraitDef ZHUL_MoustacheRitual;
        public static TraitDef ZHUL_FacialTattoos;

        // Things
        public static ThingDef ZHUL_MoustacheOil;
        public static ThingDef ZHUL_SacrificialDagger;
        public static ThingDef ZHUL_BoneNecklace;
        public static ThingDef ZHUL_BoneAltar;
        public static ThingDef ZHUL_OilSwirl;

        // Race
        public static ThingDef ZHUL_AlienHumanoid;

        // Faction
        public static FactionDef ZHUL_Tribe;

        // Pawn Kinds
        public static PawnKindDef ZHUL_CurledSavage;
        public static PawnKindDef ZHUL_SpiritEater;
        public static PawnKindDef ZHUL_BoneChief;

        // Incidents
        public static IncidentDef ZHUL_RitualFeast;
        public static IncidentDef ZHUL_MoustacheRitualEvent;

        // Sounds
        public static SoundDef ZHUL_RitualChant;
        public static SoundDef ZHUL_BoneDrums;
        public static SoundDef ZHUL_SpiritPrayer;
        public static SoundDef ZHUL_WarCry;
        public static SoundDef ZHUL_GroomingRitual;
        public static SoundDef ZHUL_FuneralRites;
        public static SoundDef ZHUL_TribalAmbience;

        // Recruitment System Definitions
        public static HediffDef ZHUL_FirstCurl;
        public static HediffDef ZHUL_BoundByCurl;
        public static HediffDef ZHUL_PregnantWithCurl;
        
        public static ThoughtDef ZHUL_FailedInitiation;
        public static ThoughtDef ZHUL_BoneWhispers;
        
        public static RitualPatternDef ZHUL_CurlInitiation;
        public static RitualPatternDef ZHUL_MatingFeast;
        
        public static JobDef ZHUL_SpiritBinderInterrogate;
        public static WorkGiverDef ZHUL_SpiritBinderInterrogation;
        
        public static ThingDef ZHUL_BoneCombAltar;
        public static PawnKindDef ZHUL_Child;
        


        static ZhulDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ZhulDefOf));
        }
    }
}
