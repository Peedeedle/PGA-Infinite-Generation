////////////////////////////////////////////////////////////
// File: WorldDataHelper.cs
// Author: Jack Peedle
// Date Created: 08/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 08/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class WorldDataHelper
{
    
    //
    public static Vector3Int ChunkPositionFromBlockCoords(World world, Vector3Int worldBlockPosition) {

        //
        return new Vector3Int {

            //
            x = Mathf.FloorToInt(worldBlockPosition.x / (float)world.chunkSize) * world.chunkSize,

            //
            y = Mathf.FloorToInt(worldBlockPosition.y / (float)world.chunkHeight) * world.chunkHeight,

            //
            z = Mathf.FloorToInt(worldBlockPosition.z / (float)world.chunkSize) * world.chunkSize,

        };

    }

    //
    internal static List<Vector3Int> GetChunkPositionsAroundStartingPosition(World world, Vector3Int startingPosition) {

        //
        int startX = startingPosition.x - (world.chunkDrawingRange) * world.chunkSize;

        //
        int startZ = startingPosition.z - (world.chunkDrawingRange) * world.chunkSize;

        //
        int endX = startingPosition.x + (world.chunkDrawingRange) * world.chunkSize;

        //
        int endZ = startingPosition.z + (world.chunkDrawingRange) * world.chunkSize;


        //
        List<Vector3Int> chunkPositionsToCreate = new List<Vector3Int>();

        //
        for (int x = startX; x <= endX; x += world.chunkSize) {

            //
            for (int z = startZ; z <= endZ; z += world.chunkSize) {

                //
                Vector3Int chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, 0, z));

                //
                chunkPositionsToCreate.Add(chunkPos);

                //
                if (x >= startingPosition.x - world.chunkSize && x <= startingPosition.x + world.chunkSize 
                    && z >= startingPosition.z - world.chunkSize && z <= startingPosition.z + world.chunkSize) {

                    //
                    for (int y = -world.chunkHeight; y >= startingPosition.y - world.chunkHeight * 2; y -= world.chunkHeight) {

                        //
                        chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, y, z));

                        //
                        chunkPositionsToCreate.Add(chunkPos);

                    }


                }

            }


        }

        //
        return chunkPositionsToCreate;

    }



    //
    internal static void RemoveChunkData(World world, Vector3Int pos) {

        //
        world.worldData.chunkDataDictionary.Remove(pos);

    }

    //
    internal static void RemoveChunk(World world, Vector3Int pos) {

        //
        ChunkRenderer chunk = null;

        //
        if (world.worldData.chunkDictionary.TryGetValue(pos, out chunk)) {

            // Havent changed and updated world script yet
            world.worldRenderer.RemoveChunk(chunk);

            //
            world.worldData.chunkDictionary.Remove(pos);

        }

    }


    //
    internal static List <Vector3Int> GetDataPositionsAroundStartingPosition(World world, Vector3Int startingPosition) {

        //
        int startX = startingPosition.x - (world.chunkDrawingRange + 1) * world.chunkSize;

        //
        int startZ = startingPosition.z - (world.chunkDrawingRange + 1) * world.chunkSize;

        //
        int endX = startingPosition.x + (world.chunkDrawingRange + 1) * world.chunkSize;

        //
        int endZ = startingPosition.z + (world.chunkDrawingRange + 1) * world.chunkSize;


        //
        List<Vector3Int> chunkDataPositionsToCreate = new List<Vector3Int>();

        //
        for (int x = startX; x <= endX; x += world.chunkSize) {

            //
            for (int z = startZ; z <= endZ; z += world.chunkSize) {

                //
                Vector3Int chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, 0, z));

                //
                chunkDataPositionsToCreate.Add(chunkPos);

                //
                if (x >= startingPosition.x - world.chunkSize && x <= startingPosition.x + world.chunkSize
                    && z >= startingPosition.z - world.chunkSize && z <= startingPosition.z + world.chunkSize) {

                    //
                    for (int y = -world.chunkHeight; y >= startingPosition.y - world.chunkHeight * 2; y -= world.chunkHeight) {

                        //
                        chunkPos = ChunkPositionFromBlockCoords(world, new Vector3Int(x, y, z));

                        //
                        chunkDataPositionsToCreate.Add(chunkPos);

                    }


                }

            }


        }

        //
        return chunkDataPositionsToCreate;

    }


    //
    internal static ChunkRenderer GetChunk (World worldReference, Vector3Int worldPosition) {

        //
        if (worldReference.worldData.chunkDictionary.ContainsKey(worldPosition))
            return worldReference.worldData.chunkDictionary[worldPosition];

        //
        return null;

    }

    //
    internal static void SetBlock(World worldReference, Vector3Int worldBlockPosition, BlockType blockType) {

        //
        ChunkData chunkData = GetChunkData(worldReference, worldBlockPosition);

        //
        if (chunkData != null) {

            //
            Vector3Int localPosition = Chunk.GetBlockInChunkCoordinates(chunkData, worldBlockPosition);

            //
            Chunk.SetBlock(chunkData, localPosition, blockType);

        }

    }


    //
    public static ChunkData GetChunkData(World worldReference, Vector3Int worldBlockPosition) {

        //
        Vector3Int chunkPosition = ChunkPositionFromBlockCoords(worldReference, worldBlockPosition);

        //
        ChunkData containerChunk = null;

        //
        worldReference.worldData.chunkDataDictionary.TryGetValue(chunkPosition, out containerChunk);

        //
        return containerChunk;


    }


    //
    internal static List<Vector3Int> GetUnneededData(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded) {

        //
        return worldData.chunkDataDictionary.Keys.Where(pos => allChunkDataPositionsNeeded.Contains(pos) == false &&
        worldData.chunkDataDictionary[pos].modifiedByThePlayer == false).ToList();



    }


    //
    internal static List<Vector3Int> GetUnneededChunks(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded) {

        //
        List<Vector3Int> positionToRemove = new List<Vector3Int>();

        //
        foreach (var pos in worldData.chunkDictionary.Keys.Where(pos => allChunkPositionsNeeded.Contains(pos) == false)) {    

            //
            if (worldData.chunkDictionary.ContainsKey(pos)) {

                //
                positionToRemove.Add(pos);

            }

        }

        //
        return positionToRemove;

    }


    //
    internal static List<Vector3Int> SelectPositionsToCreate(WorldData worldData, List<Vector3Int> allChunkPositionsNeeded, 
        Vector3Int startingPosition) {

        //
        return allChunkPositionsNeeded.Where(pos => worldData.chunkDictionary.ContainsKey(pos) == false).OrderBy
            (pos => Vector3.Distance(startingPosition, pos)).ToList();

    }


    //
    internal static List<Vector3Int> SelectDataPositionsToCreate(WorldData worldData, List<Vector3Int> allChunkDataPositionsNeeded,
        Vector3Int startingPosition) {

        //
        return allChunkDataPositionsNeeded.Where(pos => worldData.chunkDataDictionary.ContainsKey(pos) == false).OrderBy
            (pos => Vector3.Distance(startingPosition, pos)).ToList();

    }



}