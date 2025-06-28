using RimWorld;
using Verse;
using Verse.Sound;

namespace ZhulTribe
{
    public class CompUseEffect_MoustacheOil : CompUseEffect
    {
        public override void DoEffect(Pawn user)
        {
            base.DoEffect(user);
            
            // Play grooming ritual sound
            if (ZhulDefOf.ZHUL_GroomingRitual != null)
            {
                ZhulDefOf.ZHUL_GroomingRitual.PlayOneShot(user);
            }
            
            // Add the moustache oil thought if user has the ritual trait
            if (user.story?.traits?.HasTrait(ZhulDefOf.ZHUL_MoustacheRitual) == true)
            {
                user.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_UsedMoustacheOil);
                
                // Extra mood bonus for proper ritual
                user.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.AteRawFood); // Temporary placeholder
            }
            
            // Visual effect - create a small mote or sparkle
            MoteMaker.ThrowText(user.DrawPos, user.Map, "Groomed moustache", new UnityEngine.Color(1f, 0.84f, 0f), 3.5f);
        }

        public override bool CanBeUsedBy(Pawn p, out string failReason)
        {
            // Can only be used by Zhul or pawns with moustache ritual trait
            if (p.def.defName == "ZHUL_AlienHumanoid" || 
                p.story?.traits?.HasTrait(ZhulDefOf.ZHUL_MoustacheRitual) == true)
            {
                failReason = null;
                return true;
            }
            
            failReason = "Only those versed in Zhul moustache rituals can use this oil properly.";
            return false;
        }
    }
}