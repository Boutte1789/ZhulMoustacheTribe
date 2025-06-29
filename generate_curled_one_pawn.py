#!/usr/bin/env python3
"""
Generate visual representation of a Curled One pawn as they appear in RimWorld
Based on the Zhul tribe specifications: olive-gray skin, elaborate moustaches, tribal appearance
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_curled_one_pawn():
    """Create detailed RimWorld-style pawn representation"""
    
    # RimWorld pawn dimensions (scaled up for visibility)
    width, height = 800, 1000
    
    # Create base image with transparent background
    img = Image.new('RGBA', (width, height), (0, 0, 0, 0))
    draw = ImageDraw.Draw(img)
    
    # Zhul skin color - olive-gray
    skin_color = (120, 130, 100)  # Olive-gray
    hair_color = (60, 40, 20)     # Dark brown/black
    clothing_color = (80, 60, 40) # Tribal brown
    bone_color = (240, 230, 200)  # Bone white
    
    # Body proportions (centered)
    center_x = width // 2
    body_width = 180
    head_size = 120
    
    # HEAD (elongated skull as per Zhul trait)
    head_x = center_x - head_size//2
    head_y = 100
    # Elongated skull - taller than normal
    draw.ellipse([head_x, head_y, head_x + head_size, head_y + head_size + 20], 
                fill=skin_color, outline=(100, 110, 80), width=3)
    
    # FACIAL FEATURES
    # Eyes
    eye_size = 12
    eye_y = head_y + 30
    draw.ellipse([head_x + 25, eye_y, head_x + 25 + eye_size, eye_y + eye_size], 
                fill=(20, 20, 20))
    draw.ellipse([head_x + 75, eye_y, head_x + 75 + eye_size, eye_y + eye_size], 
                fill=(20, 20, 20))
    
    # Nose
    nose_x = center_x - 5
    nose_y = eye_y + 20
    draw.polygon([(nose_x, nose_y), (nose_x - 8, nose_y + 15), (nose_x + 8, nose_y + 15)], 
                fill=(100, 110, 80))
    
    # ELABORATE SPIRALING MOUSTACHE (signature Zhul feature)
    mouth_y = nose_y + 20
    
    # Main moustache base
    draw.ellipse([center_x - 40, mouth_y, center_x + 40, mouth_y + 15], 
                fill=hair_color, outline=(40, 25, 10), width=2)
    
    # Left spiral curl
    spiral_points_left = [
        (center_x - 40, mouth_y + 7),
        (center_x - 60, mouth_y + 5),
        (center_x - 70, mouth_y - 5),
        (center_x - 65, mouth_y - 15),
        (center_x - 50, mouth_y - 20),
        (center_x - 35, mouth_y - 15),
        (center_x - 30, mouth_y - 5)
    ]
    draw.polygon(spiral_points_left, fill=hair_color, outline=(40, 25, 10))
    
    # Right spiral curl  
    spiral_points_right = [
        (center_x + 40, mouth_y + 7),
        (center_x + 60, mouth_y + 5),
        (center_x + 70, mouth_y - 5),
        (center_x + 65, mouth_y - 15),
        (center_x + 50, mouth_y - 20),
        (center_x + 35, mouth_y - 15),
        (center_x + 30, mouth_y - 5)
    ]
    draw.polygon(spiral_points_right, fill=hair_color, outline=(40, 25, 10))
    
    # TORSO
    torso_y = head_y + head_size + 40
    torso_height = 200
    draw.rectangle([center_x - body_width//2, torso_y, 
                   center_x + body_width//2, torso_y + torso_height], 
                  fill=skin_color, outline=(100, 110, 80), width=3)
    
    # TRIBAL CLOTHING
    # Simple tunic
    tunic_y = torso_y + 20
    draw.rectangle([center_x - body_width//2 + 10, tunic_y,
                   center_x + body_width//2 - 10, torso_y + torso_height - 20],
                  fill=clothing_color, outline=(60, 45, 30), width=2)
    
    # BONE NECKLACE (Zhul cultural item)
    necklace_y = tunic_y + 30
    # Necklace cord
    draw.arc([center_x - 60, necklace_y, center_x + 60, necklace_y + 40], 
             0, 180, fill=(40, 30, 20), width=3)
    
    # Bone pendant
    draw.ellipse([center_x - 15, necklace_y + 35, center_x + 15, necklace_y + 55], 
                fill=bone_color, outline=(200, 190, 170), width=2)
    
    # ARMS
    arm_width = 40
    arm_length = 150
    # Left arm
    draw.rectangle([center_x - body_width//2 - 30, torso_y + 20,
                   center_x - body_width//2, torso_y + 20 + arm_length],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    # Right arm  
    draw.rectangle([center_x + body_width//2, torso_y + 20,
                   center_x + body_width//2 + 30, torso_y + 20 + arm_length],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    
    # HANDS holding ceremonial items
    # Left hand - Moustache oil vial
    vial_x = center_x - body_width//2 - 40
    vial_y = torso_y + 150
    draw.ellipse([vial_x, vial_y, vial_x + 20, vial_y + 30], 
                fill=(100, 150, 100), outline=(80, 120, 80), width=2)
    
    # Right hand - Small ritual dagger
    dagger_x = center_x + body_width//2 + 10
    dagger_y = torso_y + 140
    draw.rectangle([dagger_x, dagger_y, dagger_x + 25, dagger_y + 5], 
                  fill=(150, 150, 150))  # Blade
    draw.rectangle([dagger_x + 25, dagger_y - 5, dagger_x + 35, dagger_y + 10], 
                  fill=(80, 50, 30))     # Handle
    
    # LEGS
    leg_width = 50
    leg_height = 180
    legs_y = torso_y + torso_height
    # Left leg
    draw.rectangle([center_x - 40, legs_y, center_x - 40 + leg_width, legs_y + leg_height],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    # Right leg
    draw.rectangle([center_x - 10, legs_y, center_x - 10 + leg_width, legs_y + leg_height],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    
    # TRIBAL BOOTS
    boot_height = 40
    boot_y = legs_y + leg_height - boot_height
    draw.rectangle([center_x - 45, boot_y, center_x - 40 + leg_width + 5, legs_y + leg_height],
                  fill=clothing_color, outline=(60, 45, 30), width=2)
    draw.rectangle([center_x - 15, boot_y, center_x - 10 + leg_width + 5, legs_y + leg_height],
                  fill=clothing_color, outline=(60, 45, 30), width=2)
    
    # Add title and description
    try:
        title_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 40)
        desc_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 24)
    except:
        title_font = ImageFont.load_default()
        desc_font = ImageFont.load_default()
    
    # Title
    title = "CURLED ONE PAWN"
    title_bbox = draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    draw.text(((width - title_width) // 2, 20), title, fill=(200, 100, 50), font=title_font)
    
    # Description
    desc_lines = [
        "Zhul Tribe Member",
        "• Olive-gray skin with elongated skull",
        "• Elaborate spiraling moustache (mandatory)",
        "• Bone necklace (cultural significance)",
        "• Moustache oil vial for daily rituals",
        "• Ceremonial dagger for cannibal practices"
    ]
    
    y_offset = height - 180
    for line in desc_lines:
        draw.text((50, y_offset), line, fill=(150, 150, 150), font=desc_font)
        y_offset += 28
    
    # Save the image
    img.save('curled_one_pawn_visual.png', 'PNG', quality=95)
    print("✅ Created curled_one_pawn_visual.png")
    
    # Create a second version showing the pawn in RimWorld UI context
    create_rimworld_ui_version()

def create_rimworld_ui_version():
    """Create version showing pawn in RimWorld-style UI"""
    
    # RimWorld UI background color
    ui_bg = (40, 40, 45)
    
    # Create UI mockup
    ui_width, ui_height = 1200, 800
    ui_img = Image.new('RGB', (ui_width, ui_height), ui_bg)
    ui_draw = ImageDraw.Draw(ui_img)
    
    # Load the pawn image we just created
    try:
        pawn_img = Image.open('curled_one_pawn_visual.png')
        # Resize to fit UI
        pawn_img = pawn_img.resize((300, 400), Image.Resampling.LANCZOS)
        # Paste onto UI background
        ui_img.paste(pawn_img, (50, 150), pawn_img if pawn_img.mode == 'RGBA' else None)
    except:
        pass
    
    try:
        ui_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 20)
        header_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 24)
    except:
        ui_font = ImageFont.load_default()
        header_font = ImageFont.load_default()
    
    # Mock RimWorld UI elements
    ui_draw.rectangle([400, 150, 1150, 750], fill=(50, 50, 55), outline=(80, 80, 85), width=2)
    
    # Pawn info panel
    ui_draw.text((420, 170), "Gharlak 'Bone-Whisper'", fill=(220, 220, 220), font=header_font)
    ui_draw.text((420, 200), "Zhul Tribe - Curled Savage", fill=(180, 180, 180), font=ui_font)
    
    # Stats
    stats = [
        "Race: ZHUL_AlienHumanoid",
        "Faction: ZhulTribe (Hostile)",
        "Traits: ZHUL_Cannibal, ZHUL_MoustacheRitual",
        "",
        "Skills:",
        "  Melee: 12 (Passion: +)",
        "  Crafting: 8",
        "  Cooking: 6 (Cannibal recipes)",
        "",
        "Equipment:",
        "  Moustache Oil (Excellent)",
        "  Bone Necklace",
        "  Sacrificial Dagger",
        "  Tribal Tunic",
        "",
        "Mood: +15 (Recent cannibal feast)",
        "Health: Healthy",
        "Body Type: Male (Big & Small scaling)"
    ]
    
    y_pos = 240
    for stat in stats:
        color = (200, 200, 200) if not stat.startswith("  ") else (160, 160, 160)
        ui_draw.text((420, y_pos), stat, fill=color, font=ui_font)
        y_pos += 25
    
    # Add RimWorld-style borders
    ui_draw.rectangle([0, 0, ui_width-1, ui_height-1], outline=(100, 100, 105), width=3)
    
    ui_img.save('curled_one_rimworld_ui.png', 'PNG', quality=95)
    print("✅ Created curled_one_rimworld_ui.png - RimWorld UI mockup")

if __name__ == "__main__":
    create_curled_one_pawn()