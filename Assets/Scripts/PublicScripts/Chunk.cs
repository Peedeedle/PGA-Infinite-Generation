////////////////////////////////////////////////////////////
// File: Chunk.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: chunk script to get and set blocks in the chunk
//////////////////////////////////////////////////////////// 


using System;
using System.Collections.Generic;
using UnityEngine;

public static class Chunk
{

    // loop through the blocks in the chunk data using the method name "actionToPerform"
    public static void LoopThroughTheBlocks (ChunkData chunkData, Action<int , int , int> actionToPerform) {

        // for each index 0, for each block in the length of blocks
        for (int index = 0; index < chunkData.blocks.Length; index++) {

            // get the position from the blocks
            var position = GetPositionFromIndex(chunkData, index);

            // action to perform method passes in 3 ints
            actionToPerform(position.x, position.y, position.z);

        }

    }

    // Get the position from the index
    private static Vector3Int GetPositionFromIndex(ChunkData chunkData, int index) {

        // calculate x position
        int x = index % chunkData.chunkSize;

        // calculate y position
        int y = (index / chunkData.chunkSize) % chunkData.chunkHeight;

        // calculate z position
        int z = index / (chunkData.chunkSize * chunkData.chunkHeight);

        // Return new x, y and z int values based on the index of the block array
        return new Vector3Int(x, y, z);

    }

    // if the axisCoordinate (x,y,z) is in range of the chunk
    private static bool InRange (ChunkData chunkData, int axisCoordinate) {

        // if the axis coordinate is less than 0 or more than or = to the chunk size then return false 
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.chunkSize)
            return false;

