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

        // Things
        public static ThingDef ZHUL_MoustacheOil;
        public static ThingDef ZHUL_SacrificialDagger;
        public static ThingDef ZHUL_BoneNecklace;

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
        public static IncidentDef ZHUL_MoustacheRitual;

        static ZhulDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ZhulDefOf));
        }
    }
}
