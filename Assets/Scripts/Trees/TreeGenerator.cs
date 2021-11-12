////////////////////////////////////////////////////////////
// File: TreeGenerator.cs
// Author: Jack Peedle
// Date Created: 06/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Generating the trees
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    // tree noise settings
    public NoiseSettings treeNoiseSettings;

    // domain warping
    public DomainWarping domainWarping;

    // Generate the tree data passing through the chunk data and a vector2int for the map offset
    public TreeData GenerateTreeData(ChunkData chunkData, Vector2Int mapSeedOffset) {

        // tree noise settings world offset = mapSeedOffset
        treeNoiseSettings.worldOffset = mapSeedOffset;

        // treeData = new TreeData
        TreeData treeData = new TreeData();

        // float array noiseData = generateTreeNoise method passing in (chunkData, treeNoiseSettings)
        float[,] noiseData = GenerateTreeNoise(chunkData, treeNoiseSettings);

        // tree positions = Data Proccessing method.findlocalMaxima passing in noise data and the chunks x and z world positions
        treeData.treePositions = DataProccessing.FindLocalMaxima(noiseData, chunkData.worldPosition.x, chunkData.worldPosition.z);

        // return the treeData
        return treeData;

    }

    // private float array for GenerateTreeNoise (for each position in chunk) passing in (chunkData, treeNoiseSettings)
    private float[,] GenerateTreeNoise(ChunkData chunkData, NoiseSettings treeNoiseSettings) {

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
                noiseMax[xIndex, zIndex] = domainWarping.GenerateDomainNoise(x, z, treeNoiseSettings);

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
