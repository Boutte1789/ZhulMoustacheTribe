# ZhulTribe - RimWorld Mod

## Overview

ZhulTribe is a .NET Standard 2.0 library designed as a mod for the RimWorld game. It leverages the Krafs.Rimworld.Ref package to interact with RimWorld's game systems and provides custom gameplay functionality. The project follows a standard C# class library structure with dependencies on RimWorld's modding framework.

## System Architecture

### Technology Stack
- **Framework**: .NET Standard 2.0
- **Primary Dependency**: Krafs.Rimworld.Ref (version 1.4.3641)
- **Build System**: MSBuild with NuGet package management
- **Target Platform**: RimWorld game mod system

### Project Structure
The project follows a typical C# library structure:
- `Source/ZhulTribe/` - Main source code directory
- `Assemblies/` - Compiled output directory containing the final DLL and dependencies
- Build artifacts are managed through standard .NET tooling

## Key Components

### Core Library
- **ZhulTribe.dll**: The main mod assembly containing custom game logic
- **Target Framework**: .NET Standard 2.0 for compatibility with RimWorld's Unity-based runtime

### RimWorld Integration
- **Krafs.Rimworld.Ref**: Provides reference assemblies for RimWorld's game systems
- Includes Unity engine modules (Physics, Audio, Animation, etc.)
- Provides access to RimWorld's core gameplay systems (Assembly-CSharp.dll)

## Data Flow

1. **Compilation**: Source code is compiled into ZhulTribe.dll targeting .NET Standard 2.0
2. **Packaging**: Compiled assembly is placed in the Assemblies directory alongside dependency metadata
3. **Game Integration**: RimWorld loads the mod DLL at runtime and integrates custom functionality
4. **Runtime Execution**: Mod code executes within RimWorld's Unity-based game engine

## External Dependencies

### Primary Dependencies
- **Krafs.Rimworld.Ref (1.4.3641)**: RimWorld modding framework providing game API access
- **NETStandard.Library (2.0.3)**: Standard .NET library support

### Unity Engine Dependencies
The mod has access to numerous Unity engine modules through the RimWorld reference:
- Core modules (CoreModule, IMGUIModule, InputModule)
- Graphics modules (ParticleSystemModule, Physics2DModule)
- Audio and animation systems
- Platform-specific modules

## Deployment Strategy

### Build Process
1. **Restoration**: NuGet packages are restored to provide RimWorld API references
2. **Compilation**: Source code is compiled against RimWorld assemblies
3. **Output**: Compiled DLL is placed in the Assemblies directory for RimWorld to load

### Distribution
- The mod is distributed as a directory structure containing the Assemblies folder
- RimWorld automatically discovers and loads mods from its Mods directory
- No additional installation steps required beyond copying to the game's mod folder

## Changelog

```
Changelog:
- June 29, 2025. Initial setup
```

## User Preferences

```
Preferred communication style: Simple, everyday language.
```