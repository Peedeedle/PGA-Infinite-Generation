////////////////////////////////////////////////////////////
// File: NoiseSettings.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Noise settings to be changed and altered in other scripts
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "noiseSettings", menuName = "Data/NoiseSettings")]
public class NoiseSettings : ScriptableObject
{


    // float to zoom in or out of the perlin noise function to see more or less hills in the output
    public float noiseZoom;

    // int number of Octaves
    public int octaves;

    // vector 2 int for the offset
    public Vector2Int offset;

    // vector 2 int for the world offset
    public Vector2Int worldOffset;
     
    // float for the persistence
    public float persistence;

    // float for the redistribution modifier
    public float redistibutionModifier;

    // float for the exponent
    public float exponent;


}
