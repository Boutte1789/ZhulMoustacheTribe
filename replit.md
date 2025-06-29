# ZhulTribe - RimWorld Mod

## Overview

ZhulTribe is a C# mod for the RimWorld game built on the .NET Framework 4.7.2. This project is designed to extend RimWorld's functionality through custom code that integrates with the game's modding framework. The mod uses the official Krafs.Rimworld.Ref package to access RimWorld's APIs and Unity engine components.

## System Architecture

### Technology Stack
- **Language**: C# 
- **Framework**: .NET Framework 4.7.2
- **Game Engine**: Unity (via RimWorld)
- **Build System**: MSBuild/.NET SDK
- **Package Management**: NuGet

### Project Structure
The project follows a standard .NET Framework class library structure:
- Main source code located in `Source/ZhulTribe/`
- Project configuration defined in `ZhulTribe.csproj`
- NuGet package restoration handled automatically
- Build artifacts generated in `obj/` directory

## Key Components

### RimWorld Integration
- **Krafs.Rimworld.Ref (v1.4.3641)**: Provides reference assemblies for RimWorld's core systems
- Includes access to Assembly-CSharp.dll (main game logic)
- Unity engine modules for graphics, audio, physics, and input systems
- Game-specific libraries like TextMeshPro for UI rendering

### Core Dependencies
- **Game Engine Components**: Unity modules for rendering, physics, audio, and input
- **System Libraries**: .NET Framework system assemblies for configuration, serialization, and XML processing
- **Audio Support**: NAudio and NVorbis for sound processing
- **Compression**: ISharpZipLib for file handling

## Data Flow

1. **Mod Loading**: RimWorld loads the compiled mod assembly during game startup
2. **Game Integration**: Mod hooks into RimWorld's event system and data structures
3. **Runtime Execution**: Custom logic executes within RimWorld's game loop
4. **Unity Rendering**: Visual elements rendered through Unity's graphics pipeline

## External Dependencies

### Primary Dependencies
- **Krafs.Rimworld.Ref**: Official RimWorld modding reference package
- **Microsoft.NETFramework.ReferenceAssemblies**: .NET Framework development support

### Unity Engine Modules
- Core engine functionality (CoreModule, IMGUIModule)
- Graphics and rendering (ClusterRendererModule, ParticleSystemModule)
- Audio processing (AudioModule via NAudio)
- Physics simulation (PhysicsModule, Physics2DModule)
- Input handling (InputModule, InputLegacyModule)

## Deployment Strategy

### Build Process
- Standard .NET Framework compilation targeting net472
- NuGet package restoration for dependencies
- Output generates mod assembly for RimWorld integration

### Distribution
- Compiled mod distributed as DLL assembly
- Integrated into RimWorld's mod loading system
- Compatible with RimWorld version 1.4.3641 and later

### Installation
- Users install mod through RimWorld's built-in mod manager
- Mod files placed in RimWorld's Mods directory structure
- Game automatically loads mod during startup

## Changelog

- June 29, 2025. Initial setup

## User Preferences

Preferred communication style: Simple, everyday language.