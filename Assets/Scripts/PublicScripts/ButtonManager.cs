////////////////////////////////////////////////////////////
// File: ButtonManager.cs
// Author: Jack Peedle
// Date Created: 26/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 05/12/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    // reference to the terrain generator
    public TerrainGenerator terrainGenerator;

    // gameobject for the menu canvas
    public GameObject MenuCanvas;

    // gameobject for the game canvas
    public GameObject GameCanvas;

    // gameobject for the biome panel
    public GameObject SelectBiomePanel;

    // gameobject for the generate biome panel
    public GameObject GenerateBiomePanel;

    // gameobject for the generate biome button
    public GameObject GenerateBiomeButton;

    // gameobject for the game load scene button
    public GameObject GameBackButton;

    //
    public GameObject[] biomeParticleSystems;

    // on start
    private void Start() {

        // set the menu canvas to true
        MenuCanvas.SetActive(true);

        // set the game canvas to false
        GameCanvas.SetActive(false);

        // set the generate biome panel to false
        GenerateBiomePanel.SetActive(false);

        // set the select biome panel to true
        SelectBiomePanel.SetActive(true);

        // set the generate biome button to false
        GenerateBiomeButton.SetActive(false);

        // set the game back button to true
        GameBackButton.SetActive(true);

        
    }





    // Button to generate the normal biome in the TerrainGenerator
    public void GenerateNormalBiome() {

        // set the biome generator in the terrain generator to the normal biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_NormalBiome.GetComponent<BiomeGenerator>();

        // call change to normal biome method
        terrainGenerator.ChangeToNormalBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(true);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);


    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateSandBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_SandBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToSandBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(true);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateIceBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_IceBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToIceBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(true);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateLavaBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_LavaBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToLavaBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(true);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateJungleBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_JungleBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToJungleBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(true);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateCursedBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_CursedBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToCursedBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(true);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(false);

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateMushroomBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_MushroomBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToMushroomBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(true);

        //
        biomeParticleSystems[7].SetActive(true);

        //
        biomeParticleSystems[8].SetActive(false);

    }


    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateFarmBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_FarmBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToFarmBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);



        //
        biomeParticleSystems[0].SetActive(false);

        //
        biomeParticleSystems[1].SetActive(false);

        //
        biomeParticleSystems[2].SetActive(false);

        //
        biomeParticleSystems[3].SetActive(false);

        //
        biomeParticleSystems[4].SetActive(false);

        //
        biomeParticleSystems[5].SetActive(false);

        //
        biomeParticleSystems[6].SetActive(false);

        //
        biomeParticleSystems[7].SetActive(false);

        //
        biomeParticleSystems[8].SetActive(true);

    }





    // change to game canvas method
    public void ChangeToGameCanvas() {

        // set the menu canvas to false
        MenuCanvas.SetActive(false);

        // set the game canvas to true
        GameCanvas.SetActive(true);

    }

    // Change to game canvas method
    public void ChangeToMenuCanvas() {

        // load the first scene (Wipe Data)
        SceneManager.LoadScene(0);

    }

}
