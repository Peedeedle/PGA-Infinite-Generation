////////////////////////////////////////////////////////////
// File: BiomeGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 30/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{

    // int for the water threshold (water surface at specific height)
    public int waterThreshold = 50;

    // int for the noise scale of the chunk / mesh (Generate noise (Perlin Noise))
    public float noiseScale = 0.03f;

    // Generate the chunk data using the data and a int for the x, z and a vector 2 for the mapSeedOffset
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset) { 

        // generate noise value to set level of the ground using world position (x and z values) then * noiseScale
        float noiseValue = Mathf.PerlinNoise((mapSeedOffset.x + data.worldPosition.x + x) * noiseScale,
            (mapSeedOffset.y + data.worldPosition.z + z) * noiseScale);

        // int for ground position where dirt and grass will be generated, above = air, below = water etc
        int groundPosition = Mathf.RoundToInt(noiseValue * data.chunkHeight);

        // look for each z local coordinate from 0 - chunkHeight
        // for chunks above the ground
        for (int y = 0; y < data.chunkHeight; y++) {

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

                // else if y is = to the ground position and y is less than the water threshold
            } else if (y == groundPosition && y < waterThreshold) {

                // block type = sand
                voxelType = BlockType.Sand;

            } else if (y == groundPosition) {

                // block type = grass dirt
                voxelType = BlockType.Grass_Dirt;

            }

            //  if none of these "if" are met, use voxel type dirt of Chunk on Line 62
            Chunk.SetBlock(data, new Vector3Int(x, y, z), voxelType);

        }

        // Return the data
        return data;

    }
}
