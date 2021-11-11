////////////////////////////////////////////////////////////
// File: World.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 11/11/21
// Brief: 
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

    //
    public int chunkDrawingRange;

    // chunk prefab GameObject
    public GameObject chunkPrefab;

    //
    public WorldRenderer worldRenderer;

    // Reference to the terrain generator script
    public TerrainGenerator terrainGenerator;

    // vector 2 int for the map seed offset (allowing different seeds and randomization etc)
    public Vector2Int mapSeedOffset;


    // Dictionary of Vector3Ints and ChunkData (Store the data of chunks we want to generate on map)
    //Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();

    // Dictionary of Vector3Ints and ChunkRenderer (Remove chunks from the map)
    //Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

    //
    CancellationTokenSource taskTokenSource = new CancellationTokenSource();

    //
    public UnityEvent OnWorldCreated, OnNewChunksGenerated;

    //
    public WorldData worldData { get; private set; }

    //
    public bool isWorldCreated { get; private set; }


    //
    private void Awake() {

        //
        worldData = new WorldData {

            //
            chunkHeight = this.chunkHeight,

            //
            chunkSize = this.chunkSize,

            //
            chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>(),

            //
            chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>()

            

        };

    }


    // Generate the World / Meshes
    public async void GenerateWorld() {

        //
        await GenerateWorld(Vector3Int.zero);

        /*

        ////// clear the chunk data dictionary
        chunkDataDictionary.Clear();

        ////// for each (ALL) chunk in the ChunkDataDictionary
        foreach (ChunkRenderer chunk in chunkDictionary.Values) {

            // destroy the chunks
            Destroy(chunk.gameObject);

        }

        //clear the ChunkDictionary
        chunkDictionary.Clear();

        //
        //WorldGenerationData worldGenerationData = GetStartingPosition(Vector3Int.zero);

        // for each x value in the mapSizeInChunks
        for (int x = 0; x < mapSizeInChunks; x++) {

            // for each z value in the mapSizeInChunks
            for (int z = 0; z < mapSizeInChunks; z++) {

                // generate data using new chunk data of (chunk size, ChunkHeight, this (world reference), new vector3Int position,
                // (6 x 16) (0) (6 x 16) == (X) (Y) (Z)
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, new Vector3Int(x * chunkSize, 0, z * chunkSize));

                // Generate the voxels using data
                //GenerateVoxels(data);

                // new chunk data of the terrainGenerator. call GenerateChunkData method passing through data and the mapSeedOffset
                ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);

                // add to the chunkDataDictionary (pass in new data to the dictionary^^)
                chunkDataDictionary.Add(newData.worldPosition, data);

            }

        }

        //
        foreach (var chunkData in chunkDataDictionary.Values) {

            //
            AddTreeLeaves(chunkData);

        }

        // for each data in the chunkDataDictionary values
        foreach (ChunkData data in chunkDataDictionary.Values) {

            // generate mesh data in the GetChunkMeshData method passing in data
            MeshData meshData = Chunk.GetChunkMeshData(data);

            // chunk object gameobject = instantiated chunk prefab with the data from the world position and no rotation
            GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);

            // chunk renderer = the chunk objects ChunkRenderer
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();

            // add the world position data and the chunkRenderer into the chunkDictionary
            chunkDictionary.Add(data.worldPosition, chunkRenderer);

            // initialize the chunk using the data
            chunkRenderer.InitializeChunk(data);

            // update the chunk using the mesh data
            chunkRenderer.UpdateChunk(meshData);

        }

        */

    }

    //
    private async Task GenerateWorld(Vector3Int position) {

        //
        terrainGenerator.GenerateBiomePoints(position, chunkDrawingRange, chunkSize, mapSeedOffset);

        //
        WorldGenerationData worldGenerationData = await Task.Run(() => GetPositionFromStart(position), taskTokenSource.Token);

        //
        foreach(Vector3Int pos in worldGenerationData.chunkPositionsToRemove) {

            //
            WorldDataHelper.RemoveChunk(this, pos);

        }

        //
        foreach (Vector3Int pos in worldGenerationData.chunkDataToRemove) {

            //
            WorldDataHelper.RemoveChunkData(this, pos);

        }

        //
        ConcurrentDictionary<Vector3Int, ChunkData> dataDictionary = null;

        //
        try {

            //
            dataDictionary = await CalculateWorldChunkData(worldGenerationData.chunkDataPositionsToCreate);

        }
        //
        catch (Exception) {

            //
            Debug.Log("Task Cancelled");
            return;

        }


        //
        foreach(var calculatedData in dataDictionary) {

            //
            worldData.chunkDataDictionary.Add(calculatedData.Key, calculatedData.Value);

        }

        //
        foreach (var chunkData in worldData.chunkDataDictionary.Values) {

            //
            AddTreeLeaves(chunkData);

        }

        //
        ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary = new ConcurrentDictionary<Vector3Int, MeshData>();

        //
        List<ChunkData> dataToRender = worldData.chunkDataDictionary.Where(KeyValuePair => worldGenerationData.chunkPositionsToCreate
        .Contains(KeyValuePair.Key)).Select(KeyValuePair => KeyValuePair.Value).ToList();

        //
        try {

            //
            meshDataDictionary = await CreateMeshDataAsync(dataToRender);

        }
        //
        catch (Exception) {

            //
            Debug.Log("Task Cancelled 1");

            //
            return;

        }

        //
        StartCoroutine(ChunkCreationCoroutine(meshDataDictionary));


    }


    //
    private void AddTreeLeaves(ChunkData chunkData) {

        //
        foreach (var treeLeaves in chunkData.treeData.treeLeavesSolid) {

            //
            Chunk.SetBlock(chunkData, treeLeaves, BlockType.TreeLeavesSolid);

        }

    }

    //
    private Task<ConcurrentDictionary<Vector3Int, MeshData>> CreateMeshDataAsync(List<ChunkData> dataToRender) {

        //
        ConcurrentDictionary<Vector3Int, MeshData> dictionary = new ConcurrentDictionary<Vector3Int, MeshData>();

        //
        return Task.Run(() => {

            //
            foreach (ChunkData data in dataToRender) {

                //
                if (taskTokenSource.Token.IsCancellationRequested) {

                    //
                    taskTokenSource.Token.ThrowIfCancellationRequested();

                }

                //
                MeshData meshData = Chunk.GetChunkMeshData(data);

                //
                dictionary.TryAdd(data.worldPosition, meshData);

            }

            //
            return dictionary;

        }, taskTokenSource.Token);

    }

    //
    private Task<ConcurrentDictionary<Vector3Int, ChunkData>> CalculateWorldChunkData(List<Vector3Int> chunkDataPositionsToCreate) {

        //
        ConcurrentDictionary<Vector3Int, ChunkData> dictionary = new ConcurrentDictionary<Vector3Int, ChunkData>();

        //
        return Task.Run(() => {

            //
            foreach (Vector3Int pos in chunkDataPositionsToCreate) {

                //
                if (taskTokenSource.Token.IsCancellationRequested) {

                    //
                    taskTokenSource.Token.ThrowIfCancellationRequested();

                }

                //
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, pos);

                //
                ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);

                //
                dictionary.TryAdd(pos, newData);

            }

            //
            return dictionary;

        }, taskTokenSource.Token);

    }

    //
    IEnumerator ChunkCreationCoroutine(ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary) {

        //
        foreach(var item in meshDataDictionary) {

            //
            CreateChunk(worldData, item.Key, item.Value);

            //
            yield return new WaitForEndOfFrame();

        }

        //
        if (isWorldCreated == false) {

            //
            isWorldCreated = true;

            //
            OnWorldCreated?.Invoke();

        }

    }

    //
    private void CreateChunk(WorldData worldData, Vector3Int position, MeshData meshData) {

        //
        ChunkRenderer chunkRenderer = worldRenderer.RenderChunk(worldData, position, meshData);

        //
        worldData.chunkDictionary.Add(position, chunkRenderer);

    }

    //
    internal bool SetBlock(RaycastHit hit, BlockType blockType) {

        //
        ChunkRenderer chunk = hit.collider.GetComponent<ChunkRenderer>();

        //
        if (chunk == null)

            //
            return false;

        //
        Vector3Int pos = GetBlockPos(hit);

        //
        WorldDataHelper.SetBlock(chunk.ChunkData.WorldReference, pos, blockType);

        //
        chunk.ModifiedByThePlayer = true;

        //
        if (Chunk.IsOnEdge(chunk.ChunkData, pos)) {

            //
            List<ChunkData> neighbourDataList = Chunk.GetEdgeNeighbourChunk(chunk.ChunkData, pos);

            //
            foreach (ChunkData neightbourData in neighbourDataList) {

                //
                ChunkRenderer chunkToUpdate = WorldDataHelper.GetChunk(neightbourData.WorldReference, neightbourData.worldPosition);

                //
                if (chunkToUpdate != null) {

                    //
                    chunkToUpdate.UpdateChunk();

                }

            }

        }

        //
        chunk.UpdateChunk();

        //
        return true;

    }

    //
    private Vector3Int GetBlockPos(RaycastHit hit) {

        //
        Vector3 pos = new Vector3(GetBlockPositionIn(hit.point.x, hit.normal.x), GetBlockPositionIn(hit.point.y, hit.normal.y),
            GetBlockPositionIn(hit.point.z, hit.normal.z));

        //
        return Vector3Int.RoundToInt(pos);

    }

    //
    private float GetBlockPositionIn(float pos, float normal) {

        //
        if (Mathf.Abs(pos % 1) == 0.5f) {

            //
            pos -= (normal / 2);

        }

        //
        return (float)pos;

    }

    //
    private WorldGenerationData GetPositionFromStart(Vector3Int startingPosition) {

        //
        List<Vector3Int> allChunkPositionsNeeded = WorldDataHelper.GetChunkPositionsAroundStartingPosition(this, startingPosition);

        //
        List<Vector3Int> allChunkDataPositionsNeeded = WorldDataHelper.GetDataPositionsAroundStartingPosition(this, startingPosition);



        //
        List<Vector3Int> chunkPositionsToCreate = WorldDataHelper.SelectPositionsToCreate(worldData, allChunkPositionsNeeded, 
            startingPosition);

        //
        List<Vector3Int> chunkDataPositionsToCreate = WorldDataHelper.SelectDataPositionsToCreate(worldData, 
            allChunkDataPositionsNeeded, startingPosition);



        //
        List<Vector3Int> chunkPositionsToRemove = WorldDataHelper.GetUnneededChunks(worldData, allChunkPositionsNeeded);

        //
        List<Vector3Int> chunkDataToRemove = WorldDataHelper.GetUnneededData(worldData, allChunkDataPositionsNeeded);

        //
        WorldGenerationData data = new WorldGenerationData {

            //
            chunkPositionsToCreate = chunkPositionsToCreate,

            //
            chunkDataPositionsToCreate = chunkDataPositionsToCreate,

            //
            chunkPositionsToRemove = chunkPositionsToRemove,

            //
            chunkDataToRemove = chunkDataToRemove,

            //
            chunkPositionsToUpdate = new List<Vector3Int>()

        };

        return data;

    }

    //
    //internal async void LoadAdditionalChunksRequest

    //
    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {

        // get the chunk position from the block coordinates
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);

        // access chunk data of this chunk and set it to null
        ChunkData containerChunk = null;

        // chunkDataDictionary try to get the value, if it finds the value it will output it into the containerChunk
        //chunkDataDictionary.TryGetValue(pos, out containerChunk);

        //
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

    //
    public void OnDisable() {

        //
        taskTokenSource.Cancel();

    }

}

