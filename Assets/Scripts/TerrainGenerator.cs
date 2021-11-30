////////////////////////////////////////////////////////////
// File: TerrainGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 29/11/21
// Brief: Generating the terrain using noise settings and data
//////////////////////////////////////////////////////////// 


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    //
    #region Water Materials
    [Header("WaterMaterials")]

    //
    public Texture normalWater, sandWater, iceWater, lavaWater;

    //
    public Material Water;

    #endregion

    #region Biome Generator GameObjects

    [Header("Biome References")]

    // reference to the normal biome game object
    public GameObject Go_NormalBiome;

    // reference to the sand biome game object
    public GameObject Go_SandBiome;

    // reference to the sand biome game object
    public GameObject Go_IceBiome;

    // reference to the sand biome game object
    public GameObject Go_LavaBiome;

    #endregion

    #region Biome Data Lists

    // list of biome data called biomeGeneratorsData
    [SerializeField]
    private List<BiomeData> biomeGeneratorsData = new List<BiomeData>();

    // list of normal biome data called normalBiomeData
    [SerializeField]
    [Header("NORMALDATA")]
    private List<BiomeData> normalBiomeData = new List<BiomeData>();

    // list of sand biome data called sandBiomeData
    [SerializeField]
    [Header("SANDDATA")]
    private List<BiomeData> sandBiomeData = new List<BiomeData>();

    // list of sand biome data called sandBiomeData
    [SerializeField]
    [Header("IceDATA")]
    private List<BiomeData> iceBiomeData = new List<BiomeData>();

    // list of sand biome data called sandBiomeData
    [SerializeField]
    [Header("IceDATA")]
    private List<BiomeData> lavaBiomeData = new List<BiomeData>();

    #endregion

    // reference to a biome data
    public BiomeData biomeData;

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

    // on start
    public void Start() {

        // reference to the water materials normal map
        Water.EnableKeyword("_NORMALMAP");

        // reference to the water materials Albedo
        Water.EnableKeyword("_ALBEDO");

        // Set the Albedo for the material to the normal water
        Water.SetTexture("_MainTex", normalWater);

        // Set the Normal Map of the material to normal water
        Water.SetTexture("_BumpMap", normalWater);

    }

    #region Change To Biomes

    // when button pressed in ButtonManager change to normal biome
    public void ChangeToNormalBiome() {

        // set the list of biome data to the normal biome data
        biomeGeneratorsData = normalBiomeData;

        // Set the Albedo for the material to the normal water
        Water.SetTexture("_MainTex", normalWater);

        // Set the Normal Map of the material to normal water
        Water.SetTexture("_BumpMap", normalWater);

    }

    // when button pressed in ButtonManager change to sand biome
    public void ChangeToSandBiome() {

        // set the list of biome data to the sand biome data
        biomeGeneratorsData = sandBiomeData;

        // Set the Albedo for the material to the sand water
        Water.SetTexture("_MainTex", sandWater);

        // Set the Normal Map of the material to sand water
        Water.SetTexture("_BumpMap", sandWater);

    }

    // when button pressed in ButtonManager change to sand biome
    public void ChangeToIceBiome() {

        // set the list of biome data to the sand biome data
        biomeGeneratorsData = iceBiomeData;

        // Set the Albedo for the material to the ice water
        Water.SetTexture("_MainTex", iceWater);

        // Set the Normal Map of the material to ice water
        Water.SetTexture("_BumpMap", iceWater);
    }

    // when button pressed in ButtonManager change to sand biome
    public void ChangeToLavaBiome() {

        // set the list of biome data to the sand biome data
        biomeGeneratorsData = lavaBiomeData;

        // Set the Albedo for the material to the ice water
        Water.SetTexture("_MainTex", lavaWater);

        // Set the Normal Map of the material to ice water
        Water.SetTexture("_BumpMap", lavaWater);
    }

    #endregion




    // Generate the chunk data using the data and a vector 2 for the mapSeedOffset
    public ChunkData GenerateChunkData(ChunkData data, Vector2Int mapSeedOffset) {

        // biome selection = select biome generator passing in the world position, data and false bool
        BiomeGeneratorSelection biomeSelection = SelectBiomeGenerator(data.worldPosition, data, false);



        // include data from the tree data
        // (Include data before world is rendered)
        data.treeData = biomeSelection.biomeGenerator.GetTreeData(data, mapSeedOffset);

        // include data from the cactus data
        // (Include data before world is rendered)
        data.cactusData = biomeSelection.biomeGenerator.GetCactusData(data, mapSeedOffset);

        // include data from the cactus data
        // (Include data before world is rendered)
        data.snowTreeData = biomeSelection.biomeGenerator.GetSnowTreeData(data, mapSeedOffset);

        // include data from the cactus data
        // (Include data before world is rendered)
        data.presentsData = biomeSelection.biomeGenerator.GetPresentsData(data, mapSeedOffset);


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

    // select biome using index
    public BiomeGenerator SelectBiome(int index) {

        // float for temperature = biome noise array of index
        float temp = biomeNoise[index];

        // for each data in biome generator data
        foreach (var data in biomeGeneratorsData) {

            // if temperature is more than or equal to the temperature start threshold and is less than the end threshold
            if (temp >= data.temperatureStartThreshold && temp < data.temperatureEndThreshold)


                // return the data for the biome terrain generator
                return data.biomeTerrainGenerator;

        }

        // return the biome generators data with an index of [0] to the biome terrain generator
        return biomeGeneratorsData[0].biomeTerrainGenerator;

    }


    // list of biome selection helpers
    private List<BiomeSelectionHelper> GetBiomeGeneratorSelectionHelpers(Vector3Int position) {

        // set the y position to 0
        position.y = 0;

        // return the closest biome index position
        return GetClosestBiomeIndex(position);

    }

    // list of biome selection helpers to get the closest biome index
    private List<BiomeSelectionHelper> GetClosestBiomeIndex(Vector3Int position) {

        // return a new biome center passing in the center and index = new biome selection helper
        return biomeCenters.Select((center, index) => new BiomeSelectionHelper {

            // set Index to index
            Index = index,

            // distance = distance passing in the center and position
            Distance = Vector3.Distance(center, position)

            // order by the distance between helpers and take in 4 ints to a list
        }).OrderBy(helper => helper.Distance).Take(4).ToList();

    }

    // struct biome selection helper
    private struct BiomeSelectionHelper
    {
        // int for index
        public int Index;

        // float for distance
        public float Distance;

    }

    // generate the biome points passing in the starting position, draw range, map size and mapSeedOffset
    public void GenerateBiomePoints(Vector3 startingPosition, int drawRange, int mapSize, Vector2Int mapSeedOffset) {

        // biome centers = a new list of vector 3 ints
        biomeCenters = new List<Vector3Int>();

        // biome centers = calculate biome centers passing in the starting position, draw range and map size
        biomeCenters = BiomeCenterFinder.CalculateBiomeCenters(startingPosition, drawRange, mapSize);

        // for i = 0, i less than biome centers count, i++
        for (int i = 0; i < biomeCenters.Count; i++) {

            // vector 2 int for domain warping offset, generate domain offset passing in the biome centers[i].x and .y
            Vector2Int domainWarpingOffset = biomeDomainWarping.GenerateDomainOffsetInt(biomeCenters[i].x, biomeCenters[i].y);

            // biome centers with the array of i + and = new vector 3(domain warping.x (X), 0 (Y), domain warping.y (Z)
            biomeCenters[i] += new Vector3Int(domainWarpingOffset.x, 0, domainWarpingOffset.y);


        }

        // biome noise = biome noise passing in the biome centers and mapSeedOffset
        biomeNoise = CalculateBiomeNoise(biomeCenters, mapSeedOffset);

    }

    // List of floats called calculateBiomeNoise passing in list of vector3ints called biome centers and vector2int for mapSeedOffset
    private List<float> CalculateBiomeNoise(List<Vector3Int> biomeCenters, Vector2Int mapSeedOffset) {

        // biomeNoiseSettings.worldOffset = mapSeedOffset
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        // return the biome centers list of center => my noise (Perlin Noise) (center.x (X), center.y (Y), biomeNoiseSettings (Z)
        return biomeCenters.Select(center => MyNoise.OctavePerlin(center.x, center.y, biomeNoiseSettings)).ToList();

    }

    // Debug for seeing the center points using gizmos at runtime
    private void OnDrawGizmos() {

        // gizmo colour blue
        Gizmos.color = Color.blue;

        // for each biome center point in biome centers
        foreach (var biomeCenterPoint in biomeCenters) {

            // draw a line from the biome center point up by 255
            Gizmos.DrawLine(biomeCenterPoint, biomeCenterPoint + Vector3.up * 255);

        }

    }


}

// struct for the biome data
[Serializable]
public class BiomeData
{

    // temperature start and end threshold 0-1
    [Range(0f, 1f)]
    public float temperatureStartThreshold, temperatureEndThreshold;

    // reference to the biome terrain generator
    [SerializeField]
    public BiomeGenerator biomeTerrainGenerator;


}

// biome generator selection
public class BiomeGeneratorSelection
{

    // set the biome generator to null
    public BiomeGenerator biomeGenerator = null;

    // terrain surface noise int = null
    public int? terrainSurfaceNoise = null;


    // biome generator selection passes in the biome generator and terrain surface noise
    public BiomeGeneratorSelection(BiomeGenerator biomeGenerator, int? terrainSurfaceNoise = null) {

        // this biome generator = biome generator
        this.biomeGenerator = biomeGenerator;

        // this terrain surface noise = terrain surface noise
        this.terrainSurfaceNoise = terrainSurfaceNoise;

    }


}
