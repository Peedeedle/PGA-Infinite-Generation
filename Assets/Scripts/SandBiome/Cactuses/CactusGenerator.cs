////////////////////////////////////////////////////////////
// File: CactusGenerator.cs
// Author: Jack Peedle
// Date Created: 25/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Generating the cactus
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusGenerator : MonoBehaviour
{

    // tree noise settings
    public NoiseSettings cactusNoiseSettings;

    // domain warping
    public DomainWarping domainWarping;

    // Generate the cactus data passing through the chunk data and a vector2int for the map offset
    public CactusData GenerateCactusData(ChunkData chunkData, Vector2Int mapSeedOffset) {

        // cactus noise settings world offset = mapSeedOffset
        cactusNoiseSettings.worldOffset = mapSeedOffset;

        // cactusData = new cactusData
        CactusData cactusData = new CactusData();

        // float array noiseData = generatecactusNoise method passing in (chunkData, cactusNoiseSettings)
        float[,] noiseData = GenerateCactusNoise(chunkData, cactusNoiseSettings);

        // cactus positions = Data Proccessing method.findlocalMaxima passing in noise data and the chunks x and z world positions
        cactusData.cactusPositions = CactusDataProccessing.FindLocalMaxima(noiseData, chunkData.worldPosition.x, chunkData.worldPosition.z);

        // return the cactusData
        return cactusData;

    }

    // private float array for GeneratecactusNoise (for each position in chunk) passing in (chunkData, cactusNoiseSettings)
    private float[,] GenerateCactusNoise(ChunkData chunkData, NoiseSettings cactusNoiseSettings) {

        // 2D float array for the max noise
        float[,] noiseMax = new float[chunkData.chunkSize, chunkData.chunkSize];

        // Maxmium X value for the chunk size (Top of chunk)
        int xMax = chunkData.worldPosition.x + chunkData.chunkSize;

        // Minimum X value for the chunk size (Bottom of chunk)
        int xMin = chunkData.worldPosition.x;

        // Maxmium Z value for the chunk size (Right of chunk)
        int zMax = chunkData.worldPosition.z + chunkData.chunkSize;

        // Minimum Z value for the chunk size (Left of chunk)
        int zMin = chunkData.worldPosition.z;

        // x and z index = 0
        int xIndex = 0, zIndex = 0;

        // for each X
        for (int x = xMin; x < xMax; x++) {

            // for each Z
            for (int z = zMin; z < zMax; z++) {

                // using the x and z Generate Domain Noise (x, z, and cactusNoiseSettings)
                noiseMax[xIndex, zIndex] = domainWarping.GenerateDomainNoise(x, z, cactusNoiseSettings);

                // increment Z
                zIndex++;

            }

            // incrmement X
            xIndex++;

            // z Index = 0
            zIndex = 0;

        }

        // return the noise max
        return noiseMax; 

    }


}
