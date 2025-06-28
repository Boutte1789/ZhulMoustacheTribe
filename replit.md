# Zhul Tribe - The Curled Ones

## Overview

This is a RimWorld mod that introduces the Zhul tribe, a unique cannibal-worshipping faction with elaborate moustache rituals. The mod adds a custom alien humanoid race with distinctive cultural elements, gameplay mechanics, and faction behavior to enhance the RimWorld experience.

## System Architecture

### Mod Structure
- **Platform**: RimWorld modding framework (XML-based definitions with C# scripting support)
- **Version Compatibility**: RimWorld 1.4 and 1.5
- **Dependencies**: Harmony framework (required)
- **Optional Enhancement**: Ideology DLC integration

### Core Components
- **Race Definition**: Custom alien humanoid race (Zhul) with unique physiological traits
- **Faction System**: Hostile tribal faction with specialized behavior patterns
- **Trait System**: Custom traits for cannibal worship and moustache rituals
- **Item System**: Sacred and cultural items specific to Zhul society
- **Event System**: Special incidents and ritual events

## Key Components

### Race System (ZHUL_AlienHumanoid)
- Olive-gray skin with elongated skull structure
- Adult moustache requirement (spiritual significance)
- Enhanced resistance to food poisoning and toxins
- Carnivorous dietary restrictions

### Faction Behavior (ZhulTribe)
- Hostile relationship with player colonies
- Preference for night raids
- Tribal tech level with stone age equipment
- Settlement size of 8 pawns per group

### Pawn Types
1. **Curled Savage** - Basic tribal warrior
2. **Spirit-Eater** - Enhanced cannibal specialist
3. **Bone-Chief** - Leadership role with ceremonial items

### Custom Traits
- **ZHUL_Cannibal**: Provides mood bonuses for consuming humanlike meat
- **ZHUL_MoustacheRitual**: Daily grooming ceremonies with mood effects

### Items and Equipment
- **Moustache Oil**: Crafted from jungle orchids for rituals
- **Sacrificial Dagger**: Ritual weapon for ceremonies
- **Bone Necklace**: Status symbol and protective gear
- **Bone Diadems**: Leadership insignia for bone-chiefs

## Data Flow

### Mood System Integration
1. **Dietary Preferences**: Humanlike meat (+10 mood), regular meat (+3 mood), plant matter (starvation penalty)
2. **Ritual Completion**: Moustache grooming provides temporary mood buffs
3. **Cultural Violations**: Penalties for not following cannibal practices

### Event Triggers
- **Ritual Feast**: Triggered by available corpses in vicinity
- **Moustache Ceremonies**: Daily scheduled events for tribe members
- **Raid Behavior**: Night preference with cannibal motivations

## External Dependencies

### Required
- **Harmony Framework**: Essential for C# code patching and mod functionality
- **RimWorld Base Game**: Version 1.4 or 1.5

### Optional
- **Ideology DLC**: Enhances ritual and belief system integration
- **Compatible with most other mods**: No major conflicts expected

## Deployment Strategy

### Installation Process
1. Extract mod folder to RimWorld/Mods directory
2. Enable mod in RimWorld mod manager
3. Ensure Harmony loads before this mod
4. Start new game for full faction integration

### File Organization
- XML definition files for races, factions, and items
- Texture assets for moustache variations and tribal appearance  
- C# assemblies for custom behavior (if needed)
- Localization files for multi-language support

## User Preferences

Preferred communication style: Simple, everyday language.

## Recent Progress

**✓ Complete Zhul Tribe Implementation**
- Built comprehensive RimWorld mod based on CoPilot conversation
- Implemented all core systems: race, traits, faction, items, events
- Added custom C# code for ritual feast mechanics
- All XML definitions follow RimWorld modding standards
- **Successfully compiled mod with .NET build system**
- Fixed all compilation errors and API compatibility issues
- Generated working ZhulTribe.dll assembly ready for RimWorld

**✓ Enhanced Sound Pack System**
- Implemented complete tribal chant and bone drumming sound framework
- Added 7 distinct sound categories with multiple variations each
- Integrated dynamic audio triggers for raids, rituals, and combat
- Enhanced moustache oil mechanics with audio feedback
- Created sound utility system for consistent audio management
- Successfully compiled enhanced mod with full audio integration

**✓ Custom Audio Files Integration**
- Trimmed user-provided audio files to exact specifications using ffmpeg
- ZHUL_RitualChant.wav (2.5s loopable), ZHUL_BoneDrums.wav (4.0s loopable)
- ZHUL_Prayer1.wav and ZHUL_Prayer2.wav (1.5s each for shamanic rituals)
- Updated all sound definitions to use authentic tribal audio files
- Integrated real chants and bone drumming into raid and ritual systems

## Changelog

Changelog:
- June 28, 2025. Initial setup and full Zhul tribe mod implementation