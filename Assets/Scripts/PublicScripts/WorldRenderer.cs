////////////////////////////////////////////////////////////
// File: WorldRenderer.cs
// Author: Jack Peedle
// Date Created: 08/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/11/21
// Brief: Render the chunks to create the "World"
//////////////////////////////////////////////////////////// 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{

    // chunkPrefab gameobject reference
    public GameObject chunkPrefab;

    // queue <component chunk renderer> chunk pool as a new queue <component chunk renderer>
    public Queue<ChunkRenderer> chunkPool = new Queue<ChunkRenderer>();

    // clear passing in the world data
    public void Clear(WorldData worldData) {

        // for each item in chunkDictionary values
        foreach (var item in worldData.chunkDictionary.Values) {

            // destroy the item.Gameobject
            Destroy(item.gameObject);

        }

        // clear the chunk pool
        chunkPool.Clear();
    }


    // render chunk passing in the world data, position and meshData
    internal ChunkRenderer RenderChunk(WorldData worldData, Vector3Int position, MeshData meshData) {

        // new chunk == null
        ChunkRenderer newChunk = null;

        // if chunk pool count is more than 0
        if (chunkPool.Count > 0) {

            // new chunk = chunk pool deQueue
            newChunk = chunkPool.Dequeue();

            // new chunk position = position
            newChunk.transform.position = position;

        } else {

            // chunk object = instantiated object as a chunk prefab, at the position with Quaternion.identity rotation
            GameObject chunkObject = Instantiate(chunkPrefab, position, Quaternion.identity);

            // new chunk = chunk object ChunkRenderer
            newChunk = chunkObject.GetComponent<ChunkRenderer>();
        }

        // new chunk initializeChunk passing in the chunkDataDictionary position array
        newChunk.InitializeChunk(worldData.chunkDataDictionary[position]);

        // update the new chunk passing through the meshData
        newChunk.UpdateChunk(meshData);

        // set the new chunk gameobject to true
        newChunk.gameObject.SetActive(true);

        // return the new chunk
        return newChunk;
    }

    // remove chunk in the chunk renderer
    public void RemoveChunk(ChunkRenderer chunk) {

        // set the chunk gameobject to false
        chunk.gameObject.SetActive(false);

        // enQueue the chunk in the chunk pool
        chunkPool.Enqueue(chunk);
    }
}