////////////////////////////////////////////////////////////
// File: JungleTreeData.cs
// Author: Jack Peedle
// Date Created: 02/12/21
// Last Edited By: Jack Peedle
// Date Last Edited: 02/12/21
// Brief: Tree data which handles the tree positions and trea leaves positions
//////////////////////////////////////////////////////////// 

using System.Collections.Generic;
using UnityEngine;

public class JungleTreeData
{

    // list of vector 2 ints for the tree positions
    public List<Vector2Int> jungleTreePositions = new List<Vector2Int>();

    // list of vector 3 ints for the solid tree leaves
    public List<Vector3Int> jungleTreeLeavesSolid = new List<Vector3Int>();


}
