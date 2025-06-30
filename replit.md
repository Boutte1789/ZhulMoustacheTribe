# ZHUL TRIBE MOD - THE CURLED ONES

## Overview

This is a complete RimWorld mod package that introduces the Zhul Tribe, a custom alien race featuring "The Curled Ones" - a cannibal faction with distinctive physical characteristics. The mod is built for RimWorld versions 1.4+ and requires the Harmony framework for proper functionality.

## System Architecture

### Mod Structure
The mod follows RimWorld's standard mod architecture with organized directories for different asset types:
- **About/**: Mod metadata and Steam Workshop assets
- **Assemblies/**: Compiled C# code with Harmony patches
- **Defs/**: XML definition files for game mechanics
- **Languages/**: Localization files
- **Sounds/**: Audio assets
- **Textures/**: Visual assets

### Framework Integration
- Built on Humanoid Alien Races (HAR) framework for alien race implementation
- Uses Harmony modding framework for runtime game modifications
- Compatible with all major RimWorld DLCs (Royalty, Ideology, Biotech, Anomaly)

## Key Components

### Race Definition System
- Custom Zhul alien race with olive-gray skin tone
- Gender-specific physical traits:
  - Males: Mandatory curly mustache (100% spawn rate)  
  - Females: Clean heads for bone-ash tattoo markings
- Custom head variations (Average/Narrow for both genders)

### Visual Assets
- Ultra-high resolution sprites (2048px body sprites)
- 11 PNG texture files including body sprites and head overlays
- Custom head variations for visual diversity

### Audio System
- 18 authentic tribal audio files
- Includes war cries, drums, chants, and ritual sounds
- Enhances immersion through cultural audio cues

### Faction Behavior
- Cannibal faction with unique behavioral patterns
- Custom rituals and cultural elements
- Tribal-themed recruitment and interaction systems

## Data Flow

### Game Integration
1. RimWorld loads mod definitions from Defs/ directory
2. Harmony patches modify game behavior at runtime
3. HAR framework handles alien race mechanics
4. Custom assets (textures, sounds) replace default content
5. Localization system provides translated text

### Asset Loading
- Textures loaded on demand for performance
- Audio files cached for quick access during gameplay
- XML definitions parsed at game startup

## External Dependencies

### Required Dependencies
- **Harmony Mod**: Core modding framework (Steam Workshop ID: 2009463077)
- **RimWorld**: Base game version 1.4 or 1.5

### Framework Dependencies  
- **Humanoid Alien Races (HAR)**: Framework for custom alien races
- Implicit compatibility with RimWorld DLC content

## Deployment Strategy

### Installation Process
1. Manual installation to RimWorld/Mods/ directory
2. Dependency management through Steam Workshop
3. Load order configuration (Harmony must load before ZhulTribe)
4. In-game mod activation through RimWorld's mod menu

### Distribution
- Complete package distribution (no separate downloads required)
- Steam Workshop compatibility for easy installation
- Cross-platform support (Windows, Mac, Linux)

## Changelog
- June 29, 2025: Initial setup and ChatGPT review fixes completed
  - Fixed all critical errors (❌): removed empty folders, resolved naming conflicts
  - Added settlement naming grammar for authentic Zhul-themed world generation
  - Balanced gameplay stats and limited overpowered mood bonuses
  - Added RimWorld 1.6 compatibility for upcoming Odyssey expansion
  - Production-ready: 325MB package with 34 XML files, 33 textures (15 new body sprites), 18 audio files
  - Integrated new body sprites with proper RimWorld naming conventions (_south, _north, _east)
  - Added support for Fat, Female, Hulk, Male, and Thin body types with directional sprites
  - Final polish: Added mod settings icon, optimized for Steam Workshop release
- June 30, 2025: Final review fixes and optimization completed
  - Fixed critical missing moustache east texture (prevents pink squares/crashes)
  - Added crafting recipe for Sacrificial Dagger (issue 3.3)
  - Cleaned up development artifacts and temp files safely
  - Final package: 325MB with 33 textures, production-ready for release
- June 30, 2025: HeadOverlayDef system implementation completed
  - Added proper HeadOverlayDef for moustache overlays with masking support
  - Created moustache mask texture for proper clipping
  - Integrated headOverlayRecords in PawnKindDefs for automatic moustache spawning
  - Full Big & Small compatibility with automatic scaling
  - Updated package: 326MB with 36 textures, 35 XML files
  - Added HAR and Big & Small Framework to modDependencies for proper dependency management

## User Preferences

Preferred communication style: Simple, everyday language.