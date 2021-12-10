////////////////////////////////////////////////////////////
// File: MelonGenerator.cs
// Author: Jack Peedle
// Date Created: 10/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 10/12/21
// Brief: Generating the trees
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelonGenerator : MonoBehaviour
{
    
    // tree noise settings
    public NoiseSettings melonNoiseSettings;

    // domain warping
    public DomainWarping domainWarping;

    // Generate the tree data passing through the chunk data and a vector2int for the map offset
    public MelonData GenerateMelonData(ChunkData chunkData, Vector2Int mapSeedOffset) {

        // tree noise settings world offset = mapSeedOffset
        melonNoiseSettings.worldOffset = mapSeedOffset;

        // treeData = new TreeData
        MelonData melonData = new MelonData();

        // float array noiseData = generateTreeNoise method passing in (chunkData, treeNoiseSettings)
        float[,] noiseData = GenerateMelonNoise(chunkData, melonNoiseSettings);

        // tree positions = Data Proccessing method.findlocalMaxima passing in noise data and the chunks x and z world positions
        melonData.melonPositions = MelonDataProccessing.FindLocalMaxima(noiseData, chunkData.worldPosition.x, chunkData.worldPosition.z);

        // return the treeData
        return melonData;

    }

    // private float array for GenerateTreeNoise (for each position in chunk) passing in (chunkData, treeNoiseSettings)
    private float[,] GenerateMelonNoise(ChunkData chunkData, NoiseSettings melonNoiseSettings) {

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

                // using the x and z Generate Domain Noise (x, z, and treeNoiseSettings)
                noiseMax[xIndex, zIndex] = domainWarping.GenerateDomainNoise(x, z, melonNoiseSettings);

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
