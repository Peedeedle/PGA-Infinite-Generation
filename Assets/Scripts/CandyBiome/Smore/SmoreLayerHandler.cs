////////////////////////////////////////////////////////////
// File: SmoreLayerHandler.cs
// Author: Jack Peedle
// Date Created: 12/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/12/21
// Brief: Script for handling the tree layers and data like where they are allowed to spawn
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoreLayerHandler : BlockLayerHandler
{

    // set a height limit for the terrain
    public float terrainHeightLimit = 25;

    // Try handling method
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if the tree is underground then don't place
        if (chunkData.worldPosition.y < 0) {

            // return false
            return false;

        }

        // if the surface height noise is less than the terrain height limit and chunk data tree positions contains a new vector 2
        // int taking in the x and z
        if (surfaceHeightNoise < terrainHeightLimit && chunkData.smoreData.smorePositions.Contains(new Vector2Int(chunkData.worldPosition.x + x, chunkData.worldPosition.z + z))) {

            // vector 3 int for chunk coordinates, get the block from new vector 3 coordinates
            // (get the block that the tree would be under)
            Vector3Int chunkCoordinates = new Vector3Int(x, surfaceHeightNoise, z);

            // type for the block that is currently under the trees location
            BlockType type = Chunk.GetBlockFromChunkCoordinates(chunkData, chunkCoordinates);

            // if the type of block is candy grass
            if (type == BlockType.CandyGrass) {

                // set the block of the local position to candy grass
                Chunk.SetBlock(chunkData, chunkCoordinates, BlockType.CandyGrass);

                // for i is less than 5
                for (int i = 1; i < 2; i++) {

                    // local position y = surface noise + 1 (get the position above the surface 5 times)
                    chunkCoordinates.y = surfaceHeightNoise + i;

                    // create a smore
                    Chunk.SetBlock(chunkData, chunkCoordinates, BlockType.Smore);

                }

            }


        }

        // return false
        return false;

    }

}
