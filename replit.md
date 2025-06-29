# Replit.md

## Overview

This repository contains a RimWorld mod called "ZhulTribe" - a .NET Standard 2.0 library that extends the RimWorld game with custom alien humanoid races, creatures, and structures. The mod is built using C# and targets the RimWorld modding framework through the Krafs.Rimworld.Ref package.

## System Architecture

### Core Framework
- **Target Framework**: .NET Standard 2.0
- **Game Integration**: RimWorld modding framework via Krafs.Rimworld.Ref (version 1.4.3641)
- **Build System**: Standard .NET compilation targeting game mod structure

### Asset Pipeline
- **Texture Management**: Custom sprite atlas system using 4096x4096 texture sheets
- **Asset Generation**: Python-based tooling for creating test atlases and texture management
- **Resource Organization**: Structured texture directory with mod-specific naming conventions

## Key Components

### 1. Mod Assembly
- **ZhulTribe.dll**: Main mod assembly containing game logic and definitions
- **Dependencies**: Integrated with RimWorld's core systems through official reference package
- **Versioning**: Version 1.0.0 with structured dependency management

### 2. Texture System
- **Atlas Structure**: Large-format sprite sheets (4096x4096) for efficient memory usage
- **Sprite Categories**:
  - AlienHumanoid: Primary alien race sprites (1024x1024 allocation)
  - CurledSavage: Secondary creature type (1024x1024 allocation)
  - SpiritEater: Special entity sprites (1024x1024 allocation)  
  - Child: Youth variant sprites (1024x1024 allocation)
  - OilSwirl: Environmental effects (256x256 allocation)
  - BoneAltar: Structure sprites (512x512 allocation)

### 3. Asset Development Tools
- **Python Utilities**: Automated atlas generation and testing tools
- **Color Coding**: Visual debugging system with distinct colors per sprite type
- **Development Workflow**: Streamlined texture creation and validation process

## Data Flow

1. **Mod Loading**: RimWorld loads ZhulTribe.dll during game initialization
2. **Asset Registration**: Texture atlas loaded and sprites registered with game systems
3. **Runtime Integration**: Custom races, creatures, and structures become available in-game
4. **Game Integration**: Mod content seamlessly integrates with RimWorld's existing systems

## External Dependencies

### Core Dependencies
- **Krafs.Rimworld.Ref (1.4.3641)**: Official RimWorld modding reference package
- **NETStandard.Library (2.0.3)**: Standard .NET library support
- **Microsoft.NETCore.Platforms (1.1.0)**: Platform abstraction layer

### Development Tools
- **PIL (Python Imaging Library)**: For texture atlas generation and manipulation
- **ImageDraw**: Graphics operations for test asset creation

## Deployment Strategy

### Mod Distribution
- Assembly builds to standard RimWorld mod structure
- Textures organized in mod-specific directory hierarchy
- Dependencies resolved through NuGet package management

### Asset Management
- Large atlas textures for memory efficiency
- Structured sprite allocation for easy maintenance
- Test asset generation for development validation

## Changelog

- June 29, 2025. Initial setup

## User Preferences

Preferred communication style: Simple, everyday language.