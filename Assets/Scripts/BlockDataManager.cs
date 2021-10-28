////////////////////////////////////////////////////////////
// File: BlockDataManager.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 21/10/21
// Brief: 
//////////////////////////////////////////////////////////// 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BlockDataSO;

public class BlockDataManager : MonoBehaviour
{

    // offset the texture so each texture has no gaps at the edges
    public static float textureOffset = 0.001f;

    // float for the tilesize x and tilesize y
    public static float tileSizeX, tileSizeY;

    // dictionary of block type and texture data, look through each texture data and place with key of block type and texture data
    public static Dictionary<BlockType, TextureData> blockTextureDataDictionary = new Dictionary<BlockType, TextureData>();

    // blockdataSO reference
    public BlockDataSO textureData;


    // On Awake
    private void Awake() {
        
        // for each item in texture data list in BlockDataSO
        foreach (var item in textureData.textureDataList) {

            // if an item BlockType is not a duplicate
            if (blockTextureDataDictionary.ContainsKey(item.blockType) == false) {

                // populate dictionary with the block type (Add item (Texture data object))
                blockTextureDataDictionary.Add(item.blockType, item);

            };

        }

        // save texture data.x into tile size x
        tileSizeX = textureData.textureSizeX;

        // save texture data.y into tile size y
        tileSizeY = textureData.textureSizeY;

    }


}

/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BlockDataSO;

public class BlockDataManager : MonoBehaviour
{
    public static float textureOffset = 0.001f;
    public static float tileSizeX, tileSizeY;
    public static Dictionary<BlockType, TextureData> blockTextureDataDictionary = new Dictionary<BlockType, TextureData>();
    public BlockDataSO textureData;

    private void Awake() {
        foreach (var item in textureData.textureDataList) {
            if (blockTextureDataDictionary.ContainsKey(item.blockType) == false) {
                blockTextureDataDictionary.Add(item.blockType, item);
            };
        }
        tileSizeX = textureData.textureSizeX;
        tileSizeY = textureData.textureSizeY;
    }
}


*/
