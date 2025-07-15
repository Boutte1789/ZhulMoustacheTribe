using HugsLib.Settings;
using Verse;

namespace ZhulTribe
{
    public class ZhulModSettings : ModSettings
    {
        public float audioVolume = 1f;
        public string recruitmentDifficulty = "Normal";
        public bool enableTerrorEffects = true;
        public int cannibalMoodBonus = 10;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref audioVolume, "audioVolume", 1f);
            Scribe_Values.Look(ref recruitmentDifficulty, "recruitmentDifficulty", "Normal");
            Scribe_Values.Look(ref enableTerrorEffects, "enableTerrorEffects", true);
            Scribe_Values.Look(ref cannibalMoodBonus, "cannibalMoodBonus", 10);
        }
    }
}
