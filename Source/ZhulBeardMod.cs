using HugsLib; 
using Verse;

namespace ZhulBeardedTribe {
    public class ZhulBeardMod : ModBase {
        public override string ModIdentifier => "ZhulBeardedTribe";

        public override void DefsLoaded() {
            Log.Message("[ZhulBeardedTribe] DefsLoaded lifecycle hook called.");
        }

        public override void WorldLoaded() {
            Log.Message("[ZhulBeardedTribe] WorldLoaded lifecycle hook called.");
        }

        // MapLoaded() is not a valid override in HugsLib.ModBase for RimWorld 1.5+
        // If you need map-specific initialization, see HugsLib documentation for alternatives.
    }
}
