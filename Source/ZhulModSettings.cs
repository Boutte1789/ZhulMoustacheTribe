using Verse;
using UnityEngine;

namespace ZhulTribe
{
    public class ZhulModSettings : ModSettings
    {
        public float audioVolume = 1.0f;
        public string raidFrequency = "Normal";
        public int cannibalMoodBonus = 10;
        public string beardRitualFrequency = "Once per day";
        public bool enableTerrorEffects = true;
        public string recruitmentDifficulty = "Hard";
        public int audioTriggerDistance = 50;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref audioVolume, "audioVolume", 1.0f);
            Scribe_Values.Look(ref raidFrequency, "raidFrequency", "Normal");
            Scribe_Values.Look(ref cannibalMoodBonus, "cannibalMoodBonus", 10);
            Scribe_Values.Look(ref beardRitualFrequency, "beardRitualFrequency", "Once per day");
            Scribe_Values.Look(ref enableTerrorEffects, "enableTerrorEffects", true);
            Scribe_Values.Look(ref recruitmentDifficulty, "recruitmentDifficulty", "Hard");
            Scribe_Values.Look(ref audioTriggerDistance, "audioTriggerDistance", 50);
            base.ExposeData();
        }

        public float GetRecruitmentDifficultyMultiplier()
        {
            return recruitmentDifficulty switch
            {
                "Easy" => 1.3f,
                "Normal" => 1.0f,
                "Hard" => 0.8f,
                "Extremely Hard" => 0.6f,
                _ => 1.0f
            };
        }
    }
}