////////////////////////////////////////////////////////////
// File: WaterLayerHandler.cs
// Author: Jack Peedle
// Date Created: 31/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Handler for the Water layer 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWaterLayerHandler : BlockLayerHandler
{

    // int for the water level
    public int sandWaterLevel = 1;

    // return true if the handler has handled the specific layer and input parameters
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {
        
        // if y is more than the surface height and is less than or equal to the water level
        if (y > surfaceHeightNoise && y <= sandWaterLevel) {

            // new vector 3 position
            Vector3Int pos = new Vector3Int(x, y, z);

            // set the block to be water
            Chunk.SetBlock(chunkData, pos, BlockType.SandWater);

            // if water is one block above the surface
            if (y == surfaceHeightNoise + 1) {

                // set the y position to the surface height
                pos.y = surfaceHeightNoise;

                // set the block to be sand
                Chunk.SetBlock(chunkData, pos, BlockType.Sand);


            }

            // return true
            return true;

        }

        // if not, return false
        return false;

    }
}
