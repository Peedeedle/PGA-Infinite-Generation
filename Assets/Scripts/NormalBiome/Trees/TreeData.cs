////////////////////////////////////////////////////////////
// File: TreeData.cs
// Author: Jack Peedle
// Date Created: 06/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: Tree data which handles the tree positions and tree leaves positions
//////////////////////////////////////////////////////////// 

using System.Collections.Generic;
using UnityEngine;

public class TreeData
{

    // list of vector 2 ints for the tree positions
    public List<Vector2Int> treePositions = new List<Vector2Int>();

    // list of vector 3 ints for the solid tree leaves
    public List<Vector3Int> treeLeavesSolid = new List<Vector3Int>();


}
