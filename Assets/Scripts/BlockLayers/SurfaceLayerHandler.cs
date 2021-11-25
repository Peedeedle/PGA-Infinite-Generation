////////////////////////////////////////////////////////////
// File: SurfaceLayerHandler.cs
// Author: Jack Peedle
// Date Created: 31/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Handler for the Surface layer 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceLayerHandler : BlockLayerHandler
{

    // public blocktype surfaceblocktype
    public BlockType surfaceBlockType;

    // return true if the handler has handled the specific layer and input parameters
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if y is equal to the surfaceheightnoise
        if (y == surfaceHeightNoise) {

            // new vector 3 position
            Vector3Int pos = new Vector3Int(x, y, z);

            // set the block to be the surface block type
            Chunk.SetBlock(chunkData, pos, surfaceBlockType);

            // return true
            return true;

        }

        // if not, return false
        return false;

    }
}
