////////////////////////////////////////////////////////////
// File: MeshData.cs
// Author: Jack Peedle
// Date Created: 20/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 20/10/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{

    // list of vector 3 for vertices, add new vertices (not all chunks the same)
    public List<Vector3> lVertices = new List<Vector3>();

    // list of ints for triangles, convert to instantiate within world
    public List<int> iTriangles = new List<int>();

    // list of vector 2's for UV, store co-ordinates for textures
    public List<Vector2> uv = new List<Vector2>();


    // list of vector 3 colliderVertices, to tell the mesh which verts are colliders
    public List<Vector3> lColliderVertices = new List<Vector3>();

    // list of vector 3 colliderTriangles, to tell the mesh which triangles are colliders
    public List<int> iColliderTriangles = new List<int>();

    // Represent water mesh, different shader and material
    public MeshData waterMesh;

    // if the main mesh is true
    private bool bIsMainMesh = true;


    //public mesh data for is main mesh
    public MeshData(bool BIsMainMesh) {

        // if main mesh is true
        if (BIsMainMesh) {

            // water mesh = new mesh data that isn't the main mesh
            waterMesh = new MeshData(false);

        }

    }


    // Add vertexes using vertecies list, position, bool vertexGeneratesCollider
    // (if block is air or water then don't generate collider)
    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider) {

        // Add vertices using vertex
        lVertices.Add(vertex);

        // if the bool is true
        if (vertexGeneratesCollider) {

            // Add vertex to collider
            lColliderVertices.Add(vertex);
        }

    }


    // add triangles for the collider or not
    public void AddQuadTriangles(bool quadGeneratesCollider) {

        // Calculate and add in clockwise order, create side visible to player
        // 4 vertices 0,1,2,3. add triangle count 4 - 4 = 0
        //                     add triangle count 4 - 3 = 1
        //                     add triangle count 4 - 2 = 2
        //
        //  [1]------------[2]                           
        //   |            . |                   
        //   |          .   |                    
        //   |        .     |                   
        //   |      .       |                   
        //   |    .         |                     
        //   |  .           |                        
        //  [0]------------[3] 


        //  [1]------------[2]                           
        //   |            .                    
        //   |          .                       
        //   |        .                         
        //   |      .                          
        //   |    .                              
        //   |  .                                   
        //  [0]
        //  
        // Count of vertices - 4 [0]
        iTriangles.Add(lVertices.Count - 4);

        // Count of vertices - 3 [1]
        iTriangles.Add(lVertices.Count - 3);

        // Count of vertices - 2 [2]
        iTriangles.Add(lVertices.Count - 2);


        //                 [2]                           
        //                . |                   
        //              .   |                    
        //            .     |                   
        //          .       |                   
        //        .         |                     
        //     .            |                        
        //  [0]------------[3] 
        //
        // add triangle count 4 - 4 = 0
        // add triangle count 4 - 2 = 2
        // add triangle count 4 - 1 = 3
        //
        // Count of vertices - 4
        iTriangles.Add(lVertices.Count - 4);

        // Count of vertices - 4
        iTriangles.Add(lVertices.Count - 2);

        // Count of vertices - 4
        iTriangles.Add(lVertices.Count - 1);


        // if quadGeneratesCollider is true
        if (quadGeneratesCollider) {

            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 4);

            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 3);

            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 2);



            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 4);

            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 2);

            // Add collider triangles to the vertices. count
            iColliderTriangles.Add(lColliderVertices.Count - 1);

        }

    }


}
