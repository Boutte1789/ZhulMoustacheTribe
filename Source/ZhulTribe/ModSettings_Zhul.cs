using UnityEngine;
using Verse;

namespace ZhulTribe.Settings
{
    public class ModSettings_Zhul : ModSettings
    {
        // Settings fields
        public float ritualFrequency = 1.0f;
        public float caravanLootMultiplier = 1.0f;
        public bool bloodOilVisual = true;
        public float audioVolume = 1.0f;
        public int raidFrequency = 2;
        public bool terrorEffects = true;
        public float recruitmentDifficulty = 1.0f;
        public float audioDistance = 1.0f;
        public bool advancedQuests = true;
        public bool customBackstories = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ritualFrequency, "ritualFrequency", 1.0f);
            Scribe_Values.Look(ref caravanLootMultiplier, "caravanLootMultiplier", 1.0f);
            Scribe_Values.Look(ref bloodOilVisual, "bloodOilVisual", true);
            Scribe_Values.Look(ref audioVolume, "audioVolume", 1.0f);
            Scribe_Values.Look(ref raidFrequency, "raidFrequency", 2);
            Scribe_Values.Look(ref terrorEffects, "terrorEffects", true);
            Scribe_Values.Look(ref recruitmentDifficulty, "recruitmentDifficulty", 1.0f);
            Scribe_Values.Look(ref audioDistance, "audioDistance", 1.0f);
            Scribe_Values.Look(ref advancedQuests, "advancedQuests", true);
            Scribe_Values.Look(ref customBackstories, "customBackstories", true);
            base.ExposeData();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);
            
            // Header
            listing.Label("Zhul Tribe - The Curled Ones Settings");
            listing.Gap();
            
            // Gameplay Section
            listing.Label("Gameplay Settings");
            listing.Gap(6f);
            
            listing.Label($"Ritual Frequency: {ritualFrequency:F1}");
            ritualFrequency = listing.Slider(ritualFrequency, 0.1f, 5.0f);
            
            listing.Label($"Caravan Loot Multiplier: {caravanLootMultiplier:F1}");
            caravanLootMultiplier = listing.Slider(caravanLootMultiplier, 0.5f, 3.0f);
            
            listing.Label($"Raid Frequency: {raidFrequency}");
            raidFrequency = (int)listing.Slider(raidFrequency, 1f, 5f);
            
            listing.Label($"Recruitment Difficulty: {recruitmentDifficulty:F1}");
            recruitmentDifficulty = listing.Slider(recruitmentDifficulty, 0.5f, 2.0f);
            
            listing.Gap();
            listing.Label("Audio Settings");
            listing.Gap(6f);
            
            listing.Label($"Audio Volume: {audioVolume:F1}");
            audioVolume = listing.Slider(audioVolume, 0.0f, 2.0f);
            
            listing.Label($"Audio Distance: {audioDistance:F1}");
            audioDistance = listing.Slider(audioDistance, 0.5f, 3.0f);
            
            listing.Gap();
            listing.Label("Visual Settings");
            listing.Gap(6f);
            
            listing.CheckboxLabeled("Blood Oil Visuals", ref bloodOilVisual);
            listing.CheckboxLabeled("Terror Effects", ref terrorEffects);
            
            listing.Gap();
            listing.Label("Content Settings");
            listing.Gap(6f);
            
            listing.CheckboxLabeled("Advanced Quest Chains", ref advancedQuests);
            listing.CheckboxLabeled("Zhul Backstories", ref customBackstories);
            
            listing.End();
        }
    }
}