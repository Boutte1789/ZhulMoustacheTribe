#!/usr/bin/env python3
"""
Generate visual comparison showing male vs female Curled One pawns
Shows the difference: males with moustaches vs females with facial tattoos
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_gender_comparison():
    """Create side-by-side comparison of male and female Zhul pawns"""
    
    # Create comparison image
    width, height = 1600, 900
    img = Image.new('RGB', (width, height), (30, 30, 35))
    draw = ImageDraw.Draw(img)
    
    # Zhul colors
    skin_color = (120, 130, 100)  # Olive-gray
    hair_color = (60, 40, 20)     # Dark brown/black
    clothing_color = (80, 60, 40) # Tribal brown
    tattoo_color = (200, 180, 150) # Bone-ash color
    
    try:
        title_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 48)
        subtitle_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 32)
        desc_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 24)
    except:
        title_font = ImageFont.load_default()
        subtitle_font = ImageFont.load_default()
        desc_font = ImageFont.load_default()
    
    # Title
    title = "ZHUL TRIBE - GENDER DIFFERENCES"
    title_bbox = draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    draw.text(((width - title_width) // 2, 30), title, fill=(255, 215, 0), font=title_font)
    
    # Male pawn (left side)
    male_center_x = width // 4
    pawn_y = 150
    
    # Male subtitle
    male_title = "MALE ZHUL"
    male_title_bbox = draw.textbbox((0, 0), male_title, font=subtitle_font)
    male_title_width = male_title_bbox[2] - male_title_bbox[0]
    draw.text((male_center_x - male_title_width//2, 100), male_title, fill=(180, 160, 120), font=subtitle_font)
    
    # Male head
    head_size = 80
    head_x = male_center_x - head_size//2
    draw.ellipse([head_x, pawn_y, head_x + head_size, pawn_y + head_size + 15], 
                fill=skin_color, outline=(100, 110, 80), width=2)
    
    # Male eyes
    eye_size = 8
    eye_y = pawn_y + 25
    draw.ellipse([head_x + 20, eye_y, head_x + 20 + eye_size, eye_y + eye_size], fill=(20, 20, 20))
    draw.ellipse([head_x + 50, eye_y, head_x + 50 + eye_size, eye_y + eye_size], fill=(20, 20, 20))
    
    # MALE MOUSTACHE (elaborate spiral)
    mouth_y = eye_y + 25
    # Main moustache base
    draw.ellipse([male_center_x - 30, mouth_y, male_center_x + 30, mouth_y + 12], 
                fill=hair_color, outline=(40, 25, 10), width=2)
    
    # Left spiral curl
    spiral_left = [
        (male_center_x - 30, mouth_y + 6),
        (male_center_x - 45, mouth_y + 4),
        (male_center_x - 50, mouth_y - 5),
        (male_center_x - 45, mouth_y - 12),
        (male_center_x - 35, mouth_y - 15),
        (male_center_x - 25, mouth_y - 10)
    ]
    draw.polygon(spiral_left, fill=hair_color, outline=(40, 25, 10))
    
    # Right spiral curl
    spiral_right = [
        (male_center_x + 30, mouth_y + 6),
        (male_center_x + 45, mouth_y + 4),
        (male_center_x + 50, mouth_y - 5),
        (male_center_x + 45, mouth_y - 12),
        (male_center_x + 35, mouth_y - 15),
        (male_center_x + 25, mouth_y - 10)
    ]
    draw.polygon(spiral_right, fill=hair_color, outline=(40, 25, 10))
    
    # Male body
    body_y = pawn_y + head_size + 30
    body_width = 60
    body_height = 120
    draw.rectangle([male_center_x - body_width//2, body_y,
                   male_center_x + body_width//2, body_y + body_height],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    
    # Male clothing
    draw.rectangle([male_center_x - body_width//2 + 5, body_y + 15,
                   male_center_x + body_width//2 - 5, body_y + body_height - 10],
                  fill=clothing_color, outline=(60, 45, 30), width=1)
    
    # Female pawn (right side)
    female_center_x = 3 * width // 4
    
    # Female subtitle
    female_title = "FEMALE ZHUL"
    female_title_bbox = draw.textbbox((0, 0), female_title, font=subtitle_font)
    female_title_width = female_title_bbox[2] - female_title_bbox[0]
    draw.text((female_center_x - female_title_width//2, 100), female_title, fill=(180, 160, 120), font=subtitle_font)
    
    # Female head (slightly smaller)
    female_head_size = 75
    female_head_x = female_center_x - female_head_size//2
    draw.ellipse([female_head_x, pawn_y, female_head_x + female_head_size, pawn_y + female_head_size + 15], 
                fill=skin_color, outline=(100, 110, 80), width=2)
    
    # Female eyes
    female_eye_y = pawn_y + 25
    draw.ellipse([female_head_x + 18, female_eye_y, female_head_x + 18 + eye_size, female_eye_y + eye_size], fill=(20, 20, 20))
    draw.ellipse([female_head_x + 48, female_eye_y, female_head_x + 48 + eye_size, female_eye_y + eye_size], fill=(20, 20, 20))
    
    # FEMALE FACIAL TATTOOS (bone-ash markings)
    # Forehead markings
    draw.line([(female_center_x - 20, pawn_y + 15), (female_center_x + 20, pawn_y + 15)], fill=tattoo_color, width=3)
    draw.line([(female_center_x - 15, pawn_y + 20), (female_center_x + 15, pawn_y + 20)], fill=tattoo_color, width=2)
    
    # Cheek markings (tribal patterns)
    # Left cheek
    draw.arc([female_head_x + 5, female_eye_y + 10, female_head_x + 25, female_eye_y + 30], 
             0, 180, fill=tattoo_color, width=2)
    draw.line([(female_head_x + 10, female_eye_y + 15), (female_head_x + 20, female_eye_y + 25)], fill=tattoo_color, width=2)
    
    # Right cheek
    draw.arc([female_head_x + 50, female_eye_y + 10, female_head_x + 70, female_eye_y + 30], 
             0, 180, fill=tattoo_color, width=2)
    draw.line([(female_head_x + 55, female_eye_y + 25), (female_head_x + 65, female_eye_y + 15)], fill=tattoo_color, width=2)
    
    # Chin markings
    chin_y = pawn_y + 65
    draw.line([(female_center_x - 10, chin_y), (female_center_x + 10, chin_y)], fill=tattoo_color, width=2)
    draw.line([(female_center_x - 5, chin_y + 5), (female_center_x + 5, chin_y + 5)], fill=tattoo_color, width=2)
    
    # Female body (slightly narrower)
    female_body_width = 55
    draw.rectangle([female_center_x - female_body_width//2, body_y,
                   female_center_x + female_body_width//2, body_y + body_height],
                  fill=skin_color, outline=(100, 110, 80), width=2)
    
    # Female clothing
    draw.rectangle([female_center_x - female_body_width//2 + 5, body_y + 15,
                   female_center_x + female_body_width//2 - 5, body_y + body_height - 10],
                  fill=clothing_color, outline=(60, 45, 30), width=1)
    
    # Descriptions
    desc_y = body_y + body_height + 50
    
    # Male description
    male_desc = [
        "Elaborate Spiraling Moustache",
        "• Sacred grooming rituals",
        "• Moustache oil ceremonies", 
        "• Symbol of spiritual power",
        "• Status within tribe hierarchy",
        "• Trait: ZHUL_MoustacheRitual"
    ]
    
    y_pos = desc_y
    for line in male_desc:
        color = (220, 200, 160) if not line.startswith("•") else (180, 160, 120)
        line_bbox = draw.textbbox((0, 0), line, font=desc_font)
        line_width = line_bbox[2] - line_bbox[0]
        draw.text((male_center_x - line_width//2, y_pos), line, fill=color, font=desc_font)
        y_pos += 30
    
    # Female description
    female_desc = [
        "Intricate Bone-Ash Tattoos",
        "• Stories of devoured enemies",
        "• Facial markings display status",
        "• Ceremonial tattoo rituals",
        "• Alternative to moustaches",
        "• Trait: ZHUL_FacialTattoos"
    ]
    
    y_pos = desc_y
    for line in female_desc:
        color = (220, 200, 160) if not line.startswith("•") else (180, 160, 120)
        line_bbox = draw.textbbox((0, 0), line, font=desc_font)
        line_width = line_bbox[2] - line_bbox[0]
        draw.text((female_center_x - line_width//2, y_pos), line, fill=color, font=desc_font)
        y_pos += 30
    
    # Bottom note
    note = "Both genders maintain the same cannibalistic traditions and tribal hierarchy"
    note_bbox = draw.textbbox((0, 0), note, font=desc_font)
    note_width = note_bbox[2] - note_bbox[0]
    draw.text(((width - note_width) // 2, height - 60), note, fill=(150, 130, 100), font=desc_font)
    
    # Save image
    img.save('zhul_gender_comparison.png', 'PNG', quality=95)
    print("✅ Created zhul_gender_comparison.png - Visual gender comparison")

if __name__ == "__main__":
    create_gender_comparison()