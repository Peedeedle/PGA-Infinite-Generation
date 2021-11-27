////////////////////////////////////////////////////////////
// File: ButtonManager.cs
// Author: Jack Peedle
// Date Created: 26/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 27/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    // reference to the terrain generator
    public TerrainGenerator terrainGenerator;

    
    // Button to generate the normal biome in the TerrainGenerator
    public void GenerateNormalBiome() {

        terrainGenerator.biomeGenerator = terrainGenerator.Go_NormalBiome.GetComponent<BiomeGenerator>();

        terrainGenerator.ChangeToNormalBiome();

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateSandBiome() {

        terrainGenerator.biomeGenerator = terrainGenerator.Go_SandBiome.GetComponent<BiomeGenerator>();

        terrainGenerator.ChangeToSandBiome();

    }


}
