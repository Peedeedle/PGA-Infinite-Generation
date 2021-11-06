////////////////////////////////////////////////////////////
// File: StoneLayerHandler.cs
// Author: Jack Peedle
// Date Created: 02/11/21
// Last Edited By: Jack Peedle
// Date Last Edited: 02/11/21
// Brief: 
//////////////////////////////////////////////////////////// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLayerHandler : BlockLayerHandler
{

    // Range between 0 and 1 for the stone threshold
    [Range(0, 1)]
    public float stoneThreshold = 0.5f;

    // private noise settings reference which is called the stone noise settings
    [SerializeField]
    private NoiseSettings stoneNoiseSettings;

    //
    public DomainWarping domainWarping;

    //
    protected override bool TryHandling(ChunkData chunkData, int x, int y, int z, int surfaceHeightNoise, Vector2Int mapSeedOffset) {

        // if the chunk data is greater than the surface height (do not change air blocks into stone blocks at that height)
        if (chunkData.worldPosition.y > surfaceHeightNoise) {

            // return false
            return false;

        }

        //^^
             // New lines
        //vv

        // noise settings world offset = map seed offset
        stoneNoiseSettings.worldOffset = mapSeedOffset;

        // terrain height = octave perlin with the chunk data x and z positions and the stone noise settings (new noise for stone layer)
        float stoneNoise = MyNoise.OctavePerlin(chunkData.worldPosition.x + x, chunkData.worldPosition.z + z, stoneNoiseSettings);

        //
        //float stoneNoise = domainWarping.GenerateDomainNoise(chunkData.worldPosition.x + x, chunkData.worldPosition.z + z, stoneNoiseSettings);

        // int for the end position = suface height noise (height of terrain)
        int endPosition = surfaceHeightNoise;

        // if chunk data.world position.Y is less than 0
        if (chunkData.worldPosition.y < 0) {

            // end position = chunkdata.worldposition.y + chunk data. height (set all chunks below terrain to be stone)
            endPosition = chunkData.worldPosition.y + chunkData.chunkHeight;

        }

        // if stone noise is greater than the stone threshold
        if (stoneNoise > stoneThreshold) {

            // i = chunk data world position.y, i is less than or equal to the end position, i++
            for (int i = chunkData.worldPosition.y; i <= endPosition; i++) {

                // new vector 3 position passing in x, i and z
                Vector3Int pos = new Vector3Int(x, i, z);

                // set the block for this position using the block data to a stone block
                Chunk.SetBlock(chunkData, pos, BlockType.Stone);

            }

            // return true
            return true;

        }

        // return true
        return false;

    }

}
