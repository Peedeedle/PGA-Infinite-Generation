////////////////////////////////////////////////////////////
// File: ChunkData.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 20/10/21
// Brief: 
//////////////////////////////////////////////////////////// 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{

    // array of block types
    public BlockType[] blocks;

    // the chunk size per map
    public int chunkSize = 16;

    // chunk height per map
    public int chunkHeight = 100;

    // world reference
    public World WorldReference;

    // vector 3 int for the world position
    public Vector3Int worldPosition;

    // bool for if something has been modified by the player
    public bool modifiedByThePlayer = false;

    //
    internal TreeData treeData;


    // Contructor for the chunk data
    public ChunkData (int chunkSize, int chunkHeight, World world, Vector3Int worldPosition) {

        // this chunk height = chunk height
        this.chunkHeight = chunkHeight;

        // this chunk size = chunk size
        this.chunkSize = chunkSize;

        // this world reference = to world
        this.WorldReference = world;

        // this world position = world position
        this.worldPosition = worldPosition;

        // Initialize chunk array with [chunkSize * chunkHeight * chunkSize]
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];

    }

}



/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    public BlockType[] blocks;
    public int chunkSize = 16;
    public int chunkHeight = 100;
    public World worldReference;
    public Vector3Int worldPosition;

    public bool modifiedByThePlayer = false;

    public ChunkData(int chunkSize, int chunkHeight, World world, Vector3Int worldPosition) {
        this.chunkHeight = chunkHeight;
        this.chunkSize = chunkSize;
        this.worldReference = world;
        this.worldPosition = worldPosition;
        blocks = new BlockType[chunkSize * chunkHeight * chunkSize];
    }

}

*/
