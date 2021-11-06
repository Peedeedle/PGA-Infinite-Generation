////////////////////////////////////////////////////////////
// File: MyNoise.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 30/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyNoise
{


    // public static float for Remapping the values, from the (minvalue) 0---1 (Max value), (OutputMin) 0---HeightOfChunk (OutputMax)
    public static float RemapValue(float value, float initialMin, float initialMax, float outputMin, float outputMax) {

        // return the output minimum value
        return outputMin + (value - initialMin) * (outputMax - outputMin) / (initialMax - initialMin);

    }

    // public static float for Remapping the value, from 0-1, output min and max
    public static float RemapValue01(float value, float outputMin, float outputMax) {

        // return the output min and max
        return outputMin + (value - 0) * (outputMax - outputMin) / (1 - 0);

    }

    // public static float for Remapping the value to an int
    public static float RemapValue01ToInt(float value, float outputMin, float outputMax) {

        // cast RemapValue01 to an int and then return it
        return (int)RemapValue01(value, outputMin, outputMax);

    }

    // public static float for creating plataus as a noise value taking in the settings
    public static float Redistribution(float noise, NoiseSettings settings) {

        // return the value as a Math power, value to multiply noise, exponent value (set and tweak values)
        return Mathf.Pow(noise * settings.redistibutionModifier, settings.exponent);

    }

    // take in the float x and z and the noise settings
    public static float OctavePerlin (float x, float z, NoiseSettings settings) {

        // zoom in or out of the perlin noise function to see more or less hills in the output
        // Calculate for X and Z value
        x *= settings.noiseZoom;
        z *= settings.noiseZoom;

        // For safety (add it) to make sure there isnt an int vvvvvv
        x += settings.noiseZoom;
        z += settings.noiseZoom;

        // total value of noise
        float total = 0;

        // frequency of the noise = 1
        float frequency = 1;

        // amplitude value for noise
        float amplitude = 1;

        // normalize value between 0-1
        float amplitudeSum = 0;

        
        // for each number of octaves
        for (int i = 0; i < settings.octaves; i++) {

            // Calculate the total, perlin noise, offset for noise settings and the world offset to include the seed for
            // the generation, multiply by frequency, multiply perlin noise by amplitude
            total += Mathf.PerlinNoise((settings.offset.x + settings.worldOffset.x + x) * frequency,
                (settings.offset.y + settings.worldOffset.y + z) * frequency) * amplitude;

            // amplitude sum = amplitude (normalize value)
            amplitudeSum += amplitude;

            // amplitude * persistence (* 2 or / 2 it will give different results
            amplitude *= settings.persistence;

            // x frequency by 2 to give a higher level frequency
            frequency *= 2;

        }


        // return value of the total value of the noise divided by the sum of the amplitude
        return total / amplitudeSum;

    }



}
