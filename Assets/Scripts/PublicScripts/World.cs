////////////////////////////////////////////////////////////
// File: World.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 28/11/21
// Brief: world script for the variables to generate the world
//////////////////////////////////////////////////////////// 


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class World : MonoBehaviour
{

    // int for map size in chunks
    public int mapSizeInChunks = 6;

    // int for the chunk size and chunk height
    public int chunkSize = 16, chunkHeight = 16;

    // public int for the chunk drawing range
    public int chunkDrawingRange;

    // chunk prefab GameObject
    public GameObject chunkPrefab;

    // reference to the world renderer
    public WorldRenderer worldRenderer;

    // Reference to the terrain generator script
    public TerrainGenerator terrainGenerator;

    // vector 2 int for the map seed offset (allowing different seeds and randomization etc)
    public Vector2Int mapSeedOffset;

    // cancellation token source called taskTokenSource
    CancellationTokenSource taskTokenSource = new CancellationTokenSource();

    // unity event for onWorldCreate and OnNewChunksGenerated
    public UnityEvent OnWorldCreated, OnNewChunksGenerated;

    //
    public ButtonManager buttonManager;

    // public world data called world data
    public WorldData worldData { get; private set; }

    // public bool for isWorldCreated
    public bool isWorldCreated { get; private set; }


    // on Awake
    private void Awake() {

        // world data = new world data
        worldData = new WorldData {

            // chunk height = this chunk height
            chunkHeight = this.chunkHeight,

            // chunk size = this chunk size
            chunkSize = this.chunkSize,

            // new chunk data dictionary of vector3ints and chunk data
            chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>(),

            // new chunk dictionary of vector3ints and chunk renderer
            chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>()

            

        };

    }


    // Generate the World / Meshes
    public async void GenerateWorld() {

        // change the UI canvas to the game canvas
        buttonManager.ChangeToGameCanvas();

        // await the generate world at zero position
        await GenerateWorld(Vector3Int.zero);

    }

    // private task to generate world on a vector3int position
    private async Task GenerateWorld(Vector3Int position) {

        // generate biome points passing in the position, chunkDrawRange, chunk size and mapSeedOffset
        terrainGenerator.GenerateBiomePoints(position, chunkDrawingRange, chunkSize, mapSeedOffset);

        // await task and get position from start passing in the position and token
        WorldGenerationData worldGenerationData = await Task.Run(() => GetPositionFromStart(position), taskTokenSource.Token);

        // for each vector3Int position in the worldGenerationData chunkPositionsToRemove
        foreach(Vector3Int pos in worldGenerationData.chunkPositionsToRemove) {

            // remove this chunk at position
            WorldDataHelper.RemoveChunk(this, pos);

        }

        // for each vector3Int position in the worldGenerationData chunkDataToRemove
        foreach (Vector3Int pos in worldGenerationData.chunkDataToRemove) {

            // remove this chunkData at position
            WorldDataHelper.RemoveChunkData(this, pos);

        }

        // concurrent dictionary of vector3ints and chunk data called dataDictionary = null
        ConcurrentDictionary<Vector3Int, ChunkData> dataDictionary = null;

        // try
        try {

            // await the calculate world chunk data of chunk data positions to create
            dataDictionary = await CalculateWorldChunkData(worldGenerationData.chunkDataPositionsToCreate);

        }
        // catch the exception
        catch (Exception) {

            // debug 
            Debug.Log("Task Cancelled");

            // return
            return;

        }


        // foreach calculated data in dataDictionary
        foreach(var calculatedData in dataDictionary) {

            // add calculated data key and calculated data value to the chunk data dictionary
            worldData.chunkDataDictionary.Add(calculatedData.Key, calculatedData.Value);

        }

        // for each chunk data in chunkDataDictionary Values
        foreach (var chunkData in worldData.chunkDataDictionary.Values) {

            // add the tree leaves using the chunk data
            AddTreeLeaves(chunkData);

            // add the tree leaves using the chunk data
            AddSnowTreeLeaves(chunkData);

            //
            AddJungleTreeLeaves(chunkData);

            //
            AddCursedTreeLeaves(chunkData);

            //
            AddRMushroomTreeLeaves(chunkData);

            //
            AddWMushroomTreeLeaves(chunkData);

        }

        // concurrent dictionary of vector3ints and meshData called meshDataDictionary = new concurrent dictionary
        ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary = new ConcurrentDictionary<Vector3Int, MeshData>();

        // list of chunk data called data to render = chunk data dictionary key => chunk positions to create.select
        // (kayValuePair.Value).List
        List<ChunkData> dataToRender = worldData.chunkDataDictionary.Where(KeyValuePair => worldGenerationData.chunkPositionsToCreate
        .Contains(KeyValuePair.Key)).Select(KeyValuePair => KeyValuePair.Value).ToList();

        // try
        try {

            // meshDataDictionary = await the Create mesh data async of data to render
            meshDataDictionary = await CreateMeshDataAsync(dataToRender);

        }
        // catch the exception
        catch (Exception) {

            // debug
            Debug.Log("Task Cancelled 1");

            // return
            return;

        }

        // start the chunkCreationCoroutine of meshDataDictionary
        StartCoroutine(ChunkCreationCoroutine(meshDataDictionary));

    }


    // add tree leaves passing in the chunk data
    private void AddTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var treeLeaves in chunkData.treeData.treeLeavesSolid) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, treeLeaves, BlockType.TreeLeavesSolid);

        }

    }

    // add tree leaves passing in the chunk data
    private void AddSnowTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var snowTreeLeaves in chunkData.snowTreeData.snowTreeLeaves) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, snowTreeLeaves, BlockType.SnowLeaves);

        }

    }

    // add tree leaves passing in the chunk data
    private void AddJungleTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var jungleTreeLeaves in chunkData.jungleTreeData.jungleTreeLeavesSolid) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, jungleTreeLeaves, BlockType.JungleTreeLeaves);

        }


    }

    // add tree leaves passing in the chunk data
    private void AddCursedTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var cursedTreeLeaves in chunkData.cursedTreeData.cursedTreeLeavesSolid) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, cursedTreeLeaves, BlockType.CursedTreeLeaves);

        }

    }

    // add tree leaves passing in the chunk data
    private void AddRMushroomTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var rMushroomTreeLeaves in chunkData.rMushroomTreeData.rMushroomTreeLeavesSolid) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, rMushroomTreeLeaves, BlockType.RMushroomTreeLeaves);

        }

    }

    // add tree leaves passing in the chunk data
    private void AddWMushroomTreeLeaves(ChunkData chunkData) {

        // for each tree leaves in the tree data tree leaves solid
        foreach (var wMushroomTreeLeaves in chunkData.wMushroomTreeData.wMushroomTreeLeavesSolid) {

            // set block passing chunkData, tree leaves and the tree leaves solid block type
            Chunk.SetBlock(chunkData, wMushroomTreeLeaves, BlockType.WMushroomTreeLeaves);

        }

    }

    // concurrent dictionary of vector3ints and mesh data called CreateMeshDataAsync of data to render
    private Task<ConcurrentDictionary<Vector3Int, MeshData>> CreateMeshDataAsync(List<ChunkData> dataToRender) {

        // concurrent dictionary of vector3ints and mesh data called dictionary = new concurrent dictionary
        ConcurrentDictionary<Vector3Int, MeshData> dictionary = new ConcurrentDictionary<Vector3Int, MeshData>();

        // run this task
        return Task.Run(() => {

            // for each chunk data in data to render
            foreach (ChunkData data in dataToRender) {

                // if the token is cancellation request
                if (taskTokenSource.Token.IsCancellationRequested) {

                    // throw IfCancellationRequest
                    taskTokenSource.Token.ThrowIfCancellationRequested();

                }

                // mesh data = getChunkMeshData passing in data
                MeshData meshData = Chunk.GetChunkMeshData(data);

                // try and add world position and meshData to the dictionary
                dictionary.TryAdd(data.worldPosition, meshData);

            }

            // return the dictionary
            return dictionary;

            // return the token
        }, taskTokenSource.Token);

    }

    // concurrent dictionary of vector3ints and chunk data called CalculateWorldChunkData
    // of list of vector3Ints called chunkDataPositionsToCreate
    private Task<ConcurrentDictionary<Vector3Int, ChunkData>> CalculateWorldChunkData(List<Vector3Int> chunkDataPositionsToCreate) {

        // concurrent dictionary of vector3ints and chunk data called dictionary = new concurrent dictionary
        ConcurrentDictionary<Vector3Int, ChunkData> dictionary = new ConcurrentDictionary<Vector3Int, ChunkData>();

        // run this task
        return Task.Run(() => {

            // for each vector3Int position in chunkDataPositionsToCreate
            foreach (Vector3Int pos in chunkDataPositionsToCreate) {

                // if the token is cancellation request
                if (taskTokenSource.Token.IsCancellationRequested) {

                    // throw IfCancellationRequest
                    taskTokenSource.Token.ThrowIfCancellationRequested();

                }

                // chunk data = new chunk data passing in chunk size, chunk height, this and position
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, pos);

                // chunkdata new data = generate chunk data passing in the data and mapSeedOffset
                ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);

                // try and add position and new Data to the dictionary
                dictionary.TryAdd(pos, newData);

            }

            // return dictionary
            return dictionary;

            // return the token
        }, taskTokenSource.Token);

    }

    // Ienumerator ChunkCreationCoroutine passing in the concurrentDictionary of vector3ints mesh data and meshDataDictionary
    IEnumerator ChunkCreationCoroutine(ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary) {

        // for each item in mesh data dictionary
        foreach(var item in meshDataDictionary) {

            // create the chunk passing in the world data, item.key and item value
            CreateChunk(worldData, item.Key, item.Value);

            // return new wait for end of frame
            yield return new WaitForEndOfFrame();

        }

        //  if isWorldCreated = false
        if (isWorldCreated == false) {

            // set the isWorldCreated to true
            isWorldCreated = true;

            // invoke the onWorldCreated
            OnWorldCreated?.Invoke();

        }

    }

    // create chunk passing in the world data, vector3int position and mesh data
    private void CreateChunk(WorldData worldData, Vector3Int position, MeshData meshData) {

        // chunk renderer = Render Chunk passing in the world data, position and mesh data
        ChunkRenderer chunkRenderer = worldRenderer.RenderChunk(worldData, position, meshData);

        // add the position and chunkRenderer to the chunkDictionary
        worldData.chunkDictionary.Add(position, chunkRenderer);

    }

    // Set the block passing raycast hit and block type
    internal bool SetBlock(RaycastHit hit, BlockType blockType) {

        // chunk renderer = hit object get component chunkRenderer
        ChunkRenderer chunk = hit.collider.GetComponent<ChunkRenderer>();

        // if chunk = null
        if (chunk == null)

            // return false
            return false;

        // vector3 int position = get block position of raycast hit
        Vector3Int pos = GetBlockPos(hit);

        // set the block passing in the world reference, position and block type
        WorldDataHelper.SetBlock(chunk.ChunkData.WorldReference, pos, blockType);

        // set the modified by the player bool to true
        chunk.ModifiedByThePlayer = true;

        // if chunk is on edge with the chunk data and position
        if (Chunk.IsOnEdge(chunk.ChunkData, pos)) {

            // list of chunk data called neighbour data list = get edge neighbour chunk passing in the chunk data and position
            List<ChunkData> neighbourDataList = Chunk.GetEdgeNeighbourChunk(chunk.ChunkData, pos);

            // for each neighbour data in neighbour data list
            foreach (ChunkData neightbourData in neighbourDataList) {

                // chunkToUpdate = get chunk passing in the world reference and the world position
                ChunkRenderer chunkToUpdate = WorldDataHelper.GetChunk(neightbourData.WorldReference, neightbourData.worldPosition);

                // if chunk to update is not = to null
                if (chunkToUpdate != null) {

                    // update the chunk
                    chunkToUpdate.UpdateChunk();

                }

            }

        }

        // update the chunk
        chunk.UpdateChunk();

        // return true
        return true;

    }

    // get block position passing in the raycast hit
    private Vector3Int GetBlockPos(RaycastHit hit) {

        // vector 3 position = new position of hit point x and hit normal x (X), hit point y and hit normal y (Y),
        // hit point z and hit normal z (Z)
        Vector3 pos = new Vector3(GetBlockPositionIn(hit.point.x, hit.normal.x), GetBlockPositionIn(hit.point.y, hit.normal.y),
            GetBlockPositionIn(hit.point.z, hit.normal.z));

        // return position as a rounded to int vector3int
        return Vector3Int.RoundToInt(pos);

    }

    // get block position in passing in the position and normal
    private float GetBlockPositionIn(float pos, float normal) {

        // if the position % 1 is = 0.5
        if (Mathf.Abs(pos % 1) == 0.5f) {

            // position -= normal divided by 2
            pos -= (normal / 2);

        }

        // return the float position
        return (float)pos;

    }

    // get position from start passing in the starting position
    private WorldGenerationData GetPositionFromStart(Vector3Int startingPosition) {

        // list of all the chunk positions needed = get the chunk positions around the starting point,
        // passing in this and the starting position
        List<Vector3Int> allChunkPositionsNeeded = WorldDataHelper.GetChunkPositionsAroundStartingPosition(this, startingPosition);

        // list of all the chunk data positions needed = get the chunk data positions around the starting point,
        // passing in this and the starting position 
        List<Vector3Int> allChunkDataPositionsNeeded = WorldDataHelper.GetDataPositionsAroundStartingPosition(this, startingPosition);



        // list of the chunk positions to create = select positions to create passing in the world data, all the chunk positions
        // needed and the starting position
        List<Vector3Int> chunkPositionsToCreate = WorldDataHelper.SelectPositionsToCreate(worldData, allChunkPositionsNeeded, 
            startingPosition);

        // list of the chunk data positions to create = select data positions to create passing in the world data,
        // all the chunk positions needed and the starting position
        List<Vector3Int> chunkDataPositionsToCreate = WorldDataHelper.SelectDataPositionsToCreate(worldData, 
            allChunkDataPositionsNeeded, startingPosition);



        // list of the chunk positions to remove = get undeeded chunks passing in the world data and all of the chunk positions needed
        List<Vector3Int> chunkPositionsToRemove = WorldDataHelper.GetUnneededChunks(worldData, allChunkPositionsNeeded);

        // list of the chunk data positions to remove = get undeeded data passing in the world data and all of the
        // chunk positions needed
        List<Vector3Int> chunkDataToRemove = WorldDataHelper.GetUnneededData(worldData, allChunkDataPositionsNeeded);

        // world generation data = new world generation data
        WorldGenerationData data = new WorldGenerationData {

            // chunk positions to create = chunk positions to create
            chunkPositionsToCreate = chunkPositionsToCreate,

            // chunk data positions to create = chunk data positions to create
            chunkDataPositionsToCreate = chunkDataPositionsToCreate,

            // chunk positions to remove = chunk positions to remove
            chunkPositionsToRemove = chunkPositionsToRemove,

            // chunk data to remove = chunk data to remove
            chunkDataToRemove = chunkDataToRemove,

            // chunk positions to update = new vector3int list
            chunkPositionsToUpdate = new List<Vector3Int>()

        };

        // return the data
        return data;

    }

    // get block from chunk Coordinates passing in the chunk data, x, y, z
    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {

        // get the chunk position from the block coordinates
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);

        // access chunk data of this chunk and set it to null
        ChunkData containerChunk = null;

        // get value position and output the container chunk
        worldData.chunkDataDictionary.TryGetValue(pos, out containerChunk);

        // if the containerChunk == null
        if (containerChunk == null)

            // return the BlockType Nothing
            return BlockType.Nothing;

        // get the position of the block from the chunk that was found, pass in containerChunk which is where the block is found in
        // and pass in a new vector 3 using x,y,z
        Vector3Int blockInChunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));

        // return the chunk using getblockfromchunkcoordinates, get correct block type from chunk and the coordinates
        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInChunkCoordinates);

    }

    // on disable
    public void OnDisable() {

        // token source cancel
        taskTokenSource.Cancel();

    }

}

// world generation data
public struct WorldGenerationData
{

    // list of vector3Ints chunkPositionsToCreate
    public List<Vector3Int> chunkPositionsToCreate;

    // list of vector3Ints chunkDataPositionsToCreate
    public List<Vector3Int> chunkDataPositionsToCreate;

    // list of vector3Ints chunkPositionsToRemove
    public List<Vector3Int> chunkPositionsToRemove;

    // list of vector3Ints chunkDataToRemove
    public List<Vector3Int> chunkDataToRemove;

    // list of vector3Ints chunkPositionsToUpdate
    public List<Vector3Int> chunkPositionsToUpdate;

}

// world data
public struct WorldData
{

    // public dictionary of vector3Ints and chunk data called chunkDataDictionary
    public Dictionary<Vector3Int, ChunkData> chunkDataDictionary;

    // public dictionary of vector3Ints and chunk renderer called chunkDictionary
    public Dictionary<Vector3Int, ChunkRenderer> chunkDictionary;

    // public int for chunk size
    public int chunkSize;

    // public int for chunk height
    public int chunkHeight;

}
