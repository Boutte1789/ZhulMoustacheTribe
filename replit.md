# RimWorld Zhul Tribe Mod

## Overview

The Zhul Tribe mod is a custom alien race modification for RimWorld, introducing "The Curled Ones" - a cannibal faction with distinctive olive-gray skin and unique gender-specific traits. This mod adds a complete alien race with custom sprites, faction behaviors, and full DLC compatibility.

## System Architecture

### Mod Structure
- **Type**: RimWorld XML/C# mod using Harmony framework
- **Target Platform**: RimWorld 1.4/1.5
- **Architecture Pattern**: Modular XML definitions with Harmony patches
- **Asset Management**: High-resolution sprite system (2048px body sprites)

### Core Dependencies
- **Harmony Framework**: Required for runtime patching and mod compatibility
- **RimWorld Core**: Base game systems for pawn generation, faction management, and rendering

## Key Components

### Race Definition System
- **Pawn Generation**: Custom alien race with olive-gray skin tone
- **Gender Differentiation**: 
  - Males: Clean heads with mandatory curly moustaches (100% spawn rate)
  - Females: Clean base suitable for bone-ash markings
- **Head Variations**: Average and Narrow head types for both genders

### Faction Management
- **Faction Type**: Cannibal faction with unique behavioral traits
- **Spawning System**: Integrated with RimWorld's faction spawning mechanics
- **AI Behaviors**: Custom traits and faction-specific behaviors

### Visual Assets
- **Sprite Resolution**: Ultra-high 2048px body sprites for enhanced visual quality
- **Head System**: Custom head variations with gender-specific styling
- **Rendering Pipeline**: Integration with RimWorld's pawn rendering system

## Data Flow

1. **Mod Loading**: Harmony patches applied during game initialization
2. **Pawn Generation**: Custom race definitions processed by RimWorld's pawn system
3. **Faction Spawning**: Zhul tribe generated according to faction spawning rules
4. **Asset Rendering**: High-resolution sprites loaded and rendered through game's graphics pipeline

## External Dependencies

### Required Dependencies
- **Harmony (Steam Workshop ID: 2009463077)**: Essential for mod functionality and compatibility

### Optional Compatibility
- **Humanoid Alien Races (HAR)**: Provides extended alien race framework compatibility
- **Big & Small Framework**: Enables automatic head size variations

### DLC Compatibility
- Full compatibility with all RimWorld DLCs (Royalty, Ideology, Biotech, Anomaly)

## Deployment Strategy

### Installation Process
1. Manual file extraction to RimWorld/Mods/ directory
2. Steam Workshop dependency management for Harmony
3. Mod load order configuration (Harmony → Core → DLCs → ZhulTribe → Other mods)

### Quality Assurance
- Pink texture fallback detection for missing assets
- Crash prevention through proper Harmony integration
- Faction spawning verification systems

## Changelog

```
Changelog:
- June 29, 2025. Initial setup
```

## User Preferences

```
Preferred communication style: Simple, everyday language.
```