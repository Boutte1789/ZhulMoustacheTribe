from PIL import Image, ImageDraw

# Create 4096x4096 test atlas
img = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
draw = ImageDraw.Draw(img)

# Draw colored rectangles for each sprite location
# AlienHumanoid (0,3072) - olive green
draw.rectangle([0, 3072, 1024, 4096], fill=(122, 119, 96, 255))
draw.text((20, 3100), "AlienHumanoid", fill=(255,255,255,255))

# CurledSavage (1024,3072) - darker olive
draw.rectangle([1024, 3072, 2048, 4096], fill=(90, 86, 66, 255))
draw.text((1044, 3100), "CurledSavage", fill=(255,255,255,255))

# SpiritEater (2048,3072) - reddish olive
draw.rectangle([2048, 3072, 3072, 4096], fill=(122, 96, 96, 255))
draw.text((2068, 3100), "SpiritEater", fill=(255,255,255,255))

# Child (3072,3072) - lighter olive
draw.rectangle([3072, 3072, 4096, 4096], fill=(140, 136, 110, 255))
draw.text((3092, 3100), "Child", fill=(255,255,255,255))

# OilSwirl (0,2816) - purple swirl
draw.rectangle([0, 2816, 256, 3072], fill=(128, 0, 128, 255))
draw.text((20, 2830), "OilSwirl", fill=(255,255,255,255))

# BoneAltar (512,2816) - bone color
draw.rectangle([512, 2816, 1024, 3328], fill=(245, 245, 220, 255))
draw.text((532, 2830), "BoneAltar", fill=(0,0,0,255))

img.save('ZhulSheet.png')
print("Test atlas created: ZhulSheet.png")
