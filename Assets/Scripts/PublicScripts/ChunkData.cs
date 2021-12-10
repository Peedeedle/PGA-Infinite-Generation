////////////////////////////////////////////////////////////
// File: ChunkData.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: script to store the chunk data
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

    // reference to the tree data
    public TreeData treeData;

    //
    public CactusData cactusData;

    //
    public SnowTreeData snowTreeData;

    //
    public PresentsData presentsData;

    //
    public JungleTreeData jungleTreeData;

    //
    public LilyPadData lilyPadData;

    //
    public SugarCaneData sugarCaneData;

    //
    public CursedTreeData cursedTreeData;

    //
    public PumpkinData pumpkinData;

    //
    public TomatoData tomatoData;

    //
    public RMushroomTreeData rMushroomTreeData;

    //
    public WMushroomTreeData wMushroomTreeData;

    //
    public BlueBushData blueBushData;

    //
    public GreenBushData greenBushData;

    //
    public LightBlueBushData lightBlueBushData;

    //
    public PurpleBushData purpleBushData;

    //
    public RedBushData redBushData;

    //
    public YellowBushData yellowBushData;

    //
    public MelonData melonData;


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




