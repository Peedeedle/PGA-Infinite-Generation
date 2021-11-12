////////////////////////////////////////////////////////////
// File: BiomeCenterFinder.cs
// Author: Jack Peedle
// Date Created: 08/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Script for finding the centre of the biome
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BiomeCenterFinder
{

    // list of vector 2 ints for the neighbours in all directions
    public static List<Vector2Int> neightbours8Directions = new List<Vector2Int> {

        // [1][1][1]
        // [1][0][1]
        // [1][1][1]
        //
        // All of the values with 1 ^^ represent one of these vector2ints
        // with the 0 being the main chunk, it will get the neighbouring chunks around it [1]'s
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),

    };

    // list of vector3ints calculate biome centers using the starting position, draw range and map size
    public static List<Vector3Int> CalculateBiomeCenters(Vector3 startingPosition, int drawRange, int mapSize) {

        // biome length = draw range times the map size
        int biomeLength = drawRange * mapSize;

        // vector 3 int for the origin of a point, x = starting position x divided by the biome length, the * biomelength
        // 0 on the y, starting position z divided by the biomelength * the biome length = x,y,z of the origin
        Vector3Int origin = new Vector3Int(Mathf.RoundToInt(startingPosition.x / biomeLength) * biomeLength, 0,
            Mathf.RoundToInt(startingPosition.z / biomeLength * biomeLength));

        // HashSet of vector3ints for the center biome temperature
        HashSet<Vector3Int> biomeCenterTemp = new HashSet<Vector3Int>();

        // add the origin to the biome center temperature
        biomeCenterTemp.Add(origin);

        // for each vector2int in the Offset X and Z in the 8 neighbours directions
        foreach (Vector2Int OffsetXZ in neightbours8Directions) {

            // vector 3 int for the biome point1  = origin x + offset x * biome length (X), 0 (y), origin z + offset y * biome length
            Vector3Int newBiomePoint_1 = new Vector3Int(origin.x + OffsetXZ.x * biomeLength, 0, origin.z + OffsetXZ.y * biomeLength);

            // vector 3 int for the biome point2  = origin x + offset x * biome length (X), 0 (y), origin z + offset y *2* biome length
            Vector3Int newBiomePoint_2 = new Vector3Int(origin.x + OffsetXZ.x * biomeLength, 0, origin.z + OffsetXZ.y * 2 * biomeLength);

            // vector 3 int for the biome point3  = origin x + offset x *2* biome length (X), 0 (y), origin z + offset y * biome length
            Vector3Int newBiomePoint_3 = new Vector3Int(origin.x + OffsetXZ.x * 2 * biomeLength, 0, origin.z + OffsetXZ.y * biomeLength);

            // vector 3 int for the biome point4  = origin x + offset x *2* biome length (X), 0 (y), origin z + offset y *2* biome length
            Vector3Int newBiomePoint_4 = new Vector3Int(origin.x + OffsetXZ.x * 2 * biomeLength, 0, origin.z + OffsetXZ.y * 2 * biomeLength);


            // add the biome point 1 to the biome center temperature
            biomeCenterTemp.Add(newBiomePoint_1);

            // add the biome point 2 to the biome center temperature
            biomeCenterTemp.Add(newBiomePoint_2);

            // add the biome point 3 to the biome center temperature
            biomeCenterTemp.Add(newBiomePoint_3);

            // add the biome point 4 to the biome center temperature
            biomeCenterTemp.Add(newBiomePoint_4);

        }

        // return the new list of biome center temperatures as vector 3 ints
        return new List<Vector3Int>(biomeCenterTemp);

    }

}
