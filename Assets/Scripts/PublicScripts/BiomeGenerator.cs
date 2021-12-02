////////////////////////////////////////////////////////////
// File: BiomeGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Generate the biomes
//////////////////////////////////////////////////////////// 


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{

    // int for the water threshold (water surface at specific height)
    public int waterThreshold = 50;

    // reference to the noise settings called biomeNoiseSettings
    public NoiseSettings biomeNoiseSettings;

    // rreference to the domain warping script 
    public DomainWarping domainWarping;

    // bool for using the domain warping or not
    public bool useDomainWarping = true;

    // blockLayerHandler called startLayerHandler
    public BlockLayerHandler startLayerHandler;

    // tree generator
    public TreeGenerator treeGenerator;

    // cactus generator
    public CactusGenerator cactusGenerator;

    //
    public SnowTreeGenerator snowTreeGenerator;

    //
    public PresentsGenerator presentsGenerator;

    //
    public JungleTreeGenerator jungleTreeGenerator;

    //
    public LilyPadGenerator lilyPadGenerator;

    //
    public SugarCaneGenerator sugarCaneGenerator;

    // Get the tree data
    internal TreeData GetTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if tree generator = null (Don't generate trees)
        if (treeGenerator == null) 

            // return tree data values which are 0
            return new TreeData();

        // return generate tree with data and map seed offset for positions
        return treeGenerator.GenerateTreeData(data, mapSeedOffset);

    }

    // Get the cactus data
    internal CactusData GetCactusData(ChunkData data, Vector2Int mapSeedOffset) {

        // if cactus generator = null (Don't generate cactuses)
        if (cactusGenerator == null)

            // return cactus data values which are 0
            return new CactusData();

        // return generate cactus with data and map seed offset for positions
        return cactusGenerator.GenerateCactusData(data, mapSeedOffset);

    }

    // 
    internal SnowTreeData GetSnowTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // 
        if (snowTreeGenerator == null)

            // 
            return new SnowTreeData();

        // 
        return snowTreeGenerator.GenerateSnowTreeData(data, mapSeedOffset);

    }

    // 
    internal PresentsData GetPresentsData(ChunkData data, Vector2Int mapSeedOffset) {

        // 
        if (presentsGenerator == null)

            // 
            return new PresentsData();

        // 
        return presentsGenerator.GeneratePresentsData(data, mapSeedOffset);

    }

    // 
    internal JungleTreeData GetJungleTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // 
        if (jungleTreeGenerator == null)

            // 
            return new JungleTreeData();

        // 
        return jungleTreeGenerator.GenerateJungleTreeData(data, mapSeedOffset);

    }

    // 
    internal LilyPadData GetLilyPadData(ChunkData data, Vector2Int mapSeedOffset) {

        // 
        if (lilyPadGenerator == null)

            // 
            return new LilyPadData();

        // 
        return lilyPadGenerator.GenerateLilyPadData(data, mapSeedOffset);

    }

    // 
    internal SugarCaneData GetSugarCaneData(ChunkData data, Vector2Int mapSeedOffset) {

        // 
        if (sugarCaneGenerator == null)

            // 
            return new SugarCaneData();

        // 
        return sugarCaneGenerator.GenerateSugarCaneData(data, mapSeedOffset);

    }

    // list of the blocklayerhandlers called additionLayerHandlers
    public List<BlockLayerHandler> additionalLayerHandlers;

    // Generate the chunk data using the data and a int for the x, z and a vector 2 for the mapSeedOffset
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset, int? terrainHeightNoise) {

        // noise settings world offset = mapSeedOffset
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        // int for ground position where dirt and grass will be generated, above = air, below = water etc
        int groundPosition;

        // if terrain height noise has value = false
        if(terrainHeightNoise.HasValue == false) {

            // ground position = surface height noise (X, Y and Z)
            groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);

        }
        else {

            // ground position = terrain height noise value
            groundPosition = terrainHeightNoise.Value;

        }

        // look for each z local coordinate from 0 - chunkHeight
        // for chunks above the ground
        for (int y = data.worldPosition.y; y < data.worldPosition.y + data.chunkHeight; y++){ //0; y < data.chunkHeight; y++) {

            //start handling the layers with the inputs
            startLayerHandler.Handle(data, x, y, z, groundPosition, mapSeedOffset);

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
    public int GetSurfaceHeightNoise(int x, int z, int chunkHeight) {

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
