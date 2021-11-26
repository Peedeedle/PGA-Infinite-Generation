////////////////////////////////////////////////////////////
// File: ButtonManager.cs
// Author: Jack Peedle
// Date Created: 26/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 26/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    //
    public TerrainGenerator terrainGenerator;

    
    //
    public void GenerateNormalBiome() {

        //
        terrainGenerator.firstValue = 0;

        terrainGenerator.secondValue = 1;


    }

    //
    public void GenerateSandBiome() {

        //
        terrainGenerator.firstValue = 2;

        terrainGenerator.secondValue = 3;

    }


}
