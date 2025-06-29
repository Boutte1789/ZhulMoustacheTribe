using UnityEngine;
using Verse;

namespace ZhulTribe
{
    public class ZhulTribeMod : Mod
    {
        public static ZhulTribeSettings settings;

        public ZhulTribeMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ZhulTribeSettings>();
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