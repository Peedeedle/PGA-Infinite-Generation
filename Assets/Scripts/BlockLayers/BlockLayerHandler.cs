////////////////////////////////////////////////////////////
// File: BlockLayerHandler.cs
// Author: Jack Peedle
// Date Created: 31/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Handler for the Block layer 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// assing in the inspector
public abstract class BlockLayerHandler : MonoBehaviour
{


    // next for the block layer (when current one cannot handle the current parameter)
    [SerializeField]
    private BlockLayerHandler Next;

    // take in the chunk currently being modified, x, y, z, surface height which is calculated in BiomeGenerator and the map seed
    // Offset
    public bool Handle(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if TryHandling returned true with this data
        if (TryHandling(chunkData, x, y, z, surfaceHeightNoise, mapSeedOffset)) {

            // return true
            return true;

        }
            
        
        // if the next block layer handler is available ( if previous one hasn't handled the block )
        if (Next != null) {

            // call the next handle and pass the same data
            return Next.Handle(chunkData, x, y, z, surfaceHeightNoise, mapSeedOffset);

        }

            // if it is null then return false
            return false;

    }

    // return true if the handler has handled the specific layer and input parameters
    protected abstract bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset);

}
