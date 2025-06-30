using Verse;
using UnityEngine;

namespace ZhulTribe
{
    public class ZhulTribeMod : Mod
    {
        public ZhulTribeMod(ModContentPack content) : base(content)
        {
            // Initialize mod settings if needed
        }

        // Mod icon override for settings window (Issue F)
        public override Texture2D SettingsCategoryIcon()
        {
            return ContentFinder<Texture2D>.Get("UI/ZhulModIcon", false);
        }
    }
}