////////////////////////////////////////////////////////////
// File: World.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 25/10/21
// Brief: 
//////////////////////////////////////////////////////////// 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    // int for map size in chunks
    public int mapSizeInChunks = 6;

    // int for the chunk size and chunk height
    public int chunkSize = 16, chunkHeight = 16;

    // int for the water threshold (water surface at specific height)
    public int waterThreshold = 50;

    // int for the noise scale of the chunk / mesh (Generate noise (Perlin Noise))
    public float noiseScale = 0.03f;

    // chunk prefab GameObject
    public GameObject chunkPrefab;


    // Dictionary of Vector3Ints and ChunkData (Store the data of chunks we want to generate on map)
    Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();

    // Dictionary of Vector3Ints and ChunkRenderer (Remove chunks from the map)
    Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();




    // Generate the World / Meshes
    public void GenerateWorld() {

        // clear the chunk data dictionary
        chunkDataDictionary.Clear();

        // for each (ALL) chunk in the ChunkDataDictionary
        foreach (ChunkRenderer chunk in chunkDictionary.Values) {

            // destroy the chunks
            Destroy(chunk.gameObject);

        }

        // clear the ChunkDictionary
        chunkDictionary.Clear();

        // for each x value in the mapSizeInChunks
        for (int x = 0; x < mapSizeInChunks; x++) {

            // for each z value in the mapSizeInChunks
            for (int z = 0; z < mapSizeInChunks; z++) {

                // generate data using new chunk data of (chunk size, ChunkHeight, this (world reference), new vector3Int position,
                // (6 x 16) (0) (6 x 16) == (X) (Y) (Z)
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, z * chunkSize));

                // Generate the voxels using data
                GenerateVoxels(data);

                // add to the chunkDataDictionary
                chunkDataDictionary.Add(data.worldPosition, data);

            }

        }

        // for each data in the chunkDataDictionary values
        foreach (ChunkData data in chunkDataDictionary.Values) {

            // generate mesh data in the GetChunkMeshData method passing in data
            MeshData meshData = Chunk.GetChunkMeshData(data);

            // chunk object gameobject = instantiated chunk prefab with the data from the world position and no rotation
            GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);

            // chunk renderer = the chunk objects ChunkRenderer
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();

            // add the world position data and the chunkRenderer into the chunkDictionary
            chunkDictionary.Add(data.worldPosition, chunkRenderer);

            // initialize the chunk using the data
            chunkRenderer.InitializeChunk(data);

            // update the chunk using the mesh data
            chunkRenderer.UpdateChunk(meshData);

        }

    }



    // Generate Voxels which takes in the chunk data
    private void GenerateVoxels(ChunkData data) {

        // look for each x local coordinate from 0 - chunksize
        for (int x = 0; x < data.chunkSize; x++) {

            // look for each z local coordinate from 0 - chunksize
            for (int z = 0; z < data.chunkSize; z++) {

                // generate noise value to set level of the ground using world position (x and z values) then * noiseScale
                float noiseValue = Mathf.PerlinNoise((data.worldPosition.x + x) * noiseScale, 
                    (data.worldPosition.z + z) * noiseScale);

                // int for ground position where dirt and grass will be generated, above = air, below = water etc
                int groundPosition = Mathf.RoundToInt(noiseValue * chunkHeight);

                // look for each z local coordinate from 0 - chunkHeight
                // for chunks above the ground
                for (int y = 0; y < chunkHeight; y++) {

                    // create block type of dirt
                    BlockType voxelType = BlockType.Dirt;

                    // if y value is more than ground position
                    if (y > groundPosition) {

                        // if y value is less than the water threshold
                        if (y < waterThreshold) {

                            // block type = water
                            voxelType = BlockType.Water;

                        } else {

                            // else the block type is air
                            voxelType = BlockType.Air;

                        }

                        // else if y is = to the ground position
                    } else if (y == groundPosition) {

                        // block type = grass
                        voxelType = BlockType.Grass;

                    }

                    //  if none of these "if" are met, use voxel type dirt of Chunk on Line 62
                    Chunk.SetBlock(data, new Vector3Int(x, y, z), voxelType);

                }

            }

        }

    }

    //
    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {

        // get the chunk position from the block coordinates
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);

        // access chunk data of this chunk and set it to null
        ChunkData containerChunk = null;

        // chunkDataDictionary try to get the value, if it finds the value it will output it into the containerChunk
        chunkDataDictionary.TryGetValue(pos, out containerChunk);

        // if the containerChunk == null
        if (containerChunk == null)

            // return the BlockType Nothing
            return BlockType.Nothing;

        // get the position of the block from the chunk that was found, pass in containerChunk which is where the block is found in
        // and pass in a new vector 3 using x,y,z
        Vector3Int blockInChunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));

        // return the chunk using getblockfromchunkcoordinates, get correct block type from chunk and the coordinates
        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInChunkCoordinates);

    }
}

/*

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int mapSizeInChunks = 6;
    public int chunkSize = 16, chunkHeight = 100;
    public int waterThreshold = 50;
    public float noiseScale = 0.03f;
    public GameObject chunkPrefab;

    Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();
    Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

    public void GenerateWorld() {
        chunkDataDictionary.Clear();
        foreach (ChunkRenderer chunk in chunkDictionary.Values) {
            Destroy(chunk.gameObject);
        }
        chunkDictionary.Clear();

        for (int x = 0; x < mapSizeInChunks; x++) {
            for (int z = 0; z < mapSizeInChunks; z++) {

                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, z * chunkSize));
                GenerateVoxels(data);
                chunkDataDictionary.Add(data.worldPosition, data);
            }
        }

        foreach (ChunkData data in chunkDataDictionary.Values) {
            MeshData meshData = Chunk.GetChunkMeshData(data);
            GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
            chunkDictionary.Add(data.worldPosition, chunkRenderer);
            chunkRenderer.InitializeChunk(data);
            chunkRenderer.UpdateChunk(meshData);

        }
    }

    private void GenerateVoxels(ChunkData data) {
        for (int x = 0; x < data.chunkSize; x++) {
            for (int z = 0; z < data.chunkSize; z++) {
                float noiseValue = Mathf.PerlinNoise((data.worldPosition.x + x) * noiseScale, (data.worldPosition.z + z) * noiseScale);
                int groundPosition = Mathf.RoundToInt(noiseValue * chunkHeight);
                for (int y = 0; y < chunkHeight; y++) {
                    BlockType voxelType = BlockType.Dirt;
                    if (y > groundPosition) {
                        if (y < waterThreshold) {
                            voxelType = BlockType.Water;
                        } else {
                            voxelType = BlockType.Air;
                        }

                    } else if (y == groundPosition) {
                        voxelType = BlockType.Dirt;
                    }

                    Chunk.SetBlock(data, new Vector3Int(x, y, z), voxelType);
                }
            }
        }
    }

    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);
        ChunkData containerChunk = null;

        chunkDataDictionary.TryGetValue(pos, out containerChunk);

        if (containerChunk == null)
            return BlockType.Nothing;
        Vector3Int blockInCHunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));
        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInCHunkCoordinates);
    }
}

*/