////////////////////////////////////////////////////////////
// File: RandomNoiseSettings.cs
// Author: Jack Peedle
// Date Created: 03/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Noise settings to be changed and altered in other scripts to randomize the world
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoiseSettings : MonoBehaviour
{

    // Reference to the public noise settings
    public NoiseSettings noiseSettings;

    // random int for the X value
    public int randomX;

    // random int for the Y value
    public int randomY;

    // float for the random persistence
    public float randomPersistence;


    // Start is called before the first frame update
    void Start()
    {

        // persistence = random range between 0.4-0.7
        randomPersistence = Random.Range(0.4f, 0.7f);

        // random X between -4000-0
        randomX = Random.Range(-4000, 0);

        // random Y between 0-4000
        randomY = Random.Range(0, 4000);

        //set the offset X to random X value
        noiseSettings.offset.x = randomX;

        //set the offset Y to random Y value
        noiseSettings.offset.y = randomY;

        // Set the random persistence to the persistence value
        noiseSettings.persistence = randomPersistence;

    }

}
