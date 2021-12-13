////////////////////////////////////////////////////////////
// File: ChunkRenderer.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 28/11/21
// Brief: render the chunks, verts etc, also show gizmos of chunks and biomes
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

// Require component Mesh filter
[RequireComponent(typeof(MeshFilter))]

// Require component Mesh renderer
[RequireComponent(typeof(MeshRenderer))]

// Require component Mesh collider
[RequireComponent(typeof(MeshCollider))]

// if Chunkrenderer (this) is attached to a gameobject and does not have the required components
// It will add the required components to that gameobject

public class ChunkRenderer : MonoBehaviour
{

    // Reference to the mesh filter
    MeshFilter meshFilter1;

    // Reference to the mesh collider
    MeshCollider meshCollider1;

    // our mesh object 
    Mesh mesh1;

    // bool to show size of chunk
    public bool showGizmos = false;

    // Reference to the chunk data property
    public ChunkData ChunkData { get; private set; }


    // bool for if the player modifies chunks
    public bool ModifiedByThePlayer {

        // get
        get {

            // return the chunk data modified by the player
            return ChunkData.modifiedByThePlayer;

        }
        // set
        set {

            // a value to the modified by player in chunk data
            ChunkData.modifiedByThePlayer = value;

        }

    }

    // On awake
    private void Awake() {

        // Get the mesh filter component
        meshFilter1 = GetComponent<MeshFilter>();

        // Get the mesh collider component
        meshCollider1 = GetComponent<MeshCollider>();

        // Get the mesh from the mesh filter mesh
        mesh1 = meshFilter1.mesh;

    }

    // initialize chunk using void as can't us contructor as it is monobehaviour
    public void InitializeChunk (ChunkData data) {

        // this chunk data = data
        this.ChunkData = data;


    }

    // Render the mesh
    private void RenderMeshNormal (MeshData meshData1) {

        // Clear the current mesh
        mesh1.Clear();

        // Different material for each submesh (good for water)
        mesh1.subMeshCount = 2;

        // set vertices = vertices in mesh data, add the vertices from water mesh and the mesh data (Concatenate)
        mesh1.vertices = meshData1.lVertices.Concat(meshData1.waterMesh.lVertices).ToArray();

        // Each submesh needs it's own triangles set seperately, set the mesh data (index of 0) triangles 
        mesh1.SetTriangles(meshData1.iTriangles.ToArray(), 0);

        // set the water mesh triangles, set the value to the mesh data vertices count to array, with a index of 1
        mesh1.SetTriangles(meshData1.waterMesh.iTriangles.Select(val => val + meshData1.lVertices.Count).ToArray(), 1);

        // get the mesh data from the uv and the water mesh uv data and convert them to an array
        mesh1.uv = meshData1.uv.Concat(meshData1.waterMesh.uv).ToArray();

        // to have correct calculation of light on mesh, recalculate normals
        mesh1.RecalculateNormals();

        // Create collider
        // set the mesh colliders to not shared
        meshCollider1.sharedMesh = null;

        // new collision mesh
        Mesh collisionMesh1 = new Mesh();

        // set the collision mesh vertices and set to collider vertices (lColliderVertices are creating the collider)
        collisionMesh1.vertices = meshData1.lColliderVertices.ToArray();

        // set the collision mesh triangles and set to collider triangles
        // (lColliderTriangles are creating the collider(no water here because water will not be a collider))
        collisionMesh1.triangles = meshData1.iColliderTriangles.ToArray();

        // Recalculate collision mesh normals
        collisionMesh1.RecalculateNormals();

        // Mesh collider = collision mesh generated in line 116
        meshCollider1.sharedMesh = collisionMesh1;

    }

    // Update chunk
    public void UpdateChunk() {

        // get the chunk mesh data and render the mesh
        RenderMeshNormal(Chunk.GetChunkMeshData(ChunkData));

    }

    // Update chunk and take mesh data
    public void UpdateChunk(MeshData data) {
        
        // render mesh using data
        RenderMeshNormal(data);

    }


#if UNITY_EDITOR

    // Show Gizmos
    private void OnDrawGizmos() {
        
        // if Show gizmos is true
        if (showGizmos) {

            // if the application is currently playing and chunk data is not = to null
            if (Application.isPlaying && ChunkData != null) {

                // if selected gameobject is active
                if (Selection.activeObject == gameObject)

                    // set colour to green
                    Gizmos.color = new Color(0, 1, 0, 0.4f);

                else

                    // set colour to magenta to see gizmo
                    Gizmos.color = new Color(1, 0, 1, 0.4f);

                // Draw the cube, set the alpha value to 0.4f so the cube gizmo is transparent, transform.position of the chunk
                // renderer. new vector 3 is the centre of the chunk, second vector 3 is setting the size of the chunk
                Gizmos.DrawCube(transform.position + new Vector3(ChunkData.chunkSize / 2f, ChunkData.chunkHeight / 2f,
                    ChunkData.chunkSize / 2f), new Vector3(ChunkData.chunkSize, ChunkData.chunkHeight, ChunkData.chunkSize));


            }

        }

    }

#endif
}
