////////////////////////////////////////////////////////////
// File: AirLayerHandler.cs
// Author: Jack Peedle
// Date Created: 31/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 31/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLayerHandler : BlockLayerHandler
{


    // return true if the handler has handled the specific layer and input parameters
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if y is more than the surface height
        if (y > surfaceHeightNoise) {

            // new vector 3 position
            Vector3Int pos = new Vector3Int(x, y, z);

            // set the block to be air
            Chunk.SetBlock(chunkData, pos, BlockType.Air);

            // return true
            return true;

        }

        // if not, return false
        return false;

    }

}