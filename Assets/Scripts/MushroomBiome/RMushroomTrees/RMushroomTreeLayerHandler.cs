////////////////////////////////////////////////////////////
// File: RMushroomTreeLayerHandler.cs
// Author: Jack Peedle
// Date Created: 09/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 09/12/21
// Brief: Script for handling the tree layers and data like where they are allowed to spawn
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMushroomTreeLayerHandler : BlockLayerHandler
{

    // set a height limit for the terrain
    public float terrainHeightLimit = 25;

    // public static list of the tree leaves layout
    public static List<Vector3Int> rMushroomTreeLeavesStaticLayout = new List<Vector3Int>() {

        // all of the vector 3 int positions for each of the leaves on the trees which are generated
        // (Create more refined wayt to set these and different variations)
        
        // Bottom Row
        //              X  Y   Z
        new Vector3Int(-2, 7, -1),
        new Vector3Int(-2, 7, 0),
        new Vector3Int(-2, 7, 1),

        new Vector3Int(-1, 7, -2),
        new Vector3Int(-1, 8, -1), //
        new Vector3Int(-1, 8, 0), //
        new Vector3Int(-1, 8, 1), //
        new Vector3Int(-1, 7, 2),

        new Vector3Int(0, 7, -2),
        new Vector3Int(0, 8, -1), //
        new Vector3Int(0, 8, 0), //
        new Vector3Int(0, 8, 1), //
        new Vector3Int(0, 7, 2),

        new Vector3Int(1, 7, -2),
        new Vector3Int(1, 8, -1), //
        new Vector3Int(1, 8, 0), //
        new Vector3Int(1, 8, 1), //
        new Vector3Int(1, 7, 2),

        new Vector3Int(2, 7, -1),
        new Vector3Int(2, 7, 0),
        new Vector3Int(2, 7, 1),





        new Vector3Int(-2, 6, -1),
        new Vector3Int(-2, 6, 0),
        new Vector3Int(-2, 6, 1),

        new Vector3Int(-1, 6, -2),
        new Vector3Int(-1, 6, 2),

        new Vector3Int(0, 6, -2),
        new Vector3Int(0, 6, 2),

        new Vector3Int(1, 6, -2),
        new Vector3Int(1, 6, 2),

        new Vector3Int(2, 6, -1),
        new Vector3Int(2, 6, 0),
        new Vector3Int(2, 6, 1),





        new Vector3Int(-2, 5, -1),
        new Vector3Int(-2, 5, 0),
        new Vector3Int(-2, 5, 1),

        new Vector3Int(-1, 5, -2),
        new Vector3Int(-1, 5, 2),

        new Vector3Int(0, 5, -2),
        new Vector3Int(0, 5, 2),

        new Vector3Int(1, 5, -2),
        new Vector3Int(1, 5, 2),

        new Vector3Int(2, 5, -1),
        new Vector3Int(2, 5, 0),
        new Vector3Int(2, 5, 1),




        new Vector3Int(-2, 4, -1),
        new Vector3Int(-2, 4, 0),
        new Vector3Int(-2, 4, 1),

        new Vector3Int(-1, 4, -2),
        new Vector3Int(-1, 4, 2),

        new Vector3Int(0, 4, -2),
        new Vector3Int(0, 4, 2),

        new Vector3Int(1, 4, -2),
        new Vector3Int(1, 4, 2),

        new Vector3Int(2, 4, -1),
        new Vector3Int(2, 4, 0),
        new Vector3Int(2, 4, 1),

    };


    // Try handling method
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if the tree is underground then don't place
        if (chunkData.worldPosition.y < 0) {

            // return false
            return false;

        }

        // if the surface height noise is less than the terrain height limit and chunk data tree positions contains a new vector 2
        // int taking in the x and z
        if (surfaceHeightNoise < terrainHeightLimit && chunkData.rMushroomTreeData.rMushroomTreePositions.Contains(new Vector2Int(chunkData.worldPosition.x + x, chunkData.worldPosition.z + z))) {

            // vector 3 int for chunk coordinates, get the block from new vector 3 coordinates
            // (get the block that the tree would be under)
            Vector3Int chunkCoordinates = new Vector3Int(x, surfaceHeightNoise, z);

            // type for the block that is currently under the trees location
            BlockType type = Chunk.GetBlockFromChunkCoordinates(chunkData, chunkCoordinates);

            // if the type of block is grass_Dirt
            if (type == BlockType.MushroomGrass) {

                // set the block of the local position to dirt
                Chunk.SetBlock(chunkData, chunkCoordinates, BlockType.Dirt);

                // for i is less than 5
                for (int i = 1; i < 13; i++) {

                    // local position y = surface noise + 1 (get the position above the surface 5 times)
                    chunkCoordinates.y = surfaceHeightNoise + i;

                    // create a tree log
                    Chunk.SetBlock(chunkData, chunkCoordinates, BlockType.RMushroomTree);

                }

                // for each vector 3 int in leaf position in the tree static layout
                foreach (Vector3Int leafPosition in rMushroomTreeLeavesStaticLayout) {

                    // add the tree leaves solid to the vector 3 ints (X, Y and Z), surface height noise + 5 = i loop on line 106
                    chunkData.rMushroomTreeData.rMushroomTreeLeavesSolid.Add(new Vector3Int(x + leafPosition.x, surfaceHeightNoise + 5 
                        + leafPosition.y, z + leafPosition.z));

                }

            }


        }

        // return false
        return false;

    }

}