//
public struct WorldGenerationData
{

    //
    public List<Vector3Int> chunkPositionsToCreate;

    //
    public List<Vector3Int> chunkDataPositionsToCreate;

    //
    public List<Vector3Int> chunkPositionsToRemove;

    //
    public List<Vector3Int> chunkDataToRemove;

    //
    public List<Vector3Int> chunkPositionsToUpdate;

}

//
public struct WorldData
{

    //
    public Dictionary<Vector3Int, ChunkData> chunkDataDictionary;

    //
    public Dictionary<Vector3Int, ChunkRenderer> chunkDictionary;

    //
    public int chunkSize;

    //
    public int chunkHeight;

}

/*

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class World : MonoBehaviour
{
    public int mapSizeInChunks = 6;
    public int chunkSize = 16, chunkHeight = 100;
    public int chunkDrawingRange = 8;

    public GameObject chunkPrefab;
    public WorldRenderer worldRenderer;

    public TerrainGenerator terrainGenerator;
    public Vector2Int mapSeedOffset;

    CancellationTokenSource taskTokenSource = new CancellationTokenSource();


    //public Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();
    //public Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

    public UnityEvent OnWorldCreated, OnNewChunksGenerated;

    public WorldData worldData { get; private set; }
    public bool IsWorldCreated { get; private set; }

    private void Awake() {
        worldData = new WorldData {
            chunkHeight = this.chunkHeight,
            chunkSize = this.chunkSize,
            chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>(),
            chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>()
        };
    }

    public async void GenerateWorld() {
        await GenerateWorld(Vector3Int.zero);
    }

    private async Task GenerateWorld(Vector3Int position) {
        terrainGenerator.GenerateBiomePoints(position, chunkDrawingRange, chunkSize, mapSeedOffset);

        WorldGenerationData worldGenerationData = await Task.Run(() => GetPositionsThatPlayerSees(position), taskTokenSource.Token);

        foreach (Vector3Int pos in worldGenerationData.chunkPositionsToRemove) {
            WorldDataHelper.RemoveChunk(this, pos);
        }

        foreach (Vector3Int pos in worldGenerationData.chunkDataToRemove) {
            WorldDataHelper.RemoveChunkData(this, pos);
        }


        ConcurrentDictionary<Vector3Int, ChunkData> dataDictionary = null;

        try {
            dataDictionary = await CalculateWorldChunkData(worldGenerationData.chunkDataPositionsToCreate);
        }
        catch (Exception) {
            Debug.Log("Task canceled");
            return;
        }


        foreach (var calculatedData in dataDictionary) {
            worldData.chunkDataDictionary.Add(calculatedData.Key, calculatedData.Value);
        }
        foreach (var chunkData in worldData.chunkDataDictionary.Values) {
            AddTreeLeafs(chunkData);
        }

        ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary = new ConcurrentDictionary<Vector3Int, MeshData>();

        List<ChunkData> dataToRender = worldData.chunkDataDictionary
            .Where(keyvaluepair => worldGenerationData.chunkPositionsToCreate.Contains(keyvaluepair.Key))
            .Select(keyvalpair => keyvalpair.Value)
            .ToList();

        try {
            meshDataDictionary = await CreateMeshDataAsync(dataToRender);
        }
        catch (Exception) {
            Debug.Log("Task canceled");
            return;
        }

        StartCoroutine(ChunkCreationCoroutine(meshDataDictionary));
    }

    private void AddTreeLeafs(ChunkData chunkData) {
        foreach (var treeLeafes in chunkData.treeData.treeLeavesSolid) {
            Chunk.SetBlock(chunkData, treeLeafes, BlockType.TreeLeavesSolid);
        }
    }

    private Task<ConcurrentDictionary<Vector3Int, MeshData>> CreateMeshDataAsync(List<ChunkData> dataToRender) {
        ConcurrentDictionary<Vector3Int, MeshData> dictionary = new ConcurrentDictionary<Vector3Int, MeshData>();
        return Task.Run(() => {

            foreach (ChunkData data in dataToRender) {
                if (taskTokenSource.Token.IsCancellationRequested) {
                    taskTokenSource.Token.ThrowIfCancellationRequested();
                }
                MeshData meshData = Chunk.GetChunkMeshData(data);
                dictionary.TryAdd(data.worldPosition, meshData);
            }

            return dictionary;
        }, taskTokenSource.Token
        );
    }

    private Task<ConcurrentDictionary<Vector3Int, ChunkData>> CalculateWorldChunkData(List<Vector3Int> chunkDataPositionsToCreate) {
        ConcurrentDictionary<Vector3Int, ChunkData> dictionary = new ConcurrentDictionary<Vector3Int, ChunkData>();

        return Task.Run(() => {
            foreach (Vector3Int pos in chunkDataPositionsToCreate) {
                if (taskTokenSource.Token.IsCancellationRequested) {
                    taskTokenSource.Token.ThrowIfCancellationRequested();
                }
                ChunkData data = new ChunkData(chunkSize, chunkHeight, this, pos);
                ChunkData newData = terrainGenerator.GenerateChunkData(data, mapSeedOffset);
                dictionary.TryAdd(pos, newData);
            }
            return dictionary;
        },
        taskTokenSource.Token
        );


    }

    IEnumerator ChunkCreationCoroutine(ConcurrentDictionary<Vector3Int, MeshData> meshDataDictionary) {
        foreach (var item in meshDataDictionary) {
            CreateChunk(worldData, item.Key, item.Value);
            yield return new WaitForEndOfFrame();
        }
        if (IsWorldCreated == false) {
            IsWorldCreated = true;
            OnWorldCreated?.Invoke();
        }
    }

    private void CreateChunk(WorldData worldData, Vector3Int position, MeshData meshData) {
        ChunkRenderer chunkRenderer = worldRenderer.RenderChunk(worldData, position, meshData);
        worldData.chunkDictionary.Add(position, chunkRenderer);

    }

    internal bool SetBlock(RaycastHit hit, BlockType blockType) {
        ChunkRenderer chunk = hit.collider.GetComponent<ChunkRenderer>();
        if (chunk == null)
            return false;

        Vector3Int pos = GetBlockPos(hit);

        WorldDataHelper.SetBlock(chunk.ChunkData.WorldReference, pos, blockType);
        chunk.ModifiedByThePlayer = true;

        if (Chunk.IsOnEdge(chunk.ChunkData, pos)) {
            List<ChunkData> neighbourDataList = Chunk.GetEdgeNeighbourChunk(chunk.ChunkData, pos);
            foreach (ChunkData neighbourData in neighbourDataList) {
                //neighbourData.modifiedByThePlayer = true;
                ChunkRenderer chunkToUpdate = WorldDataHelper.GetChunk(neighbourData.WorldReference, neighbourData.worldPosition);
                if (chunkToUpdate != null)
                    chunkToUpdate.UpdateChunk();
            }

        }

        chunk.UpdateChunk();
        return true;
    }

    private Vector3Int GetBlockPos(RaycastHit hit) {
        Vector3 pos = new Vector3(
             GetBlockPositionIn(hit.point.x, hit.normal.x),
             GetBlockPositionIn(hit.point.y, hit.normal.y),
             GetBlockPositionIn(hit.point.z, hit.normal.z)
             );

        return Vector3Int.RoundToInt(pos);
    }

    private float GetBlockPositionIn(float pos, float normal) {
        if (Mathf.Abs(pos % 1) == 0.5f) {
            pos -= (normal / 2);
        }


        return (float)pos;
    }


    private WorldGenerationData GetPositionsThatPlayerSees(Vector3Int playerPosition) {
        List<Vector3Int> allChunkPositionsNeeded = WorldDataHelper.GetChunkPositionsAroundPlayer(this, playerPosition);

        List<Vector3Int> allChunkDataPositionsNeeded = WorldDataHelper.GetDataPositionsAroundPlayer(this, playerPosition);

        List<Vector3Int> chunkPositionsToCreate = WorldDataHelper.SelectPositonsToCreate(worldData, allChunkPositionsNeeded, playerPosition);
        List<Vector3Int> chunkDataPositionsToCreate = WorldDataHelper.SelectDataPositonsToCreate(worldData, allChunkDataPositionsNeeded, playerPosition);

        List<Vector3Int> chunkPositionsToRemove = WorldDataHelper.GetUnnededChunks(worldData, allChunkPositionsNeeded);
        List<Vector3Int> chunkDataToRemove = WorldDataHelper.GetUnnededData(worldData, allChunkDataPositionsNeeded);

        WorldGenerationData data = new WorldGenerationData {
            chunkPositionsToCreate = chunkPositionsToCreate,
            chunkDataPositionsToCreate = chunkDataPositionsToCreate,
            chunkPositionsToRemove = chunkPositionsToRemove,
            chunkDataToRemove = chunkDataToRemove,
            chunkPositionsToUpdate = new List<Vector3Int>()
        };
        return data;

    }

    internal async void LoadAdditionalChunksRequest(GameObject player) {
        Debug.Log("Load more chunks");
        await GenerateWorld(Vector3Int.RoundToInt(player.transform.position));
        OnNewChunksGenerated?.Invoke();
    }

    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z) {
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);
        ChunkData containerChunk = null;

        worldData.chunkDataDictionary.TryGetValue(pos, out containerChunk);

        if (containerChunk == null)
            return BlockType.Nothing;
        Vector3Int blockInCHunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));
        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInCHunkCoordinates);
    }

    public void OnDisable() {
        taskTokenSource.Cancel();
    }

    public struct WorldGenerationData
    {
        public List<Vector3Int> chunkPositionsToCreate;
        public List<Vector3Int> chunkDataPositionsToCreate;
        public List<Vector3Int> chunkPositionsToRemove;
        public List<Vector3Int> chunkDataToRemove;
        public List<Vector3Int> chunkPositionsToUpdate;
    }


}
public struct WorldData
{
    public Dictionary<Vector3Int, ChunkData> chunkDataDictionary;
    public Dictionary<Vector3Int, ChunkRenderer> chunkDictionary;
    public int chunkSize;
    public int chunkHeight;
}

*/