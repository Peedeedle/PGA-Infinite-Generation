////////////////////////////////////////////////////////////
// File: RandomNoiseSettings.cs
// Author: Jack Peedle
// Date Created: 03/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 03/12/21
// Brief: Noise settings to be changed and altered in other scripts
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoiseSettings : MonoBehaviour
{
    //
    public NoiseSettings noiseSettings;

    //
    public int randomX;

    //
    public int randomY;

    // Start is called before the first frame update
    void Start()
    {

        //
        randomX = Random.Range(-4000, 0);

        //
        randomY = Random.Range(0, 4000);

        //
        noiseSettings.offset.x = randomX;

        //
        noiseSettings.offset.y = randomY;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
