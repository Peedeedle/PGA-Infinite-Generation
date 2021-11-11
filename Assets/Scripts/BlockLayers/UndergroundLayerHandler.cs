////////////////////////////////////////////////////////////
// File: UndergroundLayerHandler.cs
// Author: Jack Peedle
// Date Created: 31/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 31/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundLayerHandler : BlockLayerHandler
{

    // public blocktype undergroundblocktype
    public BlockType undergroundBlockType;

    // return true if the handler has handled the specific layer and input parameters
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if y is less than the surface height noise
        if (y < surfaceHeightNoise) {

            // new vector 3 position
            Vector3Int pos = new Vector3Int(x, y - chunkData.worldPosition.y, z);

            // set the block to be the underground block type
            Chunk.SetBlock(chunkData, pos, undergroundBlockType);

            // return true
            return true;

        }

        // if not, return false
        return false;

    }
}