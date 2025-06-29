#!/usr/bin/env python3
"""
Create an overview image showing all Zhul head shapes and the extracted moustache
"""

from PIL import Image, ImageDraw, ImageFont
import os

def create_head_overview():
    """Create a composite image showing all head variants and moustache"""
    
    # File paths
    files = {
        'Female Average': 'Textures/Things/Pawn/Humanlike/Heads/Zhul/Female_Average_south.png',
        'Female Narrow': 'Textures/Things/Pawn/Humanlike/Heads/Zhul/Female_Narrow_south.png',
        'Male Average (Clean)': 'Textures/Things/Pawn/Humanlike/Heads/Zhul/Male_Average_Clean_south.png',
        'Male Narrow (Clean)': 'Textures/Things/Pawn/Humanlike/Heads/Zhul/Male_Narrow_Clean_south.png',
        'Extracted Moustache': 'Textures/Things/Pawn/Humanlike/FacialHair/Zhul_Moustache_south.png'
    }
    
    # Load all images
    images = {}
    for name, path in files.items():
        if os.path.exists(path):
            img = Image.open(path).convert("RGBA")
            images[name] = img
            print(f"Loaded {name}: {img.size}")
        else:
            print(f"File not found: {path}")
    
    if not images:
        print("No images found to create overview")
        return
    
    # Calculate layout
    thumb_size = 256  # Thumbnail size
    padding = 20
    label_height = 30
    
    # 2x3 grid layout (2 rows, 3 columns)
    cols = 3
    rows = 2
    
    canvas_width = (thumb_size * cols) + (padding * (cols + 1))
    canvas_height = (thumb_size * rows) + (padding * (rows + 1)) + (label_height * rows)
    
    # Create canvas
    canvas = Image.new("RGBA", (canvas_width, canvas_height), (50, 50, 50, 255))
    draw = ImageDraw.Draw(canvas)
    
    # Try to load a font
    try:
        font = ImageFont.truetype("/usr/share/fonts/truetype/dejavu/DejaVuSans-Bold.ttf", 16)
    except:
        font = ImageFont.load_default()
    
    # Position images
    positions = [
        ('Female Average', 0, 0),
        ('Female Narrow', 1, 0),
        ('Extracted Moustache', 2, 0),
        ('Male Average (Clean)', 0, 1),
        ('Male Narrow (Clean)', 1, 1)
    ]
    
    for name, col, row in positions:
        if name not in images:
            continue
            
        img = images[name]
        
        # Resize to thumbnail
        img_thumb = img.copy()
        img_thumb.thumbnail((thumb_size, thumb_size), Image.Resampling.LANCZOS)
        
        # Calculate position
        x = padding + (col * (thumb_size + padding))
        y = padding + (row * (thumb_size + padding + label_height))
        
        # Center the thumbnail if it's smaller than thumb_size
        thumb_w, thumb_h = img_thumb.size
        center_x = x + (thumb_size - thumb_w) // 2
        center_y = y + (thumb_size - thumb_h) // 2
        
        # Paste image
        canvas.paste(img_thumb, (center_x, center_y), img_thumb)
        
        # Add border
        draw.rectangle([x-1, y-1, x+thumb_size, y+thumb_size], outline=(200, 200, 200), width=1)
        
        # Add label
        label_y = y + thumb_size + 5
        
        # Calculate text position for centering
        bbox = draw.textbbox((0, 0), name, font=font)
        text_width = bbox[2] - bbox[0]
        text_x = x + (thumb_size - text_width) // 2
        
        draw.text((text_x, label_y), name, fill=(255, 255, 255), font=font)
    
    # Add title
    title = "Zhul Tribe Head System Overview"
    title_bbox = draw.textbbox((0, 0), title, font=font)
    title_width = title_bbox[2] - title_bbox[0]
    title_x = (canvas_width - title_width) // 2
    
    # Draw title background
    draw.rectangle([title_x - 10, 5, title_x + title_width + 10, 25], fill=(0, 0, 0, 200))
    draw.text((title_x, 8), title, fill=(255, 255, 255), font=font)
    
    # Add description
    description = [
        "• Female heads: Olive-gray skin with bone-ash markings",
        "• Male heads: Clean bases for RimWorld facial feature overlay",
        "• Moustache: Extracted for BeardDef system (auto-applied to males)",
        "• Big & Small Framework: Will handle size variations",
        "• RimWorld: Auto-generates other directions from south-facing sprites"
    ]
    
    desc_y = canvas_height - 120
    for i, line in enumerate(description):
        draw.text((10, desc_y + i * 20), line, fill=(220, 220, 220), font=font)
    
    # Save overview
    output_path = "Zhul_Head_System_Overview.png"
    canvas.save(output_path, "PNG")
    print(f"Overview saved to: {output_path}")
    return output_path

if __name__ == "__main__":
    create_head_overview()