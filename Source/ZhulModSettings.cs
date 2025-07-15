using HugsLib;
using HugsLib.Settings;
using RimWorld;
using Verse;
using UnityEngine;
using System.Linq;

namespace ZhulTribe
{
    // Main mod class using Verse.Mod
    public class ZhulTribeMod : Mod
    {
        public static ZhulModSettings settings;

        public ZhulTribeMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ZhulModSettings>();

            // Preload custom eye overlay texture
            ZhulTextureCache.EyeOverlayTex = ContentFinder<Texture2D>.Get("Things/Pawn/Humanlike/Heads/EyeOverlays/zhulalien_eyes_south", true);

            // Log error if texture is missing
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

            listingStandard.Label("Audio Volume: " + settings.audioVolume.ToString("F1"));
            settings.audioVolume = listingStandard.Slider(settings.audioVolume, 0f, 2f);

            listingStandard.Gap();

            listingStandard.Label("Recruitment Difficulty: " + settings.recruitmentDifficulty);
            if (listingStandard.ButtonText(settings.recruitmentDifficulty))
            {
                var options = new string[] { "Easy", "Normal", "Hard", "Extremely Hard" };
                Find.WindowStack.Add(new FloatMenu(options.Select(opt => new FloatMenuOption(opt, () => settings.recruitmentDifficulty = opt)).ToList()));
            }

            listingStandard.Gap();
            listingStandard.CheckboxLabeled("Enable Terror Effects", ref settings.enableTerrorEffects);

            listingStandard.Gap();
            listingStandard.Label("Cannibal Mood Bonus: " + settings.cannibalMoodBonus);
            settings.cannibalMoodBonus = (int)listingStandard.Slider(settings.cannibalMoodBonus, 5, 20);

            listingStandard.End();
        }

        public override string SettingsCategory()
        {
            return "Zhul Tribe - The Curled Ones";
        }
    }

    // Settings storage class
    public class ZhulModSettings : ModSettings
    {
        public float audioVolume = 1f;
        public string recruitmentDifficulty = "Normal";
        public bool enableTerrorEffects = true;
        public int cannibalMoodBonus = 10;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref audioVolume, "audioVolume", 1f);
            Scribe_Values.Look(ref recruitmentDifficulty, "recruitmentDifficulty", "Normal");
            Scribe_Values.Look(ref enableTerrorEffects, "enableTerrorEffects", true);
            Scribe_Values.Look(ref cannibalMoodBonus, "cannibalMoodBonus", 10);
        }
    }
}
