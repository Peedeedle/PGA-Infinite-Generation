////////////////////////////////////////////////////////////
// File: BlockHelper.cs
// Author: Jack Peedle
// Date Created: 25/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 12/11/21
// Brief: 
//////////////////////////////////////////////////////////// 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class BlockHelper
{

    // private static array of directions
    private static Direction[] directions = {

        // direction for backwards
        Direction.backwards,

        // direction for down
        Direction.down,

        // direction for forward
        Direction.forward,

        // direction for left
        Direction.left,

        // direction for right
        Direction.right,

        // direction for up
        Direction.up

    };

    // method for getting the mesh data from chunk data, chunk data x,y,z + mesh data and block type
    public static MeshData GetMeshData (ChunkData chunk, int x, int y, int z, MeshData meshData, BlockType blockType) {

        // if the block type is air or nothing (do not render those blocks)
        if (blockType == BlockType.Air || blockType == BlockType.Nothing) {

            // Retun the meshData
            return meshData;

        }

            

        // look for each direction
        foreach (Direction direction in directions) {

            // position of the neighbour block
            var neighbourBlockCoordinates = new Vector3Int(x, y, z) + direction.GetVector();

            // block type for the neighbouring block
            var neighbourBlockType = Chunk.GetBlockFromChunkCoordinates(chunk, neighbourBlockCoordinates);

            // if should generate face in the direction or not (do not generate faces that are not visible) and there is or isn't
            // a solid block next to it (air or nothing) and should generate that face on the side neighbouring the air / nothing
            if (neighbourBlockType != BlockType.Nothing && BlockDataManager.blockTextureDataDictionary
                [neighbourBlockType].isSolid == false) {

                // if the rednered block type is water
                if (blockType == BlockType.Water) {

                    // if the water is a neighbout of the air
                    if (neighbourBlockType == BlockType.Air) {

                        // generate face on the water mesh
                        meshData.waterMesh = GetFaceDataIn(direction, chunk, x, y, z, meshData.waterMesh, blockType);

                    }

                        

                } else {

                    // generate face using GetFaceDataIn as a new meshData, 
                    meshData = GetFaceDataIn(direction, chunk, x, y, z, meshData, blockType);

                }

            }

        }

        // return the meshData
        return meshData;

    }

    // getFaceDataIn pass in, direction, chunkdata, x,y,z coordinates, meshData and blockType
    public static MeshData GetFaceDataIn(Direction direction, ChunkData chunk, int x, int y, int z, MeshData meshData, BlockType blockType) {

        // Get the faceVertices from mesh data and block type
        GetFaceVertices(direction, x, y, z, meshData, blockType);

        // add quad triangles and the bool generatesColliders
        meshData.AddQuadTriangles(BlockDataManager.blockTextureDataDictionary[blockType].generatesCollider);

        // add UV's using FaceUVs method
        meshData.uv.AddRange(FaceUVs(direction, blockType));

        // Return the meshData
        return meshData;

    }

    // get the vertices per face of voxel, direction, x,y,z of coordinates, take mesh data and block type
    public static void GetFaceVertices(Direction direction, int x, int y, int z, MeshData meshData, BlockType blockType) {

        // variable for generates collider which is gathered from the block data manager
        var generatesCollider = BlockDataManager.blockTextureDataDictionary[blockType].generatesCollider;

        // switch method taking in the direction
        switch (direction) {

            // backwards direction
            case Direction.backwards:

                // define the back side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the vertecies and generating a collider on that point
                // For all 4 corners of that side
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);

                // break the switch statement
                break;


            // forward direction
            case Direction.forward:

                // define the front side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the verticies and generating a collider on that point
                // For all 4 corners of that side

                // Bottom Left
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // Top Left
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Top right
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Bottom right
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // break the switch statement
                break;


            // left direction
            case Direction.left:

                // define the left side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the vertecies and generating a collider on that point
                // For all 4 corners of that side

                // Bottom Left
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // Top Left
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Top right
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);

                // Bottom right
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);

                // break the switch statement
                break;


            // right direction
            case Direction.right:

                // define the right side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the vertecies and generating a collider on that point
                // For all 4 corners of that side

                // Bottom Left
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);

                // Top Left
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);

                // Top right
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Bottom right
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // break the switch statement
                break;


            // down direction
            case Direction.down:

                // define the down side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the vertecies and generating a collider on that point
                // For all 4 corners of that side

                // Bottom Left
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), generatesCollider);

                // Top Left
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), generatesCollider);

                // Top right
                meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // Bottom right
                meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), generatesCollider);

                // break the switch statement
                break;


            // up direction
            case Direction.up:

                // define the up side of the voxel by taking the centre point and finding the corners of the cube
                // then adding the vertecies and generating a collider on that point
                // For all 4 corners of that side

                // Bottom Left
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Top Left
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), generatesCollider);

                // Top right
                meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), generatesCollider);

                // Bottom right
                meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), generatesCollider);

                // break the switch statement
                break;

            // if doesn't meet conditions set it to default statement (break out)
            default:

                // break the statement
                break;

        }

    }

    // vector 2 array for the direction and the block type UVs
    public static Vector2[] FaceUVs(Direction direction, BlockType blockType) {

        // create a new vector 2 UV with 4 vertices
        Vector2[] UVs = new Vector2[4];

        // tile position = texture position taking in direction and block type
        var tilePos = TexturePosition(direction, blockType);

        // new vector 2 taking in the tile size * tile position texture
        // - offset of the texture to avoid errors of computers storing float values (do the same for the Y)
        UVs[0] = new Vector2(BlockDataManager.tileSizeX * tilePos.x + BlockDataManager.tileSizeX - BlockDataManager.textureOffset,
            BlockDataManager.tileSizeY * tilePos.y + BlockDataManager.textureOffset);

        // new vector 2 taking in the tile size * tile position texture
        // - offset of the texture to avoid errors of computers storing float values (do the same for the Y)
        UVs[1] = new Vector2(BlockDataManager.tileSizeX * tilePos.x + BlockDataManager.tileSizeX - BlockDataManager.textureOffset,
            BlockDataManager.tileSizeY * tilePos.y + BlockDataManager.tileSizeY - BlockDataManager.textureOffset);

        // new vector 2 taking in the tile size * tile position texture
        // - offset of the texture to avoid errors of computers storing float values (do the same for the Y)
        UVs[2] = new Vector2(BlockDataManager.tileSizeX * tilePos.x + BlockDataManager.textureOffset,
            BlockDataManager.tileSizeY * tilePos.y + BlockDataManager.tileSizeY - BlockDataManager.textureOffset);

        // new vector 2 taking in the tile size * tile position texture
        // - offset of the texture to avoid errors of computers storing float values (do the same for the Y)
        UVs[3] = new Vector2(BlockDataManager.tileSizeX * tilePos.x + BlockDataManager.textureOffset,
            BlockDataManager.tileSizeY * tilePos.y + BlockDataManager.textureOffset);

        // Return the UVs
        return UVs;

    }

    // generate texture position, take in the direction and the block type
    public static Vector2Int TexturePosition(Direction direction, BlockType blockType) {

        // return a direction
        return direction switch {

            // direction up, switch, provide up texture of block type
            Direction.up => BlockDataManager.blockTextureDataDictionary[blockType].up,

            // direction down, switch, provide down texture of block type
            Direction.down => BlockDataManager.blockTextureDataDictionary[blockType].down,

            // any other case (left,right etc) return the side texture
            _ => BlockDataManager.blockTextureDataDictionary[blockType].side

        };


    }


}