        // return true
        return true;

    }

    // 0 - chunk height
    // if the yCoordinate (y) is in range of the chunk
    private static bool InRangeHeight(ChunkData chunkData, int yCoordinate) {

        // if the axis coordinate is less than 0 or more than or = to the chunk size then return false 
        if (yCoordinate < 0 || yCoordinate >= chunkData.chunkHeight)
            return false;

        // return true
        return true;

    }

    // Overload of the same method below, pass through vector 3 int, chunk data and chunk coordinates
    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates) {

        // return the chunk coordinates (x,y,z) not needed to convert vector 3 into seperate parameters
        return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);

    }

    // get block from chunk coordinates, access block type from block type array, pass in chunk data and x,y,z
    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {

        // if local coordinates are in range of this chunk
        if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z)) {

            // (calculate) get index from position (x,y,z)
            int index = GetIndexFromPosition(chunkData, x, y, z);

            // return the chunk data blocks with the index
            return chunkData.blocks[index];

        }

            // reference to the world, return all of the data for x, y and z to the World script GetBlockFromChunkCoordinates()
            // add the world position to the x, y and z to get the block coordinates in the world space
            return chunkData.WorldReference.GetBlockFromChunkCoordinates
                (chunkData, chunkData.worldPosition.x + x, chunkData.worldPosition.y + y, chunkData.worldPosition.z + z);

    }

    // set the block, pass in chunk data and local position, block type
    public static void SetBlock (ChunkData chunkData, Vector3Int localPosition, BlockType block) {

        // if local position in in range of this chunk
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z)) {

            // get index from position
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);

            // set the chunk data blocks with index to be new block
            chunkData.blocks[index] = block;

        } else {

            // set the block at the world reference (X), local position(Y), world position (Z) and set the block to Block
            WorldDataHelper.SetBlock(chunkData.WorldReference, localPosition + chunkData.worldPosition, block);

            //Debug.Log("Set the block Else Statement");

        }


    }

    // get the index from position in chunk data with a x,y,z value
    private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z) {

        // return reversed calculation from method "GetPositionFromIndex"
        return x + chunkData.chunkSize * y + chunkData.chunkSize * chunkData.chunkHeight * z;

    }

    // get the block in the chunk coordinates, distinguish between world and chunk coordinates
    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos) {

        // return a new vector 3 int
        return new Vector3Int {

            // convert x,y,z world position into chunk coordinates so can access the correct index in block array
            x = pos.x - chunkData.worldPosition.x,
            y = pos.y - chunkData.worldPosition.y,
            z = pos.z - chunkData.worldPosition.z,

        };

    }

    // mesh data that takes in the chunk data
    public static MeshData GetChunkMeshData(ChunkData chunkData) {

        // mesh data takes in a bool to generate mesh data
        MeshData meshData = new MeshData(true);

        // loop through the blocks and look for each block in chunkData, pass in x,y,z coordinate, set meshData = BlockHelper
        // GetMeshData method and pass in chunkData, x, y, z, meshData, chunkData.blocks, select specific block by using the
        // GetIndexFromPosition method, pass in chunkData, x, y, z
        LoopThroughTheBlocks(chunkData, (x, y, z) => meshData = 
        BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.blocks[GetIndexFromPosition(chunkData, x, y, z)]));

        // return the mesh data so then it can be generated
        return meshData;

    }

    // take the x,y,x coordinates from a specific block in the world coordinates
    internal static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z) {

        // create a new vector 3 int called pos
        Vector3Int pos = new Vector3Int {

            // floor to int, x divided by float world chunk size to get a valid position of the chunk
            x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,

            // floor to int, y divided by float world chunk height to get a valid position of the chunk
            y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,

            // floor to int, z divided by float world chunk size to get a valid position of the chunk
            z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize,

        };

        // return the position value
        return pos;

    }

    // get the neighbouring chunk using the chunkData and world position
    internal static List<ChunkData> GetEdgeNeighbourChunk(ChunkData chunkData, Vector3Int worldPosition) {

        // Chunk position = GetBlockInChunkCoOrdinates using the chunk data and world position
        Vector3Int chunkPosition = GetBlockInChunkCoordinates(chunkData, worldPosition);

        // list of chunk data neighbours to update = new list of chunk dataw
        List<ChunkData> neighboursToUpdate = new List<ChunkData>();

        // if the chunk position X = 0
        if (chunkPosition.x == 0) {

            //add the chunk data world reference, world position - vector 3 int right to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.right));

        }

        // if chunk position X == chunk size - 1
        if (chunkPosition.x == chunkData.chunkSize - 1) {

            //add the chunk data world reference, world position + vector 3 int right to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.right));

        }

        // if the chunk position Y = 0
        if (chunkPosition.y == 0) {

            //add the chunk data world reference, world position - vector 3 int up to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.up));

        }

        // if chunk position Y == chunk height - 1
        if (chunkPosition.y == chunkData.chunkHeight - 1) {

            //add the chunk data world reference, world position + vector 3 int up to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.up));

        }

        // if the chunk position Z = 0
        if (chunkPosition.z == 0) {

            //add the chunk data world reference, world position - vector 3 int forward to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.forward));

        }

        // if chunk position Z == chunk size - 1
        if (chunkPosition.z == chunkData.chunkSize - 1) {

            //add the chunk data world reference, world position + vector 3 int forward to the neighbours to update
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.forward));

        }


        // return the neighb ours to update
        return neighboursToUpdate;

    }

    // bool is on edge pass in the chunk data and world position
    internal static bool IsOnEdge (ChunkData chunkData, Vector3Int worldPosition) {

        // vector3int chunk position = get block in chunk coordinates (chunk data, world position)
        Vector3Int chunkPosition = GetBlockInChunkCoordinates(chunkData, worldPosition);

        // If
        if (

            // chunk position x = 0 or chunk position x = chunk size - 1 or
            chunkPosition.x == 0 || chunkPosition.x == chunkData.chunkSize - 1 ||

            // chunk position y = 0 or chunk position y = chunk height - 1 or
            chunkPosition.y == 0 || chunkPosition.y == chunkData.chunkHeight - 1 ||

            // chunk position z = 0 or chunk position z = chunk size - 1
            chunkPosition.z == 0 || chunkPosition.z == chunkData.chunkSize - 1)

            // return true
            return true;

        // if not return false
        return false;

    }

}
