using UnityEngine;
using Verse;

namespace ZhulTribe.Graphics
{
    public class Graphic_Atlas : Graphic
    {
        private static Texture2D atlasTex;
        private const int PAWN_TILE  = 1024;
        private const int MOTE_TILE  = 256;
        private const int ALTAR_TILE = 512;

        // bottom-left corners of each tile in pixels
        private static readonly Vector2Int[] tilePos = {
            new Vector2Int(    0, 3072), // 0: AlienHumanoid (1024)
            new Vector2Int(1024, 3072), // 1: CurledSavage
            new Vector2Int(2048, 3072), // 2: SpiritEater
            new Vector2Int(3072, 3072), // 3: Child
            new Vector2Int(    0, 2816), // 4: OilSwirl (256)
            new Vector2Int( 512, 2816)   // 5: BoneAltar (512)
        };

        public int atlasIndex;  // set by XML

        public override void Init(GraphicRequest req)
        {
            base.Init(req);
            if (atlasTex == null)
                atlasTex = ContentFinder<Texture2D>.Get(req.path, true);
        }

        public override Material MatAt(Rot4 rot, Thing thing = null)
        {
            if (atlasTex == null) return BaseContent.BadMat;
            
            // choose tile size based on atlas index
            int size = atlasIndex <= 3 ? PAWN_TILE
                     : atlasIndex == 4 ? MOTE_TILE
                                       : ALTAR_TILE;

            var pos = tilePos[atlasIndex];
            float uMin = pos.x / (float)atlasTex.width;
            float vMin = pos.y / (float)atlasTex.height;
            float uMax = (pos.x + size) / (float)atlasTex.width;
            float vMax = (pos.y + size) / (float)atlasTex.height;

            // create material with proper RimWorld API
            MaterialRequest matReq = new MaterialRequest(atlasTex, ShaderDatabase.Cutout);
            Material mat = MaterialPool.MatFrom(matReq);
            mat.mainTextureOffset = new Vector2(uMin, vMin);
            mat.mainTextureScale = new Vector2(uMax - uMin, vMax - vMin);
            
            return mat;
        }

        public override void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
        {
            base.DrawWorker(loc, rot, thingDef, thing, extraRotation);
        }
    }
}