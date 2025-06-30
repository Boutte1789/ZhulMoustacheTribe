using HarmonyLib;
using Verse;
using System.Reflection;

namespace ZhulTribe
{
    // Main patch class with HarmonyAfter attribute for Big & Small compatibility
    [HarmonyPatch]
    [HarmonyAfter("harmony.bigsmall.core")]
    public static class ZhulTribeHarmonyPatches
    {
        // Static field cache for Graphic_Atlas reflection (optimization)
        private static FieldInfo _atlasFieldInfo;
        
        static ZhulTribeHarmonyPatches()
        {
            // Cache the reflection field for performance
            _atlasFieldInfo = typeof(Graphic_Atlas).GetField("atlas", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        // Example patch method (you can add specific patches here as needed)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Pawn), "SpawnSetup")]
        public static void Pawn_SpawnSetup_Postfix(Pawn __instance)
        {
            // Zhul-specific spawn setup if needed
            if (__instance?.def?.defName == "ZHUL_AlienHumanoid")
            {
                // Custom initialization for Zhul pawns
            }
        }
    }
}