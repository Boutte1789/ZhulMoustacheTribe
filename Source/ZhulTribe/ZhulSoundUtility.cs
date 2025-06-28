using RimWorld;
using Verse;
using Verse.Sound;

namespace ZhulTribe
{
    public static class ZhulSoundUtility
    {
        public static void PlayRaidAmbience(Map map, bool hasCorpses)
        {
            if (hasCorpses)
            {
                // Ceremonial sounds for ritual feast
                ZhulDefOf.ZHUL_RitualChant?.PlayOneShot(new TargetInfo(map.Center, map));
                // Delay bone drums slightly for layered effect
                LongEventHandler.ExecuteWhenFinished(() =>
                {
                    ZhulDefOf.ZHUL_BoneDrums?.PlayOneShot(new TargetInfo(map.Center, map));
                });
            }
            else
            {
                // Aggressive sounds for frustrated raiders
                ZhulDefOf.ZHUL_WarCry?.PlayOneShot(new TargetInfo(map.Center, map));
                ZhulDefOf.ZHUL_TribalAmbience?.PlayOneShot(new TargetInfo(map.Center, map));
            }
        }

        public static void PlayDeathSound(Pawn pawn)
        {
            if (pawn.def.defName == "ZHUL_AlienHumanoid" && pawn.Map != null)
            {
                // Play funeral rites for fallen Zhul
                ZhulDefOf.ZHUL_FuneralRites?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
            }
        }

        public static void PlayGroomingRitual(Pawn pawn)
        {
            if (pawn.story?.traits?.HasTrait(ZhulDefOf.ZHUL_MoustacheRitual) == true && pawn.Map != null)
            {
                ZhulDefOf.ZHUL_GroomingRitual?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
                
                // Show visual effect
                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "Ritual grooming", UnityEngine.Color.yellow, 2f);
            }
        }

        public static void PlaySpiritEaterPrayer(Pawn pawn)
        {
            if (pawn.kindDef?.defName == "ZHUL_SpiritEater" && pawn.Map != null)
            {
                ZhulDefOf.ZHUL_SpiritPrayer?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
            }
        }

        public static void PlayBoneChiefWarCry(Pawn pawn)
        {
            if (pawn.kindDef?.defName == "ZHUL_BoneChief" && pawn.Map != null)
            {
                ZhulDefOf.ZHUL_WarCry?.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map));
                
                // Show intimidating visual effect
                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "War cry!", UnityEngine.Color.red, 3f);
            }
        }
    }
}