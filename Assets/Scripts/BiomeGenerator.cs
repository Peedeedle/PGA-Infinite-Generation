////////////////////////////////////////////////////////////
// File: BiomeGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 11/11/21
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



    //
    internal TreeData GetTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if tree generator = null (Don't generate trees)
        if (treeGenerator == null) 

            // return tree data values which are 0
            return new TreeData();

        // return generate tree with data and map seed offset for positions
        return treeGenerator.GenerateTreeData(data, mapSeedOffset);

        

    }


    // list of the blocklayerhandlers called additionLayerHandlers
    public List<BlockLayerHandler> additionalLayerHandlers;


    // Generate the chunk data using the data and a int for the x, z and a vector 2 for the mapSeedOffset
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset, int? terrainHeightNoise) {

        // noise settings world offset = mapSeedOffset
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        // int for ground position where dirt and grass will be generated, above = air, below = water etc
        int groundPosition; //= GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);

        //
        if(terrainHeightNoise.HasValue == false) {

            //
            groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);

        }
        else {

            //
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

/*

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    public int waterThreshold = 50;

    public NoiseSettings biomeNoiseSettings;

    public DomainWarping domainWarping;

    public bool useDomainWarping = true;

    public BlockLayerHandler startLayerHandler;

    public TreeGenerator treeGenerator;

    internal TreeData GetTreeData(ChunkData data, Vector2Int mapSeedOffset) {
        if (treeGenerator == null)
            return new TreeData();
        return treeGenerator.GenerateTreeData(data, mapSeedOffset);
    }

    public List<BlockLayerHandler> additionalLayerHandlers;

    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset, int? terrainHeightNoise) {
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        int groundPosition;
        if (terrainHeightNoise.HasValue == false)
            groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);
        else
            groundPosition = terrainHeightNoise.Value;

        for (int y = data.worldPosition.y; y < data.worldPosition.y + data.chunkHeight; y++) {
            startLayerHandler.Handle(data, x, y, z, groundPosition, mapSeedOffset);
        }

        foreach (var layer in additionalLayerHandlers) {
            layer.Handle(data, x, data.worldPosition.y, z, groundPosition, mapSeedOffset);
        }
        return data;
    }

    public int GetSurfaceHeightNoise(int x, int z, int chunkHeight) {
        float terrainHeight;
        if (useDomainWarping == false) {
            terrainHeight = MyNoise.OctavePerlin(x, z, biomeNoiseSettings);
        } else {
            terrainHeight = domainWarping.GenerateDomainNoise(x, z, biomeNoiseSettings);
        }

        terrainHeight = MyNoise.Redistribution(terrainHeight, biomeNoiseSettings);
        int surfaceHeight = MyNoise.RemapValue01ToInt(terrainHeight, 0, chunkHeight);
        return surfaceHeight;
    }
}

*/
