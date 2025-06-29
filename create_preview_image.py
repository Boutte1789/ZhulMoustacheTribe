#!/usr/bin/env python3
"""
Create Steam Workshop preview image for Zhul Tribe mod
Standard Steam Workshop preview: 1920x1080 or 512x512
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_mod_preview():
    """Create professional mod preview image"""
    
    # Standard Steam preview dimensions
    width, height = 1920, 1080
    
    # Create base image with dark tribal background
    img = Image.new('RGB', (width, height), (20, 15, 10))  # Dark brown
    draw = ImageDraw.Draw(img)
    
    # Create gradient background
    for y in range(height):
        intensity = int(20 + (y / height) * 40)  # Gradient from dark to slightly lighter
        color = (intensity, intensity//2, intensity//4)  # Brown gradient
        draw.line([(0, y), (width, y)], fill=color)
    
    # Draw tribal pattern border
    border_width = 40
    tribal_color = (139, 69, 19)  # Saddle brown
    
    # Top and bottom borders
    for i in range(0, width, 60):
        draw.polygon([(i, 0), (i+30, 0), (i+40, border_width), (i+20, border_width)], fill=tribal_color)
        draw.polygon([(i, height-border_width), (i+20, height-border_width), (i+40, height), (i+30, height)], fill=tribal_color)
    
    # Try to use a bold font, fallback to default
    try:
        title_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 120)
        subtitle_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 60)
        desc_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 40)
    except:
        title_font = ImageFont.load_default()
        subtitle_font = ImageFont.load_default()
        desc_font = ImageFont.load_default()
    
    # Main title
    title = "ZHUL TRIBE"
    title_bbox = draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    title_x = (width - title_width) // 2
    
    # Draw title with glow effect
    for offset in [(-3, -3), (-3, 3), (3, -3), (3, 3)]:
        draw.text((title_x + offset[0], 200 + offset[1]), title, fill=(60, 30, 15), font=title_font)
    draw.text((title_x, 200), title, fill=(255, 215, 0), font=title_font)  # Gold
    
    # Subtitle
    subtitle = "THE CURLED ONES"
    subtitle_bbox = draw.textbbox((0, 0), subtitle, font=subtitle_font)
    subtitle_width = subtitle_bbox[2] - subtitle_bbox[0]
    subtitle_x = (width - subtitle_width) // 2
    draw.text((subtitle_x, 340), subtitle, fill=(200, 150, 100), font=subtitle_font)
    
    # Feature list
    features = [
        "🏺 Cannibal Worshipping Tribe",
        "💀 Terror-Inducing Night Raiders", 
        "🎭 Elaborate Moustache Rituals",
        "🔊 19 Custom Tribal Audio Files",
        "⚔️ Bone-Chief Warden Leadership"
    ]
    
    y_start = 500
    for i, feature in enumerate(features):
        draw.text((200, y_start + i * 70), feature, fill=(220, 190, 160), font=desc_font)
    
    # Add skull decoration
    skull_size = 150
    skull_x = width - 300
    skull_y = 450
    
    # Simple skull shape
    draw.ellipse([skull_x, skull_y, skull_x + skull_size, skull_y + skull_size//2], 
                fill=(80, 60, 40), outline=(120, 90, 60), width=3)
    
    # Eye sockets
    eye_size = 25
    draw.ellipse([skull_x + 30, skull_y + 20, skull_x + 30 + eye_size, skull_y + 20 + eye_size], 
                fill=(0, 0, 0))
    draw.ellipse([skull_x + 90, skull_y + 20, skull_x + 90 + eye_size, skull_y + 20 + eye_size], 
                fill=(0, 0, 0))
    
    # Bottom text
    bottom_text = "Compatible with RimWorld 1.4 & 1.5 • Requires Harmony & Big Small Framework"
    bottom_bbox = draw.textbbox((0, 0), bottom_text, font=desc_font)
    bottom_width = bottom_bbox[2] - bottom_bbox[0]
    bottom_x = (width - bottom_width) // 2
    draw.text((bottom_x, height - 120), bottom_text, fill=(150, 120, 90), font=desc_font)
    
    # Save the image
    img.save('About/Preview.png', 'PNG', quality=95)
    print("✅ Created About/Preview.png (1920x1080)")
    
    # Create smaller version for mod icon
    small_img = img.resize((512, 512), Image.Resampling.LANCZOS)
    small_img.save('About/Icon.png', 'PNG', quality=95)
    print("✅ Created About/Icon.png (512x512)")

if __name__ == "__main__":
    create_mod_preview()