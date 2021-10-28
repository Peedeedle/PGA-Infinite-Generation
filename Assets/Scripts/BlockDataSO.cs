////////////////////////////////////////////////////////////
// File: BlockDataSO.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 21/10/21
// Brief: 
//////////////////////////////////////////////////////////// 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create an asset menu  
[CreateAssetMenu(fileName = "Block Data", menuName = "Data/Block Data")]
public class BlockDataSO : ScriptableObject
{


    // float of the x and y texture sizes
    public float textureSizeX, textureSizeY;

    // list of the texture data
    public List<TextureData> textureDataList;


    // serializable texture data
    [Serializable]
    public class TextureData
    {

        // block type
        public BlockType blockType;

        // (textures) 2D ints for up, down and side
        public Vector2Int up, down, side;

        // bool if is solid (Water = false, ground = true)
        public bool isSolid = true;

        // bool if needs to generate collider
        public bool generatesCollider = true;


    }


}

/*

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "Data/Block Data")]
public class BlockDataSO : ScriptableObject
{
    public float textureSizeX, textureSizeY;
    public List<TextureData> textureDataList;
}

[Serializable]
public class TextureData
{
    public BlockType blockType;
    public Vector2Int up, down, side;
    public bool isSolid = true;
    public bool generatesCollider = true;
}

*/