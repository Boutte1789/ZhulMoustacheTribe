using UnityEngine;
using Verse;

namespace ZhulTribe
{
    public class ZhulTribeMod : Mod
    {
        public static ModSettings_Zhul settings;

        public ZhulTribeMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings_Zhul>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Zhul Tribe";
        }
    }
}