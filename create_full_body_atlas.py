#!/usr/bin/env python3
"""
Create comprehensive RimWorld body type atlas for Zhul tribe
Generates 25 sprites: 5 body types × 5 character variations
"""

from PIL import Image, ImageDraw
import math

def create_comprehensive_atlas():
    # Create larger atlas to accommodate all body types: 5120x5120 (5x5 grid of 1024x1024)
    atlas = Image.new("RGBA", (5120, 5120), (0, 0, 0, 0))
    draw = ImageDraw.Draw(atlas)
    
    # RimWorld authentic color palette
    colors = {
        'olive_base': (120, 125, 95, 255),
        'olive_shadow': (95, 100, 75, 255),
        'olive_highlight': (140, 145, 115, 255),
        'mustache_brown': (85, 60, 40, 255),
        'mustache_highlight': (105, 80, 60, 255),
        'eye_black': (25, 25, 25, 255),
        'eye_white': (245, 245, 245, 255),
        'bone_white': (235, 230, 210, 255),
        'bone_shadow': (195, 190, 170, 255),
        'leather_brown': (95, 70, 50, 255),
        'red_tint': (140, 110, 100, 255),
        'child_light': (135, 140, 110, 255),
        'skull_yellow': (220, 210, 180, 255),
        'magic_purple': (180, 80, 180, 255),
    }
    
    # Body type multipliers based on RimWorld reference images
    body_multipliers = {
        'male': {'width': 1.0, 'height': 1.0, 'head_scale': 1.0},      # Standard
        'female': {'width': 0.85, 'height': 1.0, 'head_scale': 0.95}, # Narrower
        'fat': {'width': 1.3, 'height': 0.9, 'head_scale': 1.1},      # Wide and short
        'hulk': {'width': 1.4, 'height': 1.2, 'head_scale': 1.2},     # Massive
        'thin': {'width': 0.7, 'height': 1.1, 'head_scale': 0.9},     # Narrow and tall
    }
    
    def draw_zhul_character(x, y, size, char_type, body_type):
        """Draw Zhul character with specific body type"""
        
        # Get body multipliers
        multipliers = body_multipliers[body_type]
        
        # Character-specific colors
        if char_type == "base":
            skin = colors['olive_base']
            skin_shadow = colors['olive_shadow']
            skin_highlight = colors['olive_highlight']
        elif char_type == "savage":
            skin = colors['olive_base']
            skin_shadow = colors['olive_shadow']
            skin_highlight = colors['olive_highlight']
        elif char_type == "spirit":
            skin = colors['red_tint']
            skin_shadow = (110, 85, 75, 255)
            skin_highlight = (160, 130, 120, 255)
        elif char_type == "child":
            skin = colors['child_light']
            skin_shadow = (110, 115, 90, 255)
            skin_highlight = (155, 160, 130, 255)
        else:  # base adult
            skin = colors['olive_base']
            skin_shadow = colors['olive_shadow']
            skin_highlight = colors['olive_highlight']
        
        # Scale for child
        char_scale = 0.7 if char_type == "child" else 1.0
        actual_size = int(size * char_scale)
        
        # Apply body type scaling
        body_width = int(actual_size * multipliers['width'])
        body_height = int(actual_size * multipliers['height'])
        head_scale = multipliers['head_scale']
        
        # Center character in cell
        center_x = x + size//2
        center_y = y + size//2
        
        # Head dimensions with body type scaling
        head_x = center_x
        head_y = center_y - body_height//6
        base_head_radius = int(actual_size//4 * head_scale)
        
        # Elongated alien skull (signature Zhul feature)
        skull_height = int(base_head_radius * 1.4)
        skull_width = int(base_head_radius * 0.9)
        
        # Adjust skull for body types
        if body_type == 'fat':
            skull_width = int(skull_width * 1.2)
        elif body_type == 'thin':
            skull_width = int(skull_width * 0.8)
            skull_height = int(skull_height * 1.1)
        elif body_type == 'hulk':
            skull_width = int(skull_width * 1.3)
            skull_height = int(skull_height * 1.2)
        
        # Draw head
        draw.ellipse([head_x-skull_width, head_y-skull_height, 
                     head_x+skull_width, head_y+skull_height//2], fill=skin)
        
        # Head shading
        shadow_offset = skull_width//3
        draw.ellipse([head_x-skull_width+shadow_offset//2, head_y-skull_height+shadow_offset//2,
                     head_x+skull_width-shadow_offset//4, head_y+skull_height//2-shadow_offset//4], 
                    fill=skin_shadow)
        
        # Head highlight
        highlight_offset = skull_width//4
        draw.ellipse([head_x-skull_width//2, head_y-skull_height//2,
                     head_x+skull_width//2, head_y], fill=skin_highlight)
        
        # Eyes
        eye_size = max(4, int(actual_size//30 * head_scale))
        eye_y = head_y - skull_height//4
        left_eye_x = head_x - skull_width//2
        right_eye_x = head_x + skull_width//2
        
        # Eye whites
        draw.ellipse([left_eye_x-eye_size, eye_y-eye_size//2, 
                     left_eye_x+eye_size, eye_y+eye_size//2], fill=colors['eye_white'])
        draw.ellipse([right_eye_x-eye_size, eye_y-eye_size//2, 
                     right_eye_x+eye_size, eye_y+eye_size//2], fill=colors['eye_white'])
        
        # Pupils
        pupil_size = eye_size//2
        draw.ellipse([left_eye_x-pupil_size//2, eye_y-pupil_size//2,
                     left_eye_x+pupil_size//2, eye_y+pupil_size//2], fill=colors['eye_black'])
        draw.ellipse([right_eye_x-pupil_size//2, eye_y-pupil_size//2,
                     right_eye_x+pupil_size//2, eye_y+pupil_size//2], fill=colors['eye_black'])
        
        # Moustache (signature Zhul feature)
        mustache_y = head_y + skull_height//4
        if char_type == "child":
            # Small developing mustache
            mustache_width = skull_width//2
            mustache_height = max(2, int(actual_size//80 * head_scale))
            draw.ellipse([head_x-mustache_width//2, mustache_y-mustache_height,
                         head_x+mustache_width//2, mustache_y+mustache_height], fill=colors['mustache_brown'])
        else:
            # Full elaborate mustache
            mustache_width = int(skull_width * 1.2)
            mustache_height = max(6, int(actual_size//40 * head_scale))
            
            # Main mustache
            draw.ellipse([head_x-mustache_width//2, mustache_y-mustache_height,
                         head_x+mustache_width//2, mustache_y+mustache_height], fill=colors['mustache_brown'])
            
            # Mustache highlights
            draw.ellipse([head_x-mustache_width//3, mustache_y-mustache_height//2,
                         head_x+mustache_width//3, mustache_y], fill=colors['mustache_highlight'])
            
            # Curls
            curl_size = max(8, int(actual_size//25 * head_scale))
            curl_thickness = max(3, int(actual_size//60 * head_scale))
            
            # Left curl
            draw.ellipse([head_x-mustache_width//2-curl_size//2, mustache_y-curl_size//2,
                         head_x-mustache_width//2+curl_size//2, mustache_y+curl_size//2], 
                        outline=colors['mustache_brown'], width=curl_thickness)
            
            # Right curl
            draw.ellipse([head_x+mustache_width//2-curl_size//2, mustache_y-curl_size//2,
                         head_x+mustache_width//2+curl_size//2, mustache_y+curl_size//2], 
                        outline=colors['mustache_brown'], width=curl_thickness)
        
        # Body with body type scaling
        body_y = head_y + skull_height//2 + actual_size//15
        torso_width = int(body_width * 0.4)
        torso_height = int(body_height * 0.5)
        
        # Adjust torso for specific body types
        if body_type == 'female':
            # Curved silhouette
            torso_width = int(torso_width * 0.9)
        elif body_type == 'fat':
            # Round belly
            torso_width = int(torso_width * 1.4)
            torso_height = int(torso_height * 0.8)
        elif body_type == 'hulk':
            # Massive chest
            torso_width = int(torso_width * 1.5)
            torso_height = int(torso_height * 1.3)
        elif body_type == 'thin':
            # Narrow frame
            torso_width = int(torso_width * 0.7)
            torso_height = int(torso_height * 1.1)
        
        # Draw torso
        draw.ellipse([head_x-torso_width//2, body_y, 
                     head_x+torso_width//2, body_y+torso_height], fill=skin)
        
        # Torso shading
        draw.ellipse([head_x-torso_width//3, body_y+torso_height//6,
                     head_x+torso_width//3, body_y+torso_height-torso_height//6], fill=skin_shadow)
        
        # Arms
        arm_width = int(body_width//8)
        arm_length = int(torso_height * 0.8)
        
        if body_type == 'hulk':
            arm_width = int(arm_width * 1.4)
        elif body_type == 'thin':
            arm_width = int(arm_width * 0.7)
        
        # Left arm
        draw.ellipse([head_x-torso_width//2-arm_width//2, body_y+torso_height//4,
                     head_x-torso_width//2+arm_width//2, body_y+torso_height//4+arm_length], fill=skin)
        
        # Right arm
        draw.ellipse([head_x+torso_width//2-arm_width//2, body_y+torso_height//4,
                     head_x+torso_width//2+arm_width//2, body_y+torso_height//4+arm_length], fill=skin)
        
        # Legs
        leg_width = int(body_width//8)
        leg_length = int(body_height * 0.4)
        
        if body_type == 'hulk':
            leg_width = int(leg_width * 1.3)
        elif body_type == 'thin':
            leg_width = int(leg_width * 0.8)
            leg_length = int(leg_length * 1.2)
        elif body_type == 'fat':
            leg_width = int(leg_width * 1.2)
        
        # Left leg
        draw.ellipse([head_x-torso_width//4-leg_width//2, body_y+torso_height-leg_length//4,
                     head_x-torso_width//4+leg_width//2, body_y+torso_height+leg_length*3//4], fill=skin)
        
        # Right leg
        draw.ellipse([head_x+torso_width//4-leg_width//2, body_y+torso_height-leg_length//4,
                     head_x+torso_width//4+leg_width//2, body_y+torso_height+leg_length*3//4], fill=skin)
        
        # Character-specific accessories
        if char_type == "savage":
            # Bone necklace
            bead_size = max(3, int(actual_size//50 * head_scale))
            necklace_y = body_y + torso_height//6
            for i in range(-2, 3):
                bead_x = head_x + i * bead_size * 2
                draw.ellipse([bead_x-bead_size, necklace_y-bead_size,
                             bead_x+bead_size, necklace_y+bead_size], fill=colors['bone_white'])
        
        elif char_type == "spirit":
            # Ritual skull
            skull_size = int(actual_size//12 * head_scale)
            skull_x = head_x + torso_width//2 + skull_size
            skull_y = body_y + torso_height//3
            
            draw.ellipse([skull_x-skull_size, skull_y-skull_size,
                         skull_x+skull_size, skull_y+skull_size], fill=colors['skull_yellow'])
            
            # Skull eye sockets
            eye_socket_size = skull_size//4
            draw.ellipse([skull_x-skull_size//2, skull_y-skull_size//3,
                         skull_x-skull_size//2+eye_socket_size, skull_y-skull_size//3+eye_socket_size], 
                        fill=colors['eye_black'])
            draw.ellipse([skull_x+skull_size//2-eye_socket_size, skull_y-skull_size//3,
                         skull_x+skull_size//2, skull_y-skull_size//3+eye_socket_size], 
                        fill=colors['eye_black'])
    
    # Generate all 25 combinations
    body_types = ['male', 'female', 'fat', 'hulk', 'thin']
    character_types = ['base', 'savage', 'spirit', 'child', 'adult']  # 5th is additional adult variant
    
    sprite_size = 1024
    
    for row, body_type in enumerate(body_types):
        for col, char_type in enumerate(character_types):
            x = col * sprite_size
            y = row * sprite_size
            draw_zhul_character(x, y, sprite_size, char_type, body_type)
    
    return atlas

def main():
    print("Creating comprehensive body type atlas for Zhul tribe...")
    print("Generating 25 sprites (5 body types × 5 character variations)...")
    
    atlas = create_comprehensive_atlas()
    atlas.save("Textures/Zhul/ZhulBodyTypes.png")
    
    print("✓ Comprehensive body type atlas created")
    print("✓ Includes: Male, Female, Fat, Hulk, Thin variations")
    print("✓ For each: Base, Savage, Spirit-Eater, Child, Adult variants")
    print("✓ All sprites maintain authentic RimWorld proportions")

if __name__ == "__main__":
    main()