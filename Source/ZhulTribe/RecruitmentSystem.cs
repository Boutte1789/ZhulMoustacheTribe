using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using UnityEngine;

namespace ZhulTribe
{
    /// <summary>
    /// Zhul Recruitment System based on Copilot conversation
    /// Implements: Curl Initiation, Spirit-Binder Interrogation, Mating Feast, Vow of Bone Comb
    /// </summary>
    
    // 1. Curl Initiation - Ideological Conversion
    public class RitualOutcomeEffectWorker_CurlInitiation : RitualOutcomeEffectWorker_FromQuality
    {
        public override void Apply(float quality, RitualRoleAssignments assignments, LookTargets letterLookTargets)
        {
            Pawn target = assignments.FirstAssignedPawn(ZhulDefOf.ZHUL_RitualRole_ConversionTarget);
            Pawn spiritEater = assignments.FirstAssignedPawn(ZhulDefOf.ZHUL_RitualRole_SpiritEater);
            
            if (target == null) return;
            
            float conversionChance = quality * 0.8f; // Base 80% max success
            
            // Bonus if Spirit-Eater has high social skill
            if (spiritEater != null)
            {
                conversionChance += spiritEater.skills.GetSkill(SkillDefOf.Social).Level * 0.02f;
            }
            
            // Bonus if target already has Cannibal trait
            if (target.story?.traits?.HasTrait(TraitDefOf.Cannibal) == true)
            {
                conversionChance += 0.3f;
            }
            
            if (Rand.Chance(conversionChance))
            {
                // Success - Apply First Curl hediff
                target.health.AddHediff(ZhulDefOf.ZHUL_FirstCurl);
                
                // Convert ideology if different
                if (target.ideo != Faction.OfPlayer.ideos.PrimaryIdeo)
                {
                    target.ideo.SetIdeo(Faction.OfPlayer.ideos.PrimaryIdeo);
                }
                
                // Add cannibal trait if not present
                if (!target.story.traits.HasTrait(TraitDefOf.Cannibal))
                {
                    target.story.traits.GainTrait(new Trait(TraitDefOf.Cannibal));
                }
                
                Messages.Message("ZHUL_CurlInitiationSuccess".Translate(target.NameShortColored), target, MessageTypeDefOf.PositiveEvent);
            }
            else
            {
                // Failure - Small mood debuff
                target.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_FailedInitiation);
                Messages.Message("ZHUL_CurlInitiationFailed".Translate(target.NameShortColored), target, MessageTypeDefOf.NeutralEvent);
            }
        }
    }
    
    // 2. Mating Feast - Reproduction Ritual
    public class RitualOutcomeEffectWorker_MatingFeast : RitualOutcomeEffectWorker_FromQuality
    {
        public override void Apply(float quality, RitualRoleAssignments assignments, LookTargets letterLookTargets)
        {
            var participants = assignments.AllAssignedPawns(ZhulDefOf.ZHUL_RitualRole_MatingPartner).ToList();
            
            if (participants.Count < 2) return;
            
            Pawn partner1 = participants[0];
            Pawn partner2 = participants[1];
            
            // Check if opposite genders and both Zhul
            if (partner1.gender != partner2.gender && 
                partner1.def == ZhulDefOf.ZHUL_AlienHumanoid && 
                partner2.def == ZhulDefOf.ZHUL_AlienHumanoid)
            {
                float conceptionChance = quality * 0.6f; // Base 60% max success
                
                if (Rand.Chance(conceptionChance))
                {
                    // Apply pregnancy to female partner
                    Pawn female = partner1.gender == Gender.Female ? partner1 : partner2;
                    female.health.AddHediff(ZhulDefOf.ZHUL_PregnantWithCurl);
                    
                    Messages.Message("ZHUL_MatingFeastSuccess".Translate(female.NameShortColored), female, MessageTypeDefOf.PositiveEvent);
                }
                else
                {
                    Messages.Message("ZHUL_MatingFeastFailed".Translate(), MessageTypeDefOf.NeutralEvent);
                }
            }
        }
    }
    
    // 3. Spirit-Binder Interrogation - Enhanced Prisoner Recruitment
    public class WorkGiver_SpiritBinderInterrogation : WorkGiver_Warden
    {
        public override Job NonScanJob(Pawn pawn)
        {
            // Only Spirit-Eaters can perform this
            if (!pawn.story.traits.HasTrait(ZhulDefOf.ZHUL_SpiritEater)) return null;
            
            return TryGiveJob(pawn);
        }
        
        public override Job TryGiveJob(Pawn pawn)
        {
            Pawn prisoner = FindPrisonerToInterrogate(pawn);
            if (prisoner == null) return null;
            
            return JobMaker.MakeJob(ZhulDefOf.ZHUL_SpiritBinderInterrogate, prisoner);
        }
        
        private Pawn FindPrisonerToInterrogate(Pawn warden)
        {
            return warden.Map.mapPawns.PrisonersOfColony
                .Where(p => p.guest.PrisonerIsSecure && 
                           p.story.traits.HasTrait(TraitDefOf.Cannibal) &&
                           !p.health.hediffSet.HasHediff(ZhulDefOf.ZHUL_FirstCurl))
                .FirstOrDefault();
        }
    }
    
    // 4. Bone Comb Altar Component - Passive Conversion
    public class CompBoneWhispers : ThingComp
    {
        private int tickCounter = 0;
        private const int CheckInterval = 2500; // Check every ~1 in-game hour
        
        public CompProperties_BoneWhispers Props => (CompProperties_BoneWhispers)props;
        
        public override void CompTick()
        {
            tickCounter++;
            if (tickCounter >= CheckInterval)
            {
                tickCounter = 0;
                DoWhisperEffect();
            }
        }
        
        private void DoWhisperEffect()
        {
            var pawns = GenRadial.RadialDistinctThingsAround(parent.Position, parent.Map, Props.range, true)
                .OfType<Pawn>()
                .Where(p => p.IsColonist && 
                           !p.story.traits.HasTrait(TraitDefOf.Cannibal) &&
                           p.needs?.mood != null);
            
            foreach (Pawn pawn in pawns)
            {
                // Small chance to gain cannibal inclinations
                if (Rand.Chance(Props.ideologyConversionRate))
                {
                    pawn.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_BoneWhispers);
                    
                    // Very small chance to gain cannibal trait
                    if (Rand.Chance(0.01f))
                    {
                        pawn.story.traits.GainTrait(new Trait(TraitDefOf.Cannibal));
                        Messages.Message("ZHUL_BoneWhispersConversion".Translate(pawn.NameShortColored), pawn, MessageTypeDefOf.PositiveEvent);
                    }
                }
            }
        }
    }
    
    public class CompProperties_BoneWhispers : CompProperties
    {
        public float ideologyConversionRate = 0.05f;
        public float range = 10f;
        
        public CompProperties_BoneWhispers()
        {
            compClass = typeof(CompBoneWhispers);
        }
    }
    
    // 5. Vow of Bone Comb - Blood Oath System
    public class Gizmo_VowOfBoneComb : Gizmo
    {
        private Pawn caster;
        
        public Gizmo_VowOfBoneComb(Pawn caster)
        {
            this.caster = caster;
            defaultLabel = "Vow of Bone Comb";
            defaultDesc = "Force a prisoner to swear a blood oath to the Zhul clan";
            icon = ContentFinder<Texture2D>.Get("UI/Commands/ZhulVowOfBoneComb");
        }
        
        public override void ProcessInput(Event ev)
        {
            base.ProcessInput(ev);
            
            var prisoners = caster.Map.mapPawns.PrisonersOfColony.Where(p => p.guest.PrisonerIsSecure);
            
            if (!prisoners.Any())
            {
                Messages.Message("No suitable prisoners for the vow", MessageTypeDefOf.RejectInput);
                return;
            }
            
            Find.Targeter.BeginTargeting(TargetingParameters.ForPawns(), delegate(LocalTargetInfo target)
            {
                if (target.Pawn?.IsPrisoner == true)
                {
                    ApplyVow(target.Pawn);
                }
            });
        }
        
        private void ApplyVow(Pawn prisoner)
        {
            prisoner.health.AddHediff(ZhulDefOf.ZHUL_BoundByCurl);
            Messages.Message("ZHUL_VowApplied".Translate(prisoner.NameShortColored), prisoner, MessageTypeDefOf.PositiveEvent);
        }
    }
    
    // 6. Child Spawning Component for Pregnancy
    public class HediffComp_SpawnChild : HediffComp
    {
        public HediffCompProperties_SpawnChild Props => (HediffCompProperties_SpawnChild)props;
        
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Severity >= Props.spawnAtSeverity)
            {
                SpawnChild();
                parent.pawn.health.RemoveHediff(parent);
            }
        }
        
        private void SpawnChild()
        {
            PawnGenerationRequest request = new PawnGenerationRequest(
                Props.childPawnKind,
                Faction.OfPlayer,
                PawnGenerationContext.NonPlayer,
                forceGenerateNewPawn: true,
                developmentalStages: DevelopmentalStage.Child
            );
            
            Pawn child = PawnGenerator.GeneratePawn(request);
            
            IntVec3 spawnPos = CellFinder.RandomClosewalkCellNear(parent.pawn.Position, parent.pawn.Map, 3);
            GenSpawn.Spawn(child, spawnPos, parent.pawn.Map);
            
            Messages.Message("ZHUL_ChildBorn".Translate(child.NameShortColored), child, MessageTypeDefOf.PositiveEvent);
        }
    }
    
    public class HediffCompProperties_SpawnChild : HediffCompProperties
    {
        public PawnKindDef childPawnKind;
        public float spawnAtSeverity = 1.0f;
        
        public HediffCompProperties_SpawnChild()
        {
            compClass = typeof(HediffComp_SpawnChild);
        }
    }
}