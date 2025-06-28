# Big and Small Framework Integration Guide

## What We've Set Up

✓ **Added Framework Dependency** - Mod now requires "Big and Small - Framework" by RedMattis
✓ **Updated Race Definition** - Switched from atlas system to framework scaling
✓ **Created Framework Extension** - Defines how Zhul body types should scale
✓ **Renamed Existing Sprite** - Your sprite is now `ZhulBase_bp_male.png`

## What You Need to Do

### Option 1: Generate Only Base Sprites (Recommended)
Create **just 5 sprites** instead of 25:

1. **ZhulBase_bp_male.png** (already done ✓)
2. **ZhulBase_bp_female.png** 
3. **ZhulBase_bp_fat.png**
4. **ZhulBase_bp_hulk.png** 
5. **ZhulBase_bp_thin.png**

The framework will handle all character variants (Savage, Spirit-Eater, etc.) automatically by scaling these base sprites.

### Option 2: Full Character Set (Advanced)
If you want distinct looks for each character type, create:
- `ZhulSavage_bp_male.png`, `ZhulSavage_bp_female.png`, etc.
- `ZhulSpirit_bp_male.png`, `ZhulSpirit_bp_female.png`, etc. 
- `ZhulChild_bp_male.png`, `ZhulChild_bp_female.png`, etc.

## AI Generation Prompts

Use the **naked body prompts** I provided earlier, but name the files with `bp_` convention:
- Save as `ZhulBase_bp_male.png` instead of `ZhulBase_Male.png`
- Save as `ZhulBase_bp_female.png` instead of `ZhulBase_Female.png`

## Framework Benefits

✓ **Automatic Scaling** - Framework handles body type differences
✓ **Fewer Sprites** - Only need 5 instead of 25
✓ **Dynamic Adjustment** - Real-time scaling based on pawn stats
✓ **Better Performance** - Less texture memory usage
✓ **Future-Proof** - Works with framework updates automatically

## Dependencies Required

Players will need to install:
1. **Harmony** (already included)
2. **Big and Small - Framework** (newly added)
3. **Your Zhul Tribe Mod**

The framework will automatically detect and scale your `bp_` named sprites!