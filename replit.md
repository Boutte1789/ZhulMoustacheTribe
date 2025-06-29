# Zhul Tribe RimWorld Mod

## Overview

The Zhul Tribe is a custom RimWorld mod that introduces a new tribal faction with unique cultural traits, including moustache worship, ritual feasting, and bone-based technology. The mod is built as a .NET Standard 2.0 class library that integrates with RimWorld's modding framework through the Krafs.Rimworld.Ref package.

## System Architecture

### Frontend Architecture
- **Graphics System**: Custom atlas-based sprite system using a single 4096x4096 texture sheet
- **Audio Integration**: Placeholder system for tribal sound effects including chants, drumming, and ritual sounds
- **UI Integration**: Extends RimWorld's existing UI framework for faction interactions

### Backend Architecture
- **Language**: C# targeting .NET Standard 2.0
- **Framework**: RimWorld modding API via Krafs.Rimworld.Ref (v1.4.3641)
- **Build System**: MSBuild with NuGet package management
- **Assembly Output**: Single DLL deployed to RimWorld's Assemblies folder

## Key Components

### Core Mod Structure
- **Source Code**: Located in `Source/ZhulTribe/` directory
- **Compiled Assembly**: Output to `Assemblies/ZhulTribe.dll`
- **Dependencies**: RimWorld reference libraries and .NET Standard 2.0

### Graphics System
- **Atlas Implementation**: Custom sprite atlas system for efficient texture management
- **Sprite Categories**: 
  - Pawn variants (AlienHumanoid, CurledSavage, SpiritEater, Child)
  - Effects (OilSwirl mote)
  - Buildings (BoneAltar)
- **Texture Format**: Single 4096x4096 PNG atlas with predefined sprite positions

### Audio System
- **Sound Categories**: Ritual chants, bone drumming, spirit prayers, war cries, grooming rituals, funeral rites, tribal ambience
- **Format Support**: .ogg (preferred), .wav, .mp3
- **Integration**: C# code hooks for triggering sounds during gameplay events

### Build Configuration
- **Development Tools**: Includes Python script for generating test atlas
- **Deployment**: Automatic compilation to RimWorld assemblies directory
- **Debug Support**: Both Debug and Release build configurations available

## Data Flow

1. **Mod Loading**: RimWorld loads the compiled assembly from `Assemblies/ZhulTribe.dll`
2. **Asset Loading**: Graphics system loads sprite atlas from `Textures/Zhul/ZhulSheet.png`
3. **Audio Loading**: Sound system references audio files in `Sounds/Zhul/` directory
4. **Runtime Integration**: Mod hooks into RimWorld's faction, pawn, and event systems

## External Dependencies

### Primary Dependencies
- **Krafs.Rimworld.Ref (v1.4.3641)**: Provides RimWorld API references for mod development
- **NETStandard.Library (v2.0.3)**: Core .NET Standard runtime libraries
- **Microsoft.NETCore.Platforms (v1.1.0)**: Platform-specific implementations

### Development Tools
- **Python PIL**: Used for generating test atlas textures
- **AI Audio Tools**: Recommended for generating tribal sound effects (ElevenLabs, Mubert, Suno AI)

## Deployment Strategy

### Build Process
1. Source code compilation targeting .NET Standard 2.0
2. Assembly output to `Assemblies/ZhulTribe.dll`
3. Dependency resolution via NuGet package management
4. Integration with RimWorld's mod loading system

### Asset Management
- **Graphics**: Single atlas approach for performance optimization
- **Audio**: Modular sound file system with placeholder implementation
- **Configuration**: XML-based definitions integrated with RimWorld's data systems

### Installation
- Deploy as standard RimWorld mod package
- Compatible with RimWorld v1.4.3641 and later
- No external runtime dependencies beyond RimWorld itself

## Changelog

- June 29, 2025. Initial setup

## User Preferences

Preferred communication style: Simple, everyday language.