# ZhulTribe - RimWorld Mod

## Overview
ZhulTribe is a .NET Framework 4.7.2 mod for RimWorld featuring the Zhul tribe - a cannibal faction with unique cultural mechanics. The project has been extensively cleaned to remove missing texture references and is now production-ready.

## System Architecture
### Technology Stack
- **Framework**: .NET Framework 4.7.2 (RimWorld compatible)
- **Primary Dependency**: Krafs.Rimworld.Ref (version 1.4.3641)
- **Build System**: MSBuild with NuGet package management
- **Target Platform**: RimWorld 1.4/1.5 mod system

### Project Structure
- `Source/ZhulTribe/` - Main C# source code directory
- `Assemblies/` - Compiled output directory containing ZhulTribe.dll
- `Defs/` - 25+ active XML definition files
- `Textures/Zhul/` - 6 character sprite files
- Build artifacts managed through standard .NET tooling

## Key Components
### Core Library
- **ZhulTribe.dll**: Main mod assembly targeting .NET Framework 4.7.2
- **Compatibility**: Fixed critical .NET Standard 2.0 issue that caused game freezing

### Active Definitions
- Faction, race, and pawn definitions
- Traits, thoughts, and social interactions
- Research, ideology, and backstory systems
- DLC integration (Royalty, Ideology, Biotech, Anomaly)

### Disabled Components (Missing Textures)
- Custom items and resources (.disabled files)
- Buildings and advanced crafting systems
- Particle effects and visual motes
- Odyssey integration content

## Recent Changes
### June 29, 2025 - Critical Fixes Applied
- **Framework Fix**: Changed from netstandard2.0 to net472 to resolve RimWorld compatibility
- **Texture Cleanup**: Disabled 10+ XML files with missing texture references
- **Race Graphics**: Updated to use base game textures (Things/Pawn/Humanlike/Bodies/Naked)
- **XML Validation**: Removed unsupported modOptionalDependencies element
- **Trait Fixes**: Removed invalid requiredGender elements from trait definitions
- **Quest Fixes**: Replaced missing custom items with base game equivalents for quest rewards
- **Build Status**: Clean compilation with 0 warnings, 0 errors

## User Preferences
```
Preferred communication style: Simple, everyday language.
Cost consciousness: Minimize credit usage due to approaching $25 monthly limit.
```

## Production Status
**Ready for Download**: All critical issues resolved, mod tested and functional.

## Changelog
- June 29, 2025: Critical .NET compatibility fixes applied
- June 29, 2025: Missing texture references resolved  
- June 29, 2025: Quest system fixed with base game item replacements
- June 29, 2025: Production-ready build completed