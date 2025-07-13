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

        public override void MapLoaded() {
            Log.Message("[ZhulBeardedTribe] MapLoaded lifecycle hook called.");
        }
    }
}
