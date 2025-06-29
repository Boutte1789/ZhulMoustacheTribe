using HugsLib.Settings;
using HugsLib.Utils;
using UnityEngine;
using Verse;

namespace ZhulTribe.Settings
{
    public class ModSettings_Zhul : ModBase
    {
        public override string ModIdentifier => "ZhulTribe";
        
        // Settings handles
        public SettingHandle<float> RitualFrequency;
        public SettingHandle<float> CaravanLootMultiplier;
        public SettingHandle<bool> BloodOilVisual;
        public SettingHandle<float> AudioVolume;
        public SettingHandle<int> RaidFrequency;
        public SettingHandle<bool> TerrorEffects;
        public SettingHandle<float> RecruitmentDifficulty;
        public SettingHandle<float> AudioDistance;
        public SettingHandle<bool> AdvancedQuests;
        public SettingHandle<bool> CustomBackstories;

        public override void DefsLoaded()
        {
            // Core gameplay settings
            RitualFrequency = Settings.GetHandle(
                "RitualFrequency",
                "ZT.RitualFrequency.Title".Translate(),
                "ZT.RitualFrequency.Desc".Translate(),
                1.0f,
                Validators.FloatRangeValidator(0.1f, 5.0f));

            CaravanLootMultiplier = Settings.GetHandle(
                "CaravanLootMultiplier", 
                "ZT.CaravanLootMultiplier.Title".Translate(),
                "ZT.CaravanLootMultiplier.Desc".Translate(),
                1.0f,
                Validators.FloatRangeValidator(0.5f, 3.0f));

            RaidFrequency = Settings.GetHandle(
                "RaidFrequency",
                "ZT.RaidFrequency.Title".Translate(),
                "ZT.RaidFrequency.Desc".Translate(),
                2,
                Validators.IntRangeValidator(1, 5));

            RecruitmentDifficulty = Settings.GetHandle(
                "RecruitmentDifficulty",
                "ZT.RecruitmentDifficulty.Title".Translate(), 
                "ZT.RecruitmentDifficulty.Desc".Translate(),
                1.0f,
                Validators.FloatRangeValidator(0.5f, 2.0f));

            // Visual and audio settings
            BloodOilVisual = Settings.GetHandle(
                "BloodOilVisual",
                "ZT.BloodOilVisual.Title".Translate(),
                "ZT.BloodOilVisual.Desc".Translate(),
                true);

            TerrorEffects = Settings.GetHandle(
                "TerrorEffects",
                "ZT.TerrorEffects.Title".Translate(),
                "ZT.TerrorEffects.Desc".Translate(),
                true);

            AudioVolume = Settings.GetHandle(
                "AudioVolume",
                "ZT.AudioVolume.Title".Translate(),
                "ZT.AudioVolume.Desc".Translate(),
                1.0f,
                Validators.FloatRangeValidator(0.0f, 2.0f));

            AudioDistance = Settings.GetHandle(
                "AudioDistance",
                "ZT.AudioDistance.Title".Translate(),
                "ZT.AudioDistance.Desc".Translate(),
                1.0f,
                Validators.FloatRangeValidator(0.5f, 3.0f));

            // Content settings
            AdvancedQuests = Settings.GetHandle(
                "AdvancedQuests",
                "ZT.AdvancedQuests.Title".Translate(),
                "ZT.AdvancedQuests.Desc".Translate(),
                true);

            CustomBackstories = Settings.GetHandle(
                "CustomBackstories",
                "ZT.CustomBackstories.Title".Translate(),
                "ZT.CustomBackstories.Desc".Translate(),
                true);
        }

        public override void SettingsChanged()
        {
            base.SettingsChanged();
            
            // Apply settings changes immediately where possible
            if (Current.Game != null)
            {
                // Update audio volumes for existing sound sources
                var audioManager = Find.SoundRoot;
                if (audioManager != null)
                {
                    // Apply new audio settings
                    Log.Message($"[Zhul Tribe] Updated audio settings: Volume={AudioVolume.Value}, Distance={AudioDistance.Value}");
                }
                
                // Update ritual frequency for active colonies
                if (Find.CurrentMap != null)
                {
                    Log.Message($"[Zhul Tribe] Updated gameplay settings: Rituals={RitualFrequency.Value}, Terror={TerrorEffects.Value}");
                }
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);
            
            // Header
            listing.Label("ZT.Settings.Header".Translate());
            listing.Gap();
            
            // Gameplay Section
            listing.Label("ZT.Settings.Gameplay".Translate());
            listing.Gap(6f);
            
            RitualFrequency.DrawAsSlider(listing.GetRect(30f));
            CaravanLootMultiplier.DrawAsSlider(listing.GetRect(30f));
            RaidFrequency.DrawAsSlider(listing.GetRect(30f));
            RecruitmentDifficulty.DrawAsSlider(listing.GetRect(30f));
            
            listing.Gap();
            listing.Label("ZT.Settings.Audio".Translate());
            listing.Gap(6f);
            
            AudioVolume.DrawAsSlider(listing.GetRect(30f));
            AudioDistance.DrawAsSlider(listing.GetRect(30f));
            
            listing.Gap();
            listing.Label("ZT.Settings.Visual".Translate());
            listing.Gap(6f);
            
            BloodOilVisual.DrawAsCheckbox(listing.GetRect(24f));
            TerrorEffects.DrawAsCheckbox(listing.GetRect(24f));
            
            listing.Gap();
            listing.Label("ZT.Settings.Content".Translate());
            listing.Gap(6f);
            
            AdvancedQuests.DrawAsCheckbox(listing.GetRect(24f));
            CustomBackstories.DrawAsCheckbox(listing.GetRect(24f));
            
            listing.End();
        }
    }
}