using HugsLib;
using Verse;
using UnityEngine;
using System.Linq;

namespace ZhulTribe
{
    public class ZhulTribeMod : ModBase
    {
        // Persisted settings instance
        public static ZhulModSettings settings;

        // Identifier for the HugsLib mod menu
        public override string ModIdentifier => "ZhulTribe";

        public override void DefsLoaded()
        {
            base.DefsLoaded();

            // Initialize settings from config
            settings = GetSettings<ZhulModSettings>();

            // Preload custom eye overlay
            ZhulTextureCache.EyeOverlayTex = ContentFinder<Texture2D>
                .Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", true);

            if (ZhulTextureCache.EyeOverlayTex == null)
                Log.Error("[Zhul Mod] Eye overlay texture not found!");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label($"Audio Volume: {settings.audioVolume:F1}");
            settings.audioVolume = listing.Slider(settings.audioVolume, 0f, 2f);

            listing.Gap();

            listing.Label($"Recruitment Difficulty: {settings.recruitmentDifficulty}");
            if (listing.ButtonText(settings.recruitmentDifficulty))
            {
                var options = new[] { "Easy", "Normal", "Hard", "Extremely Hard" };
                Find.WindowStack.Add(new FloatMenu(
                    options.Select(o => new FloatMenuOption(o, () => settings.recruitmentDifficulty = o))
                           .ToList()
                ));
            }

            listing.Gap();
            listing.CheckboxLabeled("Enable Terror Effects", ref settings.enableTerrorEffects);

            listing.Gap();
            listing.Label($"Cannibal Mood Bonus: {settings.cannibalMoodBonus}");
            settings.cannibalMoodBonus = (int)listing.Slider(settings.cannibalMoodBonus, 5, 20);

            listing.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Zhul Tribe - The Curled Ones";
        }
    }
}
