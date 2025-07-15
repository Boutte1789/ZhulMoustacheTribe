using System.Linq;
using Verse;
using RimWorld;

namespace ZhulTribe.Genes
{
    public class Gene_CannibalRitualist : Gene
    {
        public override void Notify_IngestedThing(Thing thing, int numTaken)
        {
            base.Notify_IngestedThing(thing, numTaken);

            // Ensure this runs only for humanlike meat
            if (thing?.def?.ingestible?.foodType != null &&
                (thing.def.ingestible.foodType.Value.HasFlag(FoodTypeFlags.Meat) ||
                 thing.def.ingestible.foodType.Value.HasFlag(FoodTypeFlags.Corpse)))
            {
                if (!IsHumanlikeMeat(thing.def)) return;

                // Pawn must be in the same room as a ritual altar
                Room currentRoom = pawn.Position.GetRoom(pawn.Map);
                if (currentRoom == null) return;

                bool altarNearby = currentRoom.ContainedAndAdjacentThings
                    .OfType<Building>()
                    .Any(b => b.def.defName == "Building_ZhulRitualAltar"); // customize if needed

                if (!altarNearby) return;

                // Apply mood thought if not already present
                if (!pawn.needs.mood.thoughts.memories.Memories.Any(
                    t => t.def.defName == "Zhul_CannibalRitualBuff"))
                {
                    pawn.needs.mood.thoughts.memories.TryGainMemory(
                        DefDatabase<ThoughtDef>.GetNamed("Zhul_CannibalRitualBuff"), null);
                }

                // Apply hediff buff if not already present
                if (!pawn.health.hediffSet.HasHediff(HediffDef.Named("Zhul_Hediff_CannibalRitualEmpowerment")))
                {
                    var hediff = HediffMaker.MakeHediff(HediffDef.Named("Zhul_Hediff_CannibalRitualEmpowerment"), pawn);
                    pawn.health.AddHediff(hediff);
                }
            }
        }

        private bool IsHumanlikeMeat(ThingDef def)
        {
            // Standard check for humanlike meat types
            if (def == ThingDefOf.Meat_Human || def.defName.ToLower().Contains("meat_") && def.ingestible?.sourceDef?.race?.Humanlike == true)
                return true;

            // Handle cooked or modded humanlike meals
            if (def.ingestible?.sourceDef != null && def.ingestible.sourceDef.race?.Humanlike == true)
                return true;

            return false;
        }
    }
}
