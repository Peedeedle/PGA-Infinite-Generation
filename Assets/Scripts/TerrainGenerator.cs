////////////////////////////////////////////////////////////
// File: TerrainGenerator.cs
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

public class TerrainGenerator : MonoBehaviour
{

    // Reference to the BiomeGenerator
    public BiomeGenerator biomeGenerator;

    // Generate the chunk data using the data and a vector 2 for the mapSeedOffset
    public ChunkData GenerateChunkData(ChunkData data, Vector2Int mapSeedOffset) {

        // tree data = GetTreeData passing in the data and mapSeedOffset
        TreeData treeData = biomeGenerator.GetTreeData(data, mapSeedOffset);

        // include data from the tree data
        // (Include data before world is rendered)
        data.treeData = treeData;

        // look for each x local coordinate from 0 - chunksize (loop)
        for (int x = 0; x < data.chunkSize; x++) {

            // look for each z local coordinate from 0 - chunksize (loop)
            for (int z = 0; z < data.chunkSize; z++) {

                // data = ProcessChunkColumn method passing in the data, x, z and the mapSeedOffset
                data = biomeGenerator.ProcessChunkColumn(data, x, z, mapSeedOffset);

            }

        }

        // Return the data
        return data;

    }
}
