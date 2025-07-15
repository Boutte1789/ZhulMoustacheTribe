using HugsLib;
using Verse;
using UnityEngine;
using System.Linq;

namespace ZhulTribe
{
    public class ZhulTribeMod : ModBase
    {
        public static ZhulModSettings settings;

        public override string ModIdentifier => "ZhulTribe";

        public override void DefsLoaded()
        {
            base.DefsLoaded();

            settings = GetSettings<ZhulModSettings>();

            // Preload custom eye overlay texture (South only, lowercase, correct folder)
            ZhulTextureCache.EyeOverlayTex = ContentFinder<Texture2D>
                .Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", true);

            Texture2D tex = ContentFinder<Texture2D>
                .Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", false);
            if (tex == null)
            {
                Log.Error("[Zhul Mod] Eye overlay texture not found!");
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);

            listing.Label("Audio Volume: " + settings.audioVolume.ToString("F1"));
            settings.audioVolume = listing.Slider(settings.audioVolume, 0f, 2f);

            listing.Gap();

            listing.Label("Recruitment Difficulty: " + settings.recruitmentDifficulty);
            if (listing.ButtonText(settings.recruitmentDifficulty))
            {
                var options = new string[] { "Easy", "Normal", "Hard", "Extremely Hard" };
                Find.WindowStack.Add(new FloatMenu(options.Select(opt => new FloatMenuOption(opt, () => settings.recruitmentDifficulty = opt)).ToList()));
            }

            listing.Gap();

            listing.CheckboxLabeled("Enable Terror Effects", ref settings.enableTerrorEffects);

            listing.Gap();

            listing.Label("Cannibal Mood Bonus: " + settings.cannibalMoodBonus);
            settings.cannibalMoodBonus = (int)listing.Slider(settings.cannibalMoodBonus, 5, 20);

            listing.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Zhul Tribe - The Curled Ones";
        }

        // Optional custom icon:
        // public override Texture2D SettingsCategoryIcon()
        // {
        //     return ContentFinder<Texture2D>.Get("UI/ZhulModIcon", false);
        // }
    }
}