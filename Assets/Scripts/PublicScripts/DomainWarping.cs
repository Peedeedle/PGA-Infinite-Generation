////////////////////////////////////////////////////////////
// File: DomainWarping.cs
// Author: Jack Peedle
// Date Created: 02/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Domain warping to control the amplitude and noise values 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainWarping : MonoBehaviour
{

    // noise settings for the X, Y (technically a Z but we are using 2D vectors)
    public NoiseSettings noiseDomainX, noiseDomainY;

    // ints for the amplitude X and Y set to 20
    public int amplitudeX = 20, amplitudeY = 20;


    // generate the domain noise using the (BiomeGenerator, SurfaceHeightNoise (x,z)) and a default noise setting for biome
    public float GenerateDomainNoise(int x, int z, NoiseSettings defaultNoiseSettings) {

        // vector 2, generate domain offset with the values of X and Z
        Vector2 domainOffset = GenerateDomainOffset(x, z);

        // return octave perlin with the X value + domain offset.x, Z value + domain offset.Z and the default noise settings
        return MyNoise.OctavePerlin(x + domainOffset.x, z + domainOffset.y, defaultNoiseSettings);

    }


    // Generate domain offset with a X and Z(Y) co-ordinate
    public Vector2 GenerateDomainOffset(int x, int z) {

        // variable noiseX which takes in the x, z and noise domain X and multiplies it by the amplitude X
        var noiseX = MyNoise.OctavePerlin(x, z, noiseDomainX) * amplitudeX;

        // variable noiseY which takes in the x, z and noise domain Y and multiplies it by the amplitude Y
        var noiseY = MyNoise.OctavePerlin(x, z, noiseDomainY) * amplitudeY;

        // return new vector 2 of the noiseX and the noiseY
        return new Vector2(noiseX, noiseY);

    }

    // Generate the domain offset int using X and Z (calculate centers of biomes)
    public Vector2Int GenerateDomainOffsetInt(int x, int z) {

        // return the vector 2 int using Generate domain offset passing in X and Z
        return Vector2Int.RoundToInt(GenerateDomainOffset(x, z));

    }

}
