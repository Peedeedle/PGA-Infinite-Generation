////////////////////////////////////////////////////////////
// File: ButtonManager.cs
// Author: Jack Peedle
// Date Created: 26/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 29/11/21
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
