////////////////////////////////////////////////////////////
// File: SmoreDataProccessing.cs
// Author: Jack Peedle
// Date Created: 12/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/12/21
// Brief: Proccessing the data of the meshes
//////////////////////////////////////////////////////////// 

using System;
using System.Collections.Generic;
using UnityEngine;

public static class SmoreDataProccessing
{

    // static list of vector 2 ints called directions
    public static List<Vector2Int> directions = new List<Vector2Int> {

        // Vector 2 int for the north direction
        new Vector2Int (0, 1), 

        // Vector 2 int for the north east direction
        new Vector2Int (1, 1),

        // Vector 2 int for the east direction
        new Vector2Int (1, 0),

        // Vector 2 int for the south east direction
        new Vector2Int (-1, 1),

        // Vector 2 int for the south direction
        new Vector2Int (-1, 0),

        // Vector 2 int for the south west direction
        new Vector2Int (-1, -1),

        // Vector 2 int for the west direction
        new Vector2Int (0, -1),

        // Vector 2 int for the north west direction
        new Vector2Int (1, -1),

    };


    // list of vector 2 ints that takes in float array, noise data and X and Y 
    public static List<Vector2Int> FindLocalMaxima(float[,] dataMatrix, int xCoord, int zCoord) {

        // new list of vector 2 ints for the maximas
        List<Vector2Int> maximas = new List<Vector2Int>();

        // for each x in data matrix
        for (int x = 0; x < dataMatrix.GetLength(0); x++) {

            // for each y in data matrix
            for (int y = 0; y < dataMatrix.GetLength(1); y++) {

                // float noiseVal = data matirx x and y co-ordinates
                float noiseVal = dataMatrix[x, y];

                // if checkNeighbours (data matrix and the x and y, take in the float neighbour Noise, if Neighbour Noise is less than
                // or = to the noise Val (then return true)
                if (CheckNeighbours(dataMatrix, x, y, (neighbourNoise) => neighbourNoise < noiseVal)) {

                    // (if true) have coordinates for where the tree will be placed
                    maximas.Add(new Vector2Int(xCoord + x, zCoord + y));

                }

            }

        }

        // return the maximas
        return maximas;

    }

    // Static bool method to check neighbouring blocks and meshs
    private static bool CheckNeighbours(float[,] dataMatrix, int x, int y, Func<float, bool> successCondition) {

        // for each variable "dir" in directions
        foreach(var dir in directions) {

            // variable newPost = vector 2 int (dir.x and dir.y)
            var newPost = new Vector2Int(x + dir.x, y + dir.y);

            // if x is less than 0 or is more than or = to data matrix get length(0), // if y is less than 0 or is more than or = to data matrix get length(1)
            if (newPost.x < 0 || newPost.x >= dataMatrix.GetLength(0) || newPost.y < 0 || newPost.y >= dataMatrix.GetLength(1)) {

                // Continue
                continue;

            }

            // if the neighbours value is greater than the positions value
            if (successCondition(dataMatrix[x + dir.x, y + dir.y]) == false) {

                // return false
                return false;

            }


        }

        // checked all neighbours, found localMaxima
        return true;

    }

}