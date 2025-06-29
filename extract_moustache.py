#!/usr/bin/env python3
"""
Extract moustache from male Zhul head overlays and create clean base heads
"""

from PIL import Image, ImageDraw
import os

def extract_moustache_and_clean_head():
    """Extract moustache area and create clean male heads"""
    
    # Process both male head variants
    for variant in ['Average', 'Narrow']:
        input_file = f"Textures/Things/Pawn/Humanlike/Heads/Zhul/Male_{variant}_south.png"
        
        if not os.path.exists(input_file):
            print(f"File not found: {input_file}")
            continue
            
        # Load the original image
        img = Image.open(input_file).convert("RGBA")
        width, height = img.size
        print(f"Processing {variant} head: {width}x{height}")
        
        # Create a clean version (remove moustache area)
        clean_img = img.copy()
        
        # Create a draw object for the clean version
        draw = ImageDraw.Draw(clean_img)
        
        # Fill moustache area with olive-gray skin color to match the rest of the face
        # The moustache appears to be roughly in the middle-lower area of the face
        olive_gray = (177, 164, 132, 255)  # Matching the skin tone from the image
        
        # Estimate moustache area (rough rectangle covering the moustache)
        moustache_top = int(height * 0.55)      # Start below nose
        moustache_bottom = int(height * 0.72)   # End above mouth
        moustache_left = int(width * 0.15)      # Left side with curls
        moustache_right = int(width * 0.85)     # Right side with curls
        
        # Fill the moustache area with skin tone
        draw.rectangle(
            [moustache_left, moustache_top, moustache_right, moustache_bottom],
            fill=olive_gray
        )
        
        # Save clean head (without moustache)
        clean_output = f"Textures/Things/Pawn/Humanlike/Heads/Zhul/Male_{variant}_Clean_south.png"
        clean_img.save(clean_output, "PNG")
        print(f"Saved clean head: {clean_output}")
        
        # Extract just the moustache area for facial hair texture
        moustache_img = img.crop((moustache_left, moustache_top, moustache_right, moustache_bottom))
        
        # Create a larger canvas for the facial hair (standard beard texture size)
        beard_size = (128, 128)  # Standard RimWorld facial hair texture size
        beard_img = Image.new("RGBA", beard_size, (0, 0, 0, 0))  # Transparent background
        
        # Paste the moustache in the center-lower area of the beard canvas
        moustache_width, moustache_height = moustache_img.size
        paste_x = (beard_size[0] - moustache_width) // 2
        paste_y = int(beard_size[1] * 0.4)  # Position in lower-middle of beard area
        
        beard_img.paste(moustache_img, (paste_x, paste_y), moustache_img)
        
        # Save facial hair texture
        if variant == 'Average':  # Use average as the main moustache texture
            beard_output = "Textures/Things/Pawn/Humanlike/FacialHair/Zhul_Moustache_south.png"
            os.makedirs(os.path.dirname(beard_output), exist_ok=True)
            beard_img.save(beard_output, "PNG")
            print(f"Saved moustache texture: {beard_output}")

if __name__ == "__main__":
    extract_moustache_and_clean_head()
    print("Moustache extraction complete!")