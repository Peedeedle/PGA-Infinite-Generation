////////////////////////////////////////////////////////////
// File: BiomeGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 02/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{

    // rreference to the domain warping script 
    public DomainWarping domainWarping;

    // bool for using the domain warping or not
    public bool useDomainWarping = true;

    // int for the water threshold (water surface at specific height)
    public int waterThreshold = 50;

    internal TreeData GetTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        //
        throw new NotImplementedException();

    }

    // reference to the noise settings called biomeNoiseSettings
    public NoiseSettings biomeNoiseSettings;

    // blockLayerHandler called startLayerHandler
    public BlockLayerHandler startLayerHandler;

    // list of the blocklayerhandlers called additionLayerHandlers
    public List<BlockLayerHandler> additionalLayerHandlers;

    // Generate the chunk data using the data and a int for the x, z and a vector 2 for the mapSeedOffset
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset) {

        // noise settings world offset = mapSeedOffset
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        // int for ground position where dirt and grass will be generated, above = air, below = water etc
        int groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);

        // look for each z local coordinate from 0 - chunkHeight
        // for chunks above the ground
        for (int y = 0; y < data.chunkHeight; y++) {

            //start handling the layers with the inputs
            startLayerHandler.Handle(data, x, y, z, groundPosition, mapSeedOffset);


            //// create block type of dirt
            //BlockType voxelType = BlockType.Dirt;

            //// if y value is more than ground position
            //if (y > groundPosition) {

            //    // if y value is less than the water threshold
            //    if (y < waterThreshold) {

            //        // block type = water
            //        voxelType = BlockType.Water;

            //    } else {

            //        // else the block type is air
            //        voxelType = BlockType.Air;

            //    }

            //    // else if y is = to the ground position and y is less than the water threshold
            //} else if (y == groundPosition && y < waterThreshold) {

            //    // block type = sand
            //    voxelType = BlockType.Sand;

            //} else if (y == groundPosition) {

            //    // block type = grass dirt
            //    voxelType = BlockType.Grass_Dirt;

            //}

            ////  if none of these "if" are met, use voxel type dirt of Chunk on Line 62
            //Chunk.SetBlock(data, new Vector3Int(x, y, z), voxelType);

        }

        // for each layer in additionalLayersHandlers
        foreach (var layer in additionalLayerHandlers) {

            // Handle the layer, pass through data, x position, data.worldPosition.y, z position, groundPosition and mapSeedOffset 
            layer.Handle(data, x, data.worldPosition.y, z, groundPosition, mapSeedOffset);

        }

        // Return the data
        return data;

    }

    // get the surface height noise using ints for x, z and the chunk height for y
    private int GetSurfaceHeightNoise(int x, int z, int chunkHeight) {

        // float terrainHeight
        float terrainHeight;

        // if use domain warping is false
        if (useDomainWarping == false) {

            // float for the terrain height = MyNoise script.OctavePerlin (Amplitude + frequency etc)
            // and x,z and biomenoise settings for y
            terrainHeight = MyNoise.OctavePerlin(x, z, biomeNoiseSettings);

        } else {

            // float for the terrain height = domain warping generateDomainNoise using the x, z, and biome noise settings
            terrainHeight = domainWarping.GenerateDomainNoise(x, z, biomeNoiseSettings);

        }

        


        // terrain height = redistribution passing in the terrain height and the noise settings
        terrainHeight = MyNoise.Redistribution(terrainHeight, biomeNoiseSettings);

        // surface height = remapped value int from 0-1 passing in the terrainHeight(x), 0(z), chunkheight(y)
        int surfaceHeight = (int)MyNoise.RemapValue01ToInt(terrainHeight, 0, chunkHeight);

        // return the surface height
        return surfaceHeight;

    }

}
