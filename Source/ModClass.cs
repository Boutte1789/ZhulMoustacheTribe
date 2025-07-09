using Verse;
using UnityEngine;
using System.Linq;

namespace ZhulTribe
{
    public class ZhulTribeMod : Mod
    {
        ZhulModSettings settings;

        public ZhulTribeMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ZhulModSettings>();
            
            // Preload custom eye overlay texture (South only, lowercase, correct folder)
            ZhulTextureCache.EyeOverlayTex = ContentFinder<Texture2D>.Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", true);

            // Check for custom eye overlay texture and log error if missing
            Texture2D tex = ContentFinder<Texture2D>.Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", false);
            if (tex == null)
            {
                Log.Error("[Zhul Mod] Eye overlay texture not found!");
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            
            // Audio settings
            listingStandard.Label("Audio Volume: " + settings.audioVolume.ToString("F1"));
            settings.audioVolume = listingStandard.Slider(settings.audioVolume, 0f, 2f);
            
            listingStandard.Gap();
            
            // Recruitment difficulty
            listingStandard.Label("Recruitment Difficulty: " + settings.recruitmentDifficulty);
            if (listingStandard.ButtonText(settings.recruitmentDifficulty))
            {
                var options = new string[] { "Easy", "Normal", "Hard", "Extremely Hard" };
                Find.WindowStack.Add(new FloatMenu(options.Select(opt => new FloatMenuOption(opt, () => settings.recruitmentDifficulty = opt)).ToList()));
            }
            
            listingStandard.Gap();
            
            // Terror effects
            listingStandard.CheckboxLabeled("Enable Terror Effects", ref settings.enableTerrorEffects);
            
            listingStandard.Gap();
            
            // Cannibal mood bonus
            listingStandard.Label("Cannibal Mood Bonus: " + settings.cannibalMoodBonus);
            settings.cannibalMoodBonus = (int)listingStandard.Slider(settings.cannibalMoodBonus, 5, 20);
            
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Zhul Tribe - The Curled Ones";
        }

        // Mod icon override for settings window
        public override Texture2D SettingsCategoryIcon()
        {
            return ContentFinder<Texture2D>.Get("UI/ZhulModIcon", false);
        }
    }
}