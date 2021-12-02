////////////////////////////////////////////////////////////
// File: WorldDataHelper.cs
// Author: Jack Peedle
// Date Created: 08/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/11/21
// Brief: helper for the world data 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldDataHelper
{
    
    // chunk position from block coordinates passing in thw world and world block position
    public static Vector3Int ChunkPositionFromBlockCoords(World world, Vector3Int worldBlockPosition) {

        // return new vector3Int
        return new Vector3Int {

            // x = floor to int passing in the world block position x divided by the chunk size float, * chunk size
            x = Mathf.FloorToInt(worldBlockPosition.x / (float)world.chunkSize) * world.chunkSize,

            // y = floor to int passing in the world block position y divided by the chunk height float, * chunk size
            y = Mathf.FloorToInt(worldBlockPosition.y / (float)world.chunkHeight) * world.chunkHeight,

            // z = floor to int passing in the world block position z divided by the chunk size float, * chunk size
            z = Mathf.FloorToInt(worldBlockPosition.z / (float)world.chunkSize) * world.chunkSize,

        };

    }

    // list of vector 3 ints called GetChunkPositionsAroundStartingPosition passing in the world and starting position
    internal static List<Vector3Int> GetChunkPositionsAroundStartingPosition(World world, Vector3Int startingPosition) {

        // int start x = starting position x - chunkDrawing range * world chunk size
        int startX = startingPosition.x - (world.chunkDrawingRange) * world.chunkSize;

        // int start z = starting position z - chunkDrawing range * world chunk size
        int startZ = startingPosition.z - (world.chunkDrawingRange) * world.chunkSize;

        // int end x = starting position x + chunkDrawing range * world chunk size
        int endX = startingPosition.x + (world.chunkDrawingRange) * world.chunkSize;

        // int end z = starting position x + chunkDrawing range * world chunk size
        int endZ = startingPosition.z + (world.chunkDrawingRange) * world.chunkSize;


        // list of vector3Ints called chunkPositionsToCreate = new list of vector3Ints
        List<Vector3Int> chunkPositionsToCreate = new List<Vector3Int>();

        // for int x = startingX is less than or = endX, x += chunk size
        for (int x = startX; x <= endX; x += world.chunkSize) {

            // for int z = startingZ is less than or = endZ, Z += chunk size
            for (int z = startZ; z <= endZ; z += world.chunkSize) {

                // vector3Int chunk position = ChunkPositionFromBlockCoords passing in the world and a new vector3Int of (x, 0, z)
                Vector3Int chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, 0, z));

                // add chunkPosition to chunkPositionsToCreate
                chunkPositionsToCreate.Add(chunkPos);

                // if x more than or = starting position X - world chunk size and x less than starting position X + world chunk size
                // and z more than or = starting position Z - world chunk size and z less than starting position Z + world chunk size
                if (x >= startingPosition.x - world.chunkSize && x <= startingPosition.x + world.chunkSize 
                    && z >= startingPosition.z - world.chunkSize && z <= startingPosition.z + world.chunkSize) {

                    // for y = - chunk height y more than or = starting position y - chunk height times 2, y-= chunk height
                    for (int y = -world.chunkHeight; y >= startingPosition.y - world.chunkHeight * 2; y -= world.chunkHeight) {

                        // chunkPosition = ChunkPositionFromBlockCoords passing in world and new vector3Int(x, y, z)
                        chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, y, z));

                        // add the chunkPosition to the chunkPositionsToCreate
                        chunkPositionsToCreate.Add(chunkPos);

                    }


                }

            }


        }

        // return the chunkPositionsToCreate
        return chunkPositionsToCreate;

    }



    // remove chunk data passing in the world and position
    internal static void RemoveChunkData(World world, Vector3Int pos) {

        //  remove the position from the chunkDataDictionary
        world.worldData.chunkDataDictionary.Remove(pos);

    }

    // remove the chunk passing in the world and positions
    internal static void RemoveChunk(World world, Vector3Int pos) {

        // chunk = null
        ChunkRenderer chunk = null;

        // try to get value of position and output the chunk of the chunkDictionary
        if (world.worldData.chunkDictionary.TryGetValue(pos, out chunk)) {

            // Havent changed and updated world script yet
            world.worldRenderer.RemoveChunk(chunk);

            // remove the position from the chunkDictionary
            world.worldData.chunkDictionary.Remove(pos);

        }

    }


    // Get a list of vector3Ints called GetDataPositionsAroundStartingPosition passing in the world and starting position
    internal static List <Vector3Int> GetDataPositionsAroundStartingPosition(World world, Vector3Int startingPosition) {

        // int start x = starting position x - chunkDrawing range + 1 * world chunk size
        int startX = startingPosition.x - (world.chunkDrawingRange + 1) * world.chunkSize;

        // int start z = starting position z - chunkDrawing range + 1 * world chunk size
        int startZ = startingPosition.z - (world.chunkDrawingRange + 1) * world.chunkSize;

        // int end x = starting position x + chunkDrawing range + 1 * world chunk size
        int endX = startingPosition.x + (world.chunkDrawingRange + 1) * world.chunkSize;

        // int end z = starting position x + chunkDrawing range + 1 * world chunk size
        int endZ = startingPosition.z + (world.chunkDrawingRange + 1) * world.chunkSize;


        // list of vector3Ints called chunkDataPositionsToCreate = new list of vector3Ints
        List<Vector3Int> chunkDataPositionsToCreate = new List<Vector3Int>();

        // for int x = startingX is less than or = endX, x += chunk size
        for (int x = startX; x <= endX; x += world.chunkSize) {

            // for int z = startingZ is less than or = endZ, Z += chunk size
            for (int z = startZ; z <= endZ; z += world.chunkSize) {

                // vector3Int chunk position = ChunkPositionFromBlockCoords passing in the world and a new vector3Int of (x, 0, z)
                Vector3Int chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, 0, z));

                // add chunkPosition to chunkPositionsToCreate
                chunkDataPositionsToCreate.Add(chunkPos);

                // if x more than or = starting position X - world chunk size and x less than starting position X + world chunk size
                // and z more than or = starting position Z - world chunk size and z less than starting position Z + world chunk size
                if (x >= startingPosition.x - world.chunkSize && x <= startingPosition.x + world.chunkSize
                    && z >= startingPosition.z - world.chunkSize && z <= startingPosition.z + world.chunkSize) {

                    // for y = - chunk height y more than or = starting position y - chunk height times 2, y-= chunk height
                    for (int y = -world.chunkHeight; y >= startingPosition.y - world.chunkHeight * 2; y -= world.chunkHeight) {

                        // chunkPosition = ChunkPositionFromBlockCoords passing in world and new vector3Int(x, y, z)
                        chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, y, z));

                        // add the chunkPosition to the chunkPositionsToCreate
                        chunkDataPositionsToCreate.Add(chunkPos);

                    }


                }

            }


        }

        // return the chunkPositionsToCreate
        return chunkDataPositionsToCreate;

    }


    // get the chunk passing in the world reference and world position
    internal static ChunkRenderer GetChunk (World worldReference, Vector3Int worldPosition) {

        // if the chunkDictionary contains the world position
        if (worldReference.worldData.chunkDictionary.ContainsKey(worldPosition))

            // return the world position in the chunkDictionary
            return worldReference.worldData.chunkDictionary[worldPosition];

        // return null
        return null;

    }

    // set block passing in the world reference, world block position and block type
    internal static void SetBlock(World worldReference, Vector3Int worldBlockPosition, BlockType blockType) {

        // chunk data = get chunk data passing in the world reference and world block position
        ChunkData chunkData = GetChunkData(worldReference, worldBlockPosition);

        // if chunk data is not = null
        if (chunkData != null) {

            // local position = GetBlockInChunkCoordinates passing in the chunk data and world block position
            Vector3Int localPosition = Chunk.GetBlockInChunkCoordinates(chunkData, worldBlockPosition);

            // set the block passing in the chunk data, local position and block type
            Chunk.SetBlock(chunkData, localPosition, blockType);

        }

    }


    // getChunkData passing in the world reference and world block position
    public static ChunkData GetChunkData(World worldReference, Vector3Int worldBlockPosition) {

        // chunk position = ChunkPositionFromBlockCoords passing in the world reference and worldBlockPosition
        Vector3Int chunkPosition = ChunkPositionFromBlockCoords(worldReference, worldBlockPosition);

        // container chunk = null
        ChunkData containerChunk = null;

        // try get chunk position value and output container chunk into chunkDataDictionary
        worldReference.worldData.chunkDataDictionary.TryGetValue(chunkPosition, out containerChunk);

        // return the container
        return containerChunk;


    }

    // GetUndeedData as a list of vector3ints and pass in world data and a list of all vector3Ints in allChunkDataPositionsNeeded
    internal static List<Vector3Int> GetUnneededData(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded) {

        // return the chunkDataDictionary (pos => allChunkDataPositionsNeeded.Contains(pos) = false and
        // chunkDataDictionary[pos].modifiedByThePlayer == false to list
        return worldData.chunkDataDictionary.Keys.Where(pos => allChunkDataPositionsNeeded.Contains(pos) == false &&
        worldData.chunkDataDictionary[pos].modifiedByThePlayer == false).ToList();


    }

    // GetUndeedData as a list of vector3ints and pass in world data and a list of all vector3Ints in allChunkPositionsNeeded
    internal static List<Vector3Int> GetUnneededChunks(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded) {

        // positionsToRemove = new list of positionToRemove
        List<Vector3Int> positionToRemove = new List<Vector3Int>();

        // for each position in chunkDictionary of the position => allChunkPositionsNeeded.Contains(pos) = false
        foreach (var pos in worldData.chunkDictionary.Keys.Where(pos => allChunkPositionsNeeded.Contains(pos) == false)) {    

            // if chunkdictionary contains position
            if (worldData.chunkDictionary.ContainsKey(pos)) {

                // add the position to the positionToRemove
                positionToRemove.Add(pos);

            }

        }

        // return the position to remove
        return positionToRemove;

    }

    // SelectPositionsToCreate of list of vector3Ints passing in the world data, allChunkPositionsNeeded and starting position
    internal static List<Vector3Int> SelectPositionsToCreate(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded, 
        Vector3Int startingPosition) {

        // return all chunk positions needed at the pos => chunkDictionary.ContainsKey(pos) = false, add
        // in order of distance to list
        return allChunkPositionsNeeded.Where(pos => worldData.chunkDictionary.ContainsKey(pos) == false).OrderBy
            (pos => Vector3.Distance(startingPosition, pos)).ToList();

    }

    // SelectDataPositionsToCreate of list of vector3Ints passing in the world data, allChunkDataPositionsNeeded and starting position
    internal static List<Vector3Int> SelectDataPositionsToCreate(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded,
        Vector3Int startingPosition) {

        // return all chunk data positions needed at the pos => chunkDictionary.ContainsKey(pos) = false, add
        // in order of distance to list
        return allChunkDataPositionsNeeded.Where(pos => worldData.chunkDataDictionary.ContainsKey(pos) == false).OrderBy
            (pos => Vector3.Distance(startingPosition, pos)).ToList();

    }


}