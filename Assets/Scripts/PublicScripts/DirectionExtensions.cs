////////////////////////////////////////////////////////////
// File: DirectionExtension.cs
// Author: Jack Peedle
// Date Created: 25/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Direction extentions sets the directions to vector 3 ints
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DirectionExtensions
{
    
    // Vector 3 int for a method to get the direction by using the direction script
    public static Vector3Int GetVector(this Direction direction) {

        // return a direction
        return direction switch {

            // direction up = vector3Int up
            Direction.up => Vector3Int.up,

            // direction down = vector3Int down
            Direction.down => Vector3Int.down,

            // direction right = vector3Int right
            Direction.right => Vector3Int.right,

            // direction left = vector3Int left
            Direction.left => Vector3Int.left,

            // direction forward = vector3Int forward
            Direction.forward => Vector3Int.forward,

            // direction backwards = vector3Int backwards
            Direction.backwards => Vector3Int.back,

            //
            _ => throw new Exception("Invalid input direction")

        };

    }

}
