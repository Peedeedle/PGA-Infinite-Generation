////////////////////////////////////////////////////////////
// File: ButtonManager.cs
// Author: Jack Peedle
// Date Created: 26/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Manager for all of the generate biome buttons
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    // reference to the audio manager
    public AudioManager audioManager;

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

    // show controls button
    public GameObject ShowControlsButton;

    // hide controls button
    public GameObject HideControlsButton;

    // control panel button
    public GameObject ControlsPanel;

    // array of gameobjects which are the particle systems
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

        // set the show controls to false
        ShowControlsButton.SetActive(false);

        // set the hide controls to true
        HideControlsButton.SetActive(true);

        // set the controls panel to true
        ControlsPanel.SetActive(true);

    }


    // exit the game
    public void ExitGame() {

        // quit the application
        Application.Quit();

    }

    // hide controls panel
    public void HideControls() {

        // set the controls panel to false
        ControlsPanel.SetActive(false);

        // hide controls button to false
        HideControlsButton.SetActive(false);

        // show controls button to true
        ShowControlsButton.SetActive(true);

    }

    // show controls panel
    public void ShowControls() {

        // set the controls panel to true
        ControlsPanel.SetActive(true);

        // hide controls button to true
        HideControlsButton.SetActive(true);

        // show controls button to false
        ShowControlsButton.SetActive(false);

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(true);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the normal sound
        audioManager.PlayNormalSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(true);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the desert sound
        audioManager.PlayDesertSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(true);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the ice sound
        audioManager.PlayIceSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(true);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the lava sound
        audioManager.PlayLavaSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(true);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the jungle sound
        audioManager.PlayJungleSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(true);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the cursed sound
        audioManager.PlayCursedSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(true);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(true);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the mushroom sound
        audioManager.PlayMushroomSound();

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

        // set the particle system (1) to true
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(true);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(false);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(false);

        // play the farm sound
        audioManager.PlayFarmSound();

    }

    // Button to generate the sand biome in the TerrainGenerator
    public void GenerateCandyBiome() {

        // set the biome generator in the terrain generator to the sand biomes biome generator component
        terrainGenerator.biomeGenerator = terrainGenerator.Go_CandyBiome.GetComponent<BiomeGenerator>();

        // call change to sand biome method
        terrainGenerator.ChangeToCandyBiome();

        // set the select biome panel to false
        SelectBiomePanel.SetActive(false);

        // set the generate biome panel to true
        GenerateBiomePanel.SetActive(true);

        // set the generate biome button to true
        GenerateBiomeButton.SetActive(true);

        // set the particle system (1) to false
        biomeParticleSystems[0].SetActive(false);

        // set the particle system (2) to false
        biomeParticleSystems[1].SetActive(false);

        // set the particle system (3) to false
        biomeParticleSystems[2].SetActive(false);

        // set the particle system (4) to false
        biomeParticleSystems[3].SetActive(false);

        // set the particle system (5) to false
        biomeParticleSystems[4].SetActive(false);

        // set the particle system (6) to false
        biomeParticleSystems[5].SetActive(false);

        // set the particle system (7) to false
        biomeParticleSystems[6].SetActive(false);

        // set the particle system (8) to false
        biomeParticleSystems[7].SetActive(false);

        // set the particle system (9) to false
        biomeParticleSystems[8].SetActive(false);

        // set the particle system (10) to false
        biomeParticleSystems[9].SetActive(true);

        // set the particle system (11) to false
        biomeParticleSystems[10].SetActive(true);

        // play the candy sound
        audioManager.PlayCandySound();

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

        // set the audio to none
        audioManager.SetAudioToNone();

    }

}
