////////////////////////////////////////////////////////////
// File: BiomeCenterFinder.cs
// Author: Jack Peedle
// Date Created: 08/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 08/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BiomeCenterFinder
{

    //
    public static List<Vector2Int> neightbours8Directions = new List<Vector2Int> {

        //
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),

    };

    //
    public static List<Vector3Int> CalculateBiomeCenters(Vector3 startingPosition, int drawRange, int mapSize) {

        //
        int biomeLength = drawRange * mapSize;

        //
        Vector3Int origin = new Vector3Int(Mathf.RoundToInt(startingPosition.x / biomeLength) * biomeLength, 0,
            Mathf.RoundToInt(startingPosition.z / biomeLength * biomeLength));

        //
        HashSet<Vector3Int> biomeCenterTemp = new HashSet<Vector3Int>();

        //
        biomeCenterTemp.Add(origin);

        //
        foreach (Vector2Int OffsetXZ in neightbours8Directions) {

            //
            Vector3Int newBiomePoint_1 = new Vector3Int(origin.x + OffsetXZ.x * biomeLength, 0, origin.z + OffsetXZ.y * biomeLength);

            //
            Vector3Int newBiomePoint_2 = new Vector3Int(origin.x + OffsetXZ.x * biomeLength, 0, origin.z + OffsetXZ.y * 2 * biomeLength);

            //
            Vector3Int newBiomePoint_3 = new Vector3Int(origin.x + OffsetXZ.x * 2 * biomeLength, 0, origin.z + OffsetXZ.y * biomeLength);

            //
            Vector3Int newBiomePoint_4 = new Vector3Int(origin.x + OffsetXZ.x * 2 * biomeLength, 0, origin.z + OffsetXZ.y * 2 * biomeLength);


            //
            biomeCenterTemp.Add(newBiomePoint_1);

            //
            biomeCenterTemp.Add(newBiomePoint_2);

            //
            biomeCenterTemp.Add(newBiomePoint_3);

            //
            biomeCenterTemp.Add(newBiomePoint_4);

        }

        //
        return new List<Vector3Int>(biomeCenterTemp);

    }

}
