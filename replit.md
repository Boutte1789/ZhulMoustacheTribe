# ZhulTribe RimWorld Mod - The Curled Ones

## Overview

ZhulTribe is a complete RimWorld modification that introduces a custom alien race called the Zhul - "The Curled Ones". This mod adds a new playable faction with distinctive visual characteristics, custom behaviors, and cultural elements centered around cannibalistic rituals. The mod is designed as a standalone package that integrates with RimWorld's existing systems through the Humanoid Alien Races (HAR) framework.

## System Architecture

### Mod Framework Architecture
- **Base Framework**: RimWorld modding system using XML definitions and C# patches
- **Core Dependency**: Harmony mod for runtime code patching and method injection
- **Integration Layer**: Humanoid Alien Races (HAR) framework for alien race implementation
- **Asset Pipeline**: High-resolution sprite system with 2048px body textures

### File Structure Pattern
The mod follows RimWorld's standard mod directory structure:
- Root mod folder containing mod metadata
- Subdirectories for different content types (likely Defs/, Textures/, Patches/)
- Standard RimWorld mod manifest files

## Key Components

### Race Definition System
- **Zhul Alien Race**: Custom humanoid species with olive-gray skin tone
- **Gender-Specific Features**: 
  - Males: Mandatory curly moustache (100% spawn rate)
  - Females: Clean head design optimized for bone-ash tattoo overlays
- **Head Variations**: Custom head types (Average/Narrow) for both genders

### Visual Asset System
- **Ultra-High Resolution Graphics**: 2048px body sprite resolution (significantly higher than RimWorld's standard)
- **Custom Texture Pipeline**: Specialized rendering for detailed character appearance
- **Scalable Asset Architecture**: Supports multiple head variations and gender-specific features

### Faction Behavior System
- **Cannibal Faction**: Custom AI behaviors and social interactions
- **Ritual System**: Unique cultural practices and ceremonies
- **Social Dynamics**: Specialized faction relationships and diplomatic options

## Data Flow

### Character Generation Pipeline
1. RimWorld character generation system calls HAR framework
2. HAR framework applies Zhul race definitions
3. Gender-specific feature application (moustache for males, clean heads for females)
4. High-resolution texture loading and application
5. Final character rendering with custom sprites

### Faction Interaction Flow
1. World generation places Zhul faction settlements
2. Custom faction AI governs behavior decisions
3. Cannibal-specific interactions trigger during encounters
4. Ritual system activates during appropriate game events

## External Dependencies

### Required Dependencies
- **RimWorld**: Base game (versions 1.4 or 1.5 supported)
- **Harmony Mod**: Runtime code patching framework (Steam Workshop ID: 2009463077)

### Framework Integration
- **Humanoid Alien Races (HAR)**: Alien race implementation framework
- **RimWorld DLC Support**: Full compatibility with Royalty, Ideology, Biotech, and Anomaly expansions

### Load Order Requirements
- Harmony must be loaded before ZhulTribe in the mod load order
- HAR framework dependencies resolved automatically

## Deployment Strategy

### Distribution Model
- **Complete Package**: Self-contained mod folder ready for installation
- **Manual Installation**: Direct folder copy to RimWorld/Mods/ directory
- **Steam Workshop Integration**: Harmony dependency available through Steam Workshop

### Installation Process
1. User copies mod folder to RimWorld mods directory
2. User installs Harmony from Steam Workshop
3. Both mods enabled in RimWorld mod configuration
4. Load order verification (Harmony before ZhulTribe)

### Compatibility Strategy
- Multi-version support (RimWorld 1.4 and 1.5)
- Full DLC compatibility across all major expansions
- HAR framework ensures compatibility with other alien race mods

## Changelog

```
Changelog:
- June 29, 2025. Initial setup
```

## User Preferences

```
Preferred communication style: Simple, everyday language.
```