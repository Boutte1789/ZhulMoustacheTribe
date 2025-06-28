using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI.Group;

namespace ZhulTribe
{
    public class IncidentWorker_RitualFeast : IncidentWorker_RaidEnemy
    {
        protected override bool FactionCanBeGroupSource(Faction f, Map map, bool desperate = false)
        {
            return base.FactionCanBeGroupSource(f, map, desperate) && f.def.defName == "ZHUL_Tribe";
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!TryResolveRaidFaction(parms))
            {
                return false;
            }

            // Check if there are humanlike corpses available
            List<Corpse> humanlikeCorpses = map.listerThings.ThingsInGroup(ThingRequestGroup.Corpse)
                .Cast<Corpse>()
                .Where(x => x.InnerPawn.RaceProps.Humanlike && !x.InnerPawn.RaceProps.Animal)
                .ToList();

            bool hasCorpses = humanlikeCorpses.Count > 0;

            PawnGroupKindDef combat = PawnGroupKindDefOf.Combat;
            RCellFinder.TryFindRandomPawnEntryCell(out IntVec3 result, map, CellFinder.EdgeRoadChance_Hostile);
            
            // Adjust raid size based on whether corpses are available
            if (hasCorpses)
            {
                parms.points *= 0.75f; // Smaller raid if they can get what they want peacefully
            }
            else
            {
                parms.points *= 1.25f; // Larger raid if they need to take by force
            }

            PawnGroupMakerParms groupParms = new PawnGroupMakerParms();
            groupParms.groupKind = combat;
            groupParms.faction = parms.faction;
            groupParms.points = parms.points;
            groupParms.tile = map.Tile;
            
            List<Pawn> list = PawnGroupMakerUtility.GeneratePawns(groupParms).ToList();
            if (list.Count == 0)
            {
                Log.Error("Got no pawns spawning raid from parms " + parms);
                return false;
            }

            parms.raidArrivalMode.Worker.Arrive(list, parms);

            // Create the ritual feast lord job
            LordMaker.MakeNewLord(parms.faction, new LordJob_AssaultColony(parms.faction, true, true, false, false, true), map, list);

            // Play appropriate ritual sounds using the sound utility
            ZhulSoundUtility.PlayRaidAmbience(map, hasCorpses);

            foreach (Pawn pawn in list)
            {
                if (pawn.def.defName == "ZHUL_AlienHumanoid")
                {
                    // Play pawn-specific sounds
                    if (pawn.kindDef.defName == "ZHUL_SpiritEater")
                    {
                        ZhulSoundUtility.PlaySpiritEaterPrayer(pawn);
                    }
                    else if (pawn.kindDef.defName == "ZHUL_BoneChief")
                    {
                        ZhulSoundUtility.PlayBoneChiefWarCry(pawn);
                    }

                    if (hasCorpses)
                    {
                        // They're anticipating a successful feast
                        pawn.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_RitualFeastSuccess);
                    }
                    else
                    {
                        // They're frustrated by lack of proper offerings
                        pawn.needs.mood.thoughts.memories.TryGainMemory(ZhulDefOf.ZHUL_RitualFeastFailure);
                    }
                }
            }

            // Custom letter text based on corpse availability
            string letterText;
            if (hasCorpses)
            {
                letterText = "A group of Zhul spirit-eaters has arrived for their ritual feast. They have detected the presence of humanlike corpses in your settlement and approach with ceremonial bone daggers raised. Their ornate moustaches glisten with ritual oils as they prepare to claim their sacred offerings.\n\nYou could attempt to negotiate, offering the corpses in exchange for temporary peace, or prepare to defend your settlement from their cannibal raid.";
            }
            else
            {
                letterText = "A group of Zhul spirit-eaters has arrived seeking flesh for their ritual feast. Finding no suitable corpses in your settlement, they have entered a spiritual frenzy. Their bone-crowned leader demands that your people provide the necessary sacrifices, or they will take what they need by force.\n\nWith no offerings to appease them, violence seems inevitable.";
            }

            Find.LetterStack.ReceiveLetter("Zhul Ritual Feast", letterText, LetterDefOf.ThreatBig, new TargetInfo(result, map), parms.faction);

            return true;
        }

        protected override void ResolveRaidPoints(IncidentParms parms)
        {
            if (parms.points <= 0f)
            {
                parms.points = Rand.ByCurve(PointsFactorCurve) * Find.Storyteller.difficulty.threatScale;
            }
        }

        public override void ResolveRaidStrategy(IncidentParms parms, PawnGroupKindDef groupKind)
        {
            Map map = (Map)parms.target;
            if (parms.raidStrategy != null)
            {
                return;
            }

            // Zhul prefer direct assault for their ritual needs
            parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
        }

        private static readonly SimpleCurve PointsFactorCurve = new SimpleCurve
        {
            new CurvePoint(0f, 0f),
            new CurvePoint(30f, 1f),
            new CurvePoint(50f, 2f),
            new CurvePoint(100f, 3f),
            new CurvePoint(200f, 4f),
            new CurvePoint(300f, 5f),
            new CurvePoint(500f, 6f),
            new CurvePoint(1000f, 10f),
            new CurvePoint(2000f, 15f),
            new CurvePoint(5000f, 25f)
        };
    }
}
