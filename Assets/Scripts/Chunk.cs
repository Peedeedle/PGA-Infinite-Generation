////////////////////////////////////////////////////////////
// File: Chunk.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 21/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

/*

using System;
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

            // 
            throw new Exception("Ask world for appropriate chunk");

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

}
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public static class Chunk
{

    public static void LoopThroughTheBlocks(ChunkData chunkData, Action<int, int, int> actionToPerform) {
        for (int index = 0; index < chunkData.blocks.Length; index++) {
            var position = GetPostitionFromIndex(chunkData, index);
            actionToPerform(position.x, position.y, position.z);
        }
    }

    private static Vector3Int GetPostitionFromIndex(ChunkData chunkData, int index) {
        int x = index % chunkData.chunkSize;
        int y = (index / chunkData.chunkSize) % chunkData.chunkHeight;
        int z = index / (chunkData.chunkSize * chunkData.chunkHeight);
        return new Vector3Int(x, y, z);
    }

    //in chunk coordinate system
    private static bool InRange(ChunkData chunkData, int axisCoordinate) {
        if (axisCoordinate < 0 || axisCoordinate >= chunkData.chunkSize)
            return false;

        return true;
    }

    //in chunk coordinate system
    private static bool InRangeHeight(ChunkData chunkData, int ycoordinate) {
        if (ycoordinate < 0 || ycoordinate >= chunkData.chunkHeight)
            return false;

        return true;
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, Vector3Int chunkCoordinates) {
        return GetBlockFromChunkCoordinates(chunkData, chunkCoordinates.x, chunkCoordinates.y, chunkCoordinates.z);
    }

    public static BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {
        if (InRange(chunkData, x) && InRangeHeight(chunkData, y) && InRange(chunkData, z)) {
            int index = GetIndexFromPosition(chunkData, x, y, z);
            return chunkData.blocks[index];
        }

        return chunkData.WorldReference.GetBlockFromChunkCoordinates(chunkData, chunkData.worldPosition.x + x, chunkData.worldPosition.y + y, chunkData.worldPosition.z + z);
    }

    public static void SetBlock(ChunkData chunkData, Vector3Int localPosition, BlockType block) {
        if (InRange(chunkData, localPosition.x) && InRangeHeight(chunkData, localPosition.y) && InRange(chunkData, localPosition.z)) {
            int index = GetIndexFromPosition(chunkData, localPosition.x, localPosition.y, localPosition.z);
            chunkData.blocks[index] = block;
        } else {
            WorldDataHelper.SetBlock(chunkData.WorldReference, localPosition + chunkData.worldPosition, block);
        }
    }

    private static int GetIndexFromPosition(ChunkData chunkData, int x, int y, int z) {
        return x + chunkData.chunkSize * y + chunkData.chunkSize * chunkData.chunkHeight * z;
    }

    public static Vector3Int GetBlockInChunkCoordinates(ChunkData chunkData, Vector3Int pos) {
        return new Vector3Int {
            x = pos.x - chunkData.worldPosition.x,
            y = pos.y - chunkData.worldPosition.y,
            z = pos.z - chunkData.worldPosition.z
        };
    }

    public static MeshData GetChunkMeshData(ChunkData chunkData) {
        MeshData meshData = new MeshData(true);

        LoopThroughTheBlocks(chunkData, (x, y, z) => meshData = BlockHelper.GetMeshData(chunkData, x, y, z, meshData, chunkData.blocks[GetIndexFromPosition(chunkData, x, y, z)]));


        return meshData;
    }

    internal static Vector3Int ChunkPositionFromBlockCoords(World world, int x, int y, int z) {
        Vector3Int pos = new Vector3Int {
            x = Mathf.FloorToInt(x / (float)world.chunkSize) * world.chunkSize,
            y = Mathf.FloorToInt(y / (float)world.chunkHeight) * world.chunkHeight,
            z = Mathf.FloorToInt(z / (float)world.chunkSize) * world.chunkSize
        };
        return pos;
    }

    internal static List<ChunkData> GetEdgeNeighbourChunk(ChunkData chunkData, Vector3Int worldPosition) {
        Vector3Int chunkPosition = GetBlockInChunkCoordinates(chunkData, worldPosition);
        List<ChunkData> neighboursToUpdate = new List<ChunkData>();
        if (chunkPosition.x == 0) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.right));
        }
        if (chunkPosition.x == chunkData.chunkSize - 1) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.right));
        }
        if (chunkPosition.y == 0) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.up));
        }
        if (chunkPosition.y == chunkData.chunkHeight - 1) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.up));
        }
        if (chunkPosition.z == 0) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition - Vector3Int.forward));
        }
        if (chunkPosition.z == chunkData.chunkSize - 1) {
            neighboursToUpdate.Add(WorldDataHelper.GetChunkData(chunkData.WorldReference, worldPosition + Vector3Int.forward));
        }
        return neighboursToUpdate;
    }

    internal static bool IsOnEdge(ChunkData chunkData, Vector3Int worldPosition) {
        Vector3Int chunkPosition = GetBlockInChunkCoordinates(chunkData, worldPosition);
        if (
            chunkPosition.x == 0 || chunkPosition.x == chunkData.chunkSize - 1 ||
            chunkPosition.y == 0 || chunkPosition.y == chunkData.chunkHeight - 1 ||
            chunkPosition.z == 0 || chunkPosition.z == chunkData.chunkSize - 1
            )
            return true;
        return false;
    }
}