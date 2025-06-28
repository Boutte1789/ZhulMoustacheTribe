# Zhul Tribe Sound Pack

This directory contains placeholder references for the Zhul tribe's unique sound pack. To fully implement the audio system, you'll need to add the following audio files:

## Required Audio Files

### Ritual Chants
- `RitualChant_1.ogg` - Deep, guttural tribal chanting for ritual feasts
- `RitualChant_2.ogg` - Harmonic chanting with moustache-themed incantations
- `RitualChant_3.ogg` - Ceremonial chant for successful corpse offerings

### Bone Drumming
- `BoneDrums_1.ogg` - Rhythmic bone drum beats for raid preparation
- `BoneDrums_2.ogg` - Layered drumming for ritual ceremonies
- `BoneDrums_3.ogg` - Intense war drums for frustrated raiders

### Spirit-Eater Prayers
- `SpiritPrayer_1.ogg` - Whispered spiritual incantations
- `SpiritPrayer_2.ogg` - Mystical prayers for soul consumption

### Bone-Chief War Cries
- `WarCry_1.ogg` - Intimidating battle cry from faction leader
- `WarCry_2.ogg` - Aggressive war shout during raids
- `WarCry_3.ogg` - Commanding battle call

### Moustache Grooming Rituals
- `GroomingRitual_1.ogg` - Gentle sounds of ritual oil application
- `GroomingRitual_2.ogg` - Sacred combing ceremony sounds

### Funeral Rites
- `FuneralRites_1.ogg` - Mournful chants for fallen Zhul warriors
- `FuneralRites_2.ogg` - Ceremonial sounds for "Rebirth of the Curl"

### Tribal Ambience
- `TribalAmbience_1.ogg` - Background atmosphere for Zhul settlements
- `TribalAmbience_2.ogg` - Distant tribal sounds and bone wind chimes

## Audio Specifications

- **Format**: .ogg files (preferred by RimWorld)
- **Quality**: 44.1kHz, 16-bit or higher
- **Duration**: 3-15 seconds for effects, 10-30 seconds for ambience
- **Volume**: Normalized to prevent ear damage
- **Style**: Primitive tribal with bone instruments and guttural vocals

## Integration

The sound system is fully integrated into the mod's C# code and will automatically trigger during:
- Ritual feast raids (based on corpse availability)
- Moustache oil usage
- Pawn-specific actions (Spirit-Eater prayers, Bone-Chief war cries)
- Death and funeral events
- Ambient settlement atmosphere

Without actual audio files, the mod will still function but without sound effects. The XML definitions are complete and ready for audio implementation.