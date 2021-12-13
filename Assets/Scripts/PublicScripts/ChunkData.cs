////////////////////////////////////////////////////////////
// File: ChunkData.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
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

    // reference to the cactus data
    public CactusData cactusData;

    // reference to the snowtree data
    public SnowTreeData snowTreeData;

    // reference to the presents data
    public PresentsData presentsData;

    // reference to the jungle tree data
    public JungleTreeData jungleTreeData;

    // reference to the lillypad data
    public LilyPadData lilyPadData;

    // reference to the sugarcane data
    public SugarCaneData sugarCaneData;

    // reference to the cursed tree data
    public CursedTreeData cursedTreeData;

    // reference to the pumpkin data
    public PumpkinData pumpkinData;

    // reference to the tomato data
    public TomatoData tomatoData;

    // reference to the red mushroom tree data
    public RMushroomTreeData rMushroomTreeData;

    // reference to the white mushroom tree data
    public WMushroomTreeData wMushroomTreeData;

    // reference to the blue bush data
    public BlueBushData blueBushData;

    // reference to the green bush data
    public GreenBushData greenBushData;

    // reference to the light blue bush data
    public LightBlueBushData lightBlueBushData;

    // reference to the purple bush data
    public PurpleBushData purpleBushData;

    // reference to the red bush data
    public RedBushData redBushData;

    // reference to the yellow bush data
    public YellowBushData yellowBushData;

    // reference to the melon data
    public MelonData melonData;

    // reference to the cola cube data
    public ColaCubeData colaCubeData;

    // reference to the grape cube data
    public GrapeCubeData grapeCubeData;

    // reference to the orange cube data
    public OrangeCubeData orangeCubeData;

    // reference to the pineapple cube data
    public PineappleCubeData pineappleCubeData;

    // reference to the smore data
    public SmoreData smoreData;

    // reference to the red cane data
    public RedCaneData redCaneData;

    // reference to the green cane data
    public GreenCaneData greenCaneData;

    // reference to the candy tree data
    public CandyTreeData candyTreeData;


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




