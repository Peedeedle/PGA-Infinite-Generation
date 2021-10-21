////////////////////////////////////////////////////////////
// File: ChunkRenderer.cs
// Author: Jack Peedle
// Date Created: 21/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 21/10/21
// Brief: 
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
    MeshFilter meshFilter;

    // Reference to the mesh collider
    MeshCollider meshCollider;

    // our mesh object 
    Mesh mesh;

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
        meshFilter = GetComponent<MeshFilter>();

        // Get the mesh collider component
        meshCollider = GetComponent<MeshCollider>();

        // Get the mesh from the mesh filter mesh
        mesh = meshFilter.mesh;

    }

    // initialize chunk using void as can't us contructor as it is monobehaviour
    public void InitializeChunk (ChunkData data) {

        // this chunk data = data
        this.ChunkData = data;

    }

    // Render the mesh
    private void RenderMesh (MeshData meshData) {

        // Clear the current mesh
        mesh.Clear();

        // Different material for each submesh (good for water)
        mesh.subMeshCount = 2;

        // set vertices = vertices in mesh data, add the vertices from water mesh and the mesh data (Concatenate)
        mesh.vertices = meshData.lVertices.Concat(meshData.waterMesh.lVertices).ToArray();

        // Each submesh needs it's own triangles set seperately, set the mesh data (index of 0) triangles 
        mesh.SetTriangles(meshData.iTriangles.ToArray(), 0);

        // set the water mesh triangles, set the value to the mesh data vertices count to array, with a index of 1
        mesh.SetTriangles(meshData.waterMesh.iTriangles.Select(val => val + meshData.lVertices.Count).ToArray(), 1);

        // get the mesh data from the uv and the water mesh uv data and convert them to an array
        mesh.uv = meshData.uv.Concat(meshData.waterMesh.uv).ToArray();

        // to have correct calculation of light on mesh, recalculate normals
        mesh.RecalculateNormals();

        // Create collider
        // set the mesh colliders to not shared
        meshCollider.sharedMesh = null;

        // new collision mesh
        Mesh collisionMesh = new Mesh();

        // set the collision mesh vertices and set to collider vertices (lColliderVertices are creating the collider)
        collisionMesh.vertices = meshData.lColliderVertices.ToArray();

        // set the collision mesh triangles and set to collider triangles
        // (lColliderTriangles are creating the collider(no water here because water will not be a collider))
        collisionMesh.triangles = meshData.iColliderTriangles.ToArray();

        // Recalculate collision mesh normals
        collisionMesh.RecalculateNormals();

        // Mesh collider = collision mesh generated in line 116
        meshCollider.sharedMesh = collisionMesh;

    }

    // Update chunk
    public void UpdateChunk() {

        // get the chunk mesh data and render the mesh
        RenderMesh(Chunk.GetChunkMeshData(ChunkData));

    }

    // Update chunk and take mesh data
    public void UpdateChunk(MeshData data) {
        
        // render mesh using data
        RenderMesh(data);

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
