"""
Generate final preview showing completed male and female Zhul pawns
Shows how they appear in-game with proper gender differences
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_final_zhul_preview():
    """Create comprehensive preview of finished male and female Zhul pawns"""
    
    # Load the actual sprite files
    try:
        male_sprite = Image.open("Textures/Zhul/ZhulBase_bp_male.png").convert("RGBA")
        female_sprite = Image.open("Textures/Zhul/ZhulBase_bp_female.png").convert("RGBA")
    except FileNotFoundError:
        print("Warning: Sprite files not found, creating placeholder representations")
        male_sprite = create_placeholder_sprite("male")
        female_sprite = create_placeholder_sprite("female")
    
    # Create main preview image
    width, height = 1920, 1080
    img = Image.new('RGB', (width, height), (25, 28, 35))
    draw = ImageDraw.Draw(img)
    
    # Load fonts
    try:
        title_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 64)
        subtitle_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 36)
        desc_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 24)
        small_font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf", 18)
    except:
        title_font = ImageFont.load_default()
        subtitle_font = ImageFont.load_default()
        desc_font = ImageFont.load_default()
        small_font = ImageFont.load_default()
    
    # Title
    title = "ZHUL TRIBE - THE CURLED ONES"
    title_bbox = draw.textbbox((0, 0), title, font=title_font)
    title_width = title_bbox[2] - title_bbox[0]
    draw.text(((width - title_width) // 2, 40), title, fill=(255, 215, 0), font=title_font)
    
    subtitle = "Gender-Specific Pawn Appearances"
    subtitle_bbox = draw.textbbox((0, 0), subtitle, font=subtitle_font)
    subtitle_width = subtitle_bbox[2] - subtitle_bbox[0]
    draw.text(((width - subtitle_width) // 2, 120), subtitle, fill=(200, 200, 200), font=subtitle_font)
    
    # Create comparison panels
    panel_width = 750
    panel_height = 600
    panel_y = 200
    
    # Male panel (left)
    male_panel_x = 100
    draw.rectangle([male_panel_x, panel_y, male_panel_x + panel_width, panel_y + panel_height], 
                  fill=(40, 45, 55), outline=(80, 90, 110), width=3)
    
    # Female panel (right) 
    female_panel_x = width - panel_width - 100
    draw.rectangle([female_panel_x, panel_y, female_panel_x + panel_width, panel_y + panel_height],
                  fill=(40, 45, 55), outline=(80, 90, 110), width=3)
    
    # Male section
    male_title = "MALE ZHUL WARRIOR"
    male_title_bbox = draw.textbbox((0, 0), male_title, font=subtitle_font)
    male_title_width = male_title_bbox[2] - male_title_bbox[0]
    draw.text((male_panel_x + (panel_width - male_title_width) // 2, panel_y + 20), 
              male_title, fill=(255, 180, 120), font=subtitle_font)
    
    # Scale and position male sprite
    sprite_size = 300
    male_sprite_resized = male_sprite.resize((sprite_size, sprite_size), Image.Resampling.LANCZOS)
    sprite_x = male_panel_x + (panel_width - sprite_size) // 2
    sprite_y = panel_y + 80
    
    # Add background for sprite
    bg_padding = 20
    draw.rectangle([sprite_x - bg_padding, sprite_y - bg_padding, 
                   sprite_x + sprite_size + bg_padding, sprite_y + sprite_size + bg_padding],
                  fill=(30, 35, 45), outline=(100, 110, 130), width=2)
    
    img.paste(male_sprite_resized, (sprite_x, sprite_y), male_sprite_resized)
    
    # Male characteristics
    male_features = [
        "• Elaborate curled moustache",
        "• Olive-gray skin tone", 
        "• Muscular tribal build",
        "• Traditional bone ornaments",
        "• Cannibal facial scarring",
        "• Ancestral spirit connection"
    ]
    
    feature_y = sprite_y + sprite_size + 30
    for i, feature in enumerate(male_features):
        draw.text((male_panel_x + 30, feature_y + i * 30), feature, 
                 fill=(220, 220, 220), font=desc_font)
    
    # Female section
    female_title = "FEMALE ZHUL WARRIOR"
    female_title_bbox = draw.textbbox((0, 0), female_title, font=subtitle_font)
    female_title_width = female_title_bbox[2] - female_title_bbox[0]
    draw.text((female_panel_x + (panel_width - female_title_width) // 2, panel_y + 20),
              female_title, fill=(255, 180, 120), font=subtitle_font)
    
    # Scale and position female sprite
    female_sprite_resized = female_sprite.resize((sprite_size, sprite_size), Image.Resampling.LANCZOS)
    sprite_x = female_panel_x + (panel_width - sprite_size) // 2
    
    # Add background for sprite
    draw.rectangle([sprite_x - bg_padding, sprite_y - bg_padding,
                   sprite_x + sprite_size + bg_padding, sprite_y + sprite_size + bg_padding],
                  fill=(30, 35, 45), outline=(100, 110, 130), width=2)
    
    img.paste(female_sprite_resized, (sprite_x, sprite_y), female_sprite_resized)
    
    # Female characteristics  
    female_features = [
        "• Bone-ash facial tattoos",
        "• Sleeker tribal physique",
        "• Same olive-gray skin tone",
        "• Ornate bone jewelry",
        "• Ritual scarification patterns", 
        "• Equal spiritual power"
    ]
    
    for i, feature in enumerate(female_features):
        draw.text((female_panel_x + 30, feature_y + i * 30), feature,
                 fill=(220, 220, 220), font=desc_font)
    
    # Add bottom information bar
    info_y = height - 120
    draw.rectangle([0, info_y, width, height], fill=(20, 25, 35), outline=(60, 70, 90), width=2)
    
    info_text = "✓ Professional RimWorld Integration  ✓ Big & Small Framework Scaling  ✓ Gender-Specific Traits  ✓ Steam Workshop Ready"
    info_bbox = draw.textbbox((0, 0), info_text, font=desc_font)
    info_width = info_bbox[2] - info_bbox[0]
    draw.text(((width - info_width) // 2, info_y + 30), info_text, fill=(180, 200, 255), font=desc_font)
    
    mod_info = "Zhul Tribe Mod v1.0 - Professional Grade RimWorld Faction"
    mod_bbox = draw.textbbox((0, 0), mod_info, font=small_font)
    mod_width = mod_bbox[2] - mod_bbox[0]
    draw.text(((width - mod_width) // 2, info_y + 70), mod_info, fill=(150, 150, 150), font=small_font)
    
    return img

def create_placeholder_sprite(gender):
    """Create placeholder sprite if actual files not found"""
    sprite = Image.new("RGBA", (128, 128), (0, 0, 0, 0))
    draw = ImageDraw.Draw(sprite)
    
    # Zhul colors
    skin_color = (120, 130, 100, 255)
    hair_color = (60, 40, 20, 255)
    
    # Basic humanoid shape
    # Head
    draw.ellipse([35, 10, 93, 68], fill=skin_color, outline=(90, 100, 75), width=2)
    
    # Body
    draw.rectangle([45, 65, 83, 110], fill=skin_color, outline=(90, 100, 75), width=2)
    
    # Arms
    draw.rectangle([25, 70, 45, 105], fill=skin_color, outline=(90, 100, 75), width=1)
    draw.rectangle([83, 70, 103, 105], fill=skin_color, outline=(90, 100, 75), width=1)
    
    # Legs  
    draw.rectangle([48, 110, 58, 125], fill=skin_color, outline=(90, 100, 75), width=1)
    draw.rectangle([70, 110, 80, 125], fill=skin_color, outline=(90, 100, 75), width=1)
    
    # Gender-specific features
    if gender == "male":
        # Moustache
        draw.ellipse([50, 45, 78, 55], fill=hair_color, outline=(40, 20, 10), width=1)
    else:
        # Facial tattoos (simplified lines)
        draw.line([45, 35, 55, 45], fill=(200, 180, 150), width=2)
        draw.line([73, 45, 83, 35], fill=(200, 180, 150), width=2)
    
    # Eyes
    draw.ellipse([45, 30, 50, 35], fill=(25, 25, 25))
    draw.ellipse([78, 30, 83, 35], fill=(25, 25, 25))
    
    return sprite

def main():
    print("Creating final Zhul tribe gender preview...")
    
    preview = create_final_zhul_preview()
    preview.save("zhul_final_gender_preview.png")
    
    print("✓ Final gender preview created: zhul_final_gender_preview.png")
    print("✓ Shows male Zhul with elaborate moustache")
    print("✓ Shows female Zhul with bone-ash facial tattoos")
    print("✓ Professional presentation ready for Steam Workshop")

if __name__ == "__main__":
    main()