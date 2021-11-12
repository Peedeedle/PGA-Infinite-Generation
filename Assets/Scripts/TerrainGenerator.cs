////////////////////////////////////////////////////////////
// File: TerrainGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Generating the terrain using noise settings and data
//////////////////////////////////////////////////////////// 


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    // Reference to the BiomeGenerator
    public BiomeGenerator biomeGenerator;

    //list of vector 3 int biome centers
    [SerializeField]
    List<Vector3Int> biomeCenters = new List<Vector3Int>();

    // list of floats for the biome noise
    List<float> biomeNoise = new List<float>();

    // reference to the noise settings
    [SerializeField]
    private NoiseSettings biomeNoiseSettings;

    // reference to the domain warping
    public DomainWarping biomeDomainWarping;

    // list of biome data called biomeGeneratorsData
    [SerializeField]
    private List<BiomeData> biomeGeneratorsData = new List<BiomeData>();

    // Generate the chunk data using the data and a vector 2 for the mapSeedOffset
    public ChunkData GenerateChunkData(ChunkData data, Vector2Int mapSeedOffset) {

        // biome selection = select biome generator passing in the world position, data and false bool
        BiomeGeneratorSelection biomeSelection = SelectBiomeGenerator(data.worldPosition, data, false);

        // include data from the tree data
        // (Include data before world is rendered)
        data.treeData = biomeSelection.biomeGenerator.GetTreeData(data, mapSeedOffset);

        // look for each x local coordinate from 0 - chunksize (loop)
        for (int x = 0; x < data.chunkSize; x++) {

            // look for each z local coordinate from 0 - chunksize (loop)
            for (int z = 0; z < data.chunkSize; z++) {

                // biome selection = select biome generator (new vector 3 of X world position + X, Z world position + Z and data
                biomeSelection = SelectBiomeGenerator(new Vector3Int(data.worldPosition.x + x, 0, data.worldPosition.z + z), data);

                // data = ProcessChunkColumn method passing in the data, x, z and the mapSeedOffset
                data = biomeSelection.biomeGenerator.ProcessChunkColumn(data, x, z, mapSeedOffset, biomeSelection.terrainSurfaceNoise);

            }

        }

        // Return the data
        return data;

    }

    // select biome generator passing in world position, data and bool for whether to use domain warping
    private BiomeGeneratorSelection SelectBiomeGenerator(Vector3Int worldPosition, ChunkData data, bool useDomainWarping = true) {

        // if useDomainWarping is true
        if (useDomainWarping == true) {

            // domain offset = generate domain offset to int passing in the X and Z world position
            Vector2Int domainOffset = Vector2Int.RoundToInt(biomeDomainWarping.GenerateDomainOffset(worldPosition.x, worldPosition.z));

            // world position += vector 3 int 
            worldPosition += new Vector3Int(domainOffset.x, 0, domainOffset.y);

        }

        //list of biome selection helpers called biome selection helpers passing in the world position
        List<BiomeSelectionHelper> biomeSelectionHelpers = GetBiomeGeneratorSelectionHelpers(worldPosition);

        // biome generator 1 = biome generator with an index of 0 (Assign in the inspector)
        BiomeGenerator generator_1 = SelectBiome(biomeSelectionHelpers[0].Index);

        // biome generator 2 = biome generator with an index of 1 (Assign in the inspector)
        BiomeGenerator generator_2 = SelectBiome(biomeSelectionHelpers[1].Index);

        // distance = vector 3 distance passing in the biome generators
        float distance = Vector3.Distance(biomeCenters[biomeSelectionHelpers[0].Index], biomeCenters[biomeSelectionHelpers[1].Index]);

        // float for the weight 0 = biome [0] distance / distance
        float weight_0 = biomeSelectionHelpers[0].Distance / distance;

        // float weight = 1 - weight 0
        float weight_1 = 1 - weight_0;

        // terrain height noise for the first biome 
        int terrainHeightNoise_0 = generator_1.GetSurfaceHeightNoise(worldPosition.x, worldPosition.z, data.chunkHeight);

        // terrain height noise for the second biome 
        int terrainHeightNoise_1 = generator_2.GetSurfaceHeightNoise(worldPosition.x, worldPosition.z, data.chunkHeight);

        // return new biome selection of the generator 1, with the height and weight of the other biomes
        return new BiomeGeneratorSelection(generator_1, Mathf.RoundToInt(terrainHeightNoise_0 * weight_0 + terrainHeightNoise_1 * weight_1));

    }

    // select biome
    private BiomeGenerator SelectBiome(int index) {

        //
        float temp = biomeNoise[index];

        //
        foreach (var data in biomeGeneratorsData) {

            //
            if (temp >= data.temperatureStartThreshold && temp < data.temperatureEndThreshold)

                //
                return data.biomeTerrainGenerator;

        }

        //
        return biomeGeneratorsData[0].biomeTerrainGenerator;

    }


    //
    private List<BiomeSelectionHelper> GetBiomeGeneratorSelectionHelpers(Vector3Int position) {

        //
        position.y = 0;

        //
        return GetClosestBiomeIndex(position);

    }

    //
    private List<BiomeSelectionHelper> GetClosestBiomeIndex(Vector3Int position) {

        //
        return biomeCenters.Select((center, index) => new BiomeSelectionHelper {

            //
            Index = index,

            //
            Distance = Vector3.Distance(center, position)

        }).OrderBy(helper => helper.Distance).Take(4).ToList();

    }

    //
    private struct BiomeSelectionHelper
    {
        //
        public int Index;

        //
        public float Distance;

    }

    //
    public void GenerateBiomePoints(Vector3 startingPosition, int drawRange, int mapSize, Vector2Int mapSeedOffset) {

        //
        biomeCenters = new List<Vector3Int>();

        //
        biomeCenters = BiomeCenterFinder.CalculateBiomeCenters(startingPosition, drawRange, mapSize);

        for (int i = 0; i < biomeCenters.Count; i++) {

            //
            Vector2Int domainWarpingOffset = biomeDomainWarping.GenerateDomainOffsetInt(biomeCenters[i].x, biomeCenters[i].y);

            //
            biomeCenters[i] += new Vector3Int(domainWarpingOffset.x, 0, domainWarpingOffset.y);


        }

        //
        biomeNoise = CalculateBiomeNoise(biomeCenters, mapSeedOffset);

    }

    //
    private List<float> CalculateBiomeNoise(List<Vector3Int> biomeCenters, Vector2Int mapSeedOffset) {

        //
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        //
        return biomeCenters.Select(center => MyNoise.OctavePerlin(center.x, center.y, biomeNoiseSettings)).ToList();

    }

    // Debug for seeing the center points using gizmos at runtime
    private void OnDrawGizmos() {

        //
        Gizmos.color = Color.blue;

        //
        foreach (var biomeCenterPoint in biomeCenters) {

            //
            Gizmos.DrawLine(biomeCenterPoint, biomeCenterPoint + Vector3.up * 255);

        }

    }

}

//
[Serializable]
public struct BiomeData
{

    //
    [Range(0f, 1f)]
    public float temperatureStartThreshold, temperatureEndThreshold;

    //
    public BiomeGenerator biomeTerrainGenerator;


}

//
public class BiomeGeneratorSelection
{

    //
    public BiomeGenerator biomeGenerator = null;

    //
    public int? terrainSurfaceNoise = null;


    //
    public BiomeGeneratorSelection(BiomeGenerator biomeGenerator, int? terrainSurfaceNoise = null) {

        //
        this.biomeGenerator = biomeGenerator;

        //
        this.terrainSurfaceNoise = terrainSurfaceNoise;

    }


}
