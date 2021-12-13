////////////////////////////////////////////////////////////
// File: BiomeGenerator.cs
// Author: Jack Peedle
// Date Created: 30/10/21
// Last Edited By: Jack Peedle
// Date Last Edited: 13/12/21
// Brief: Generate the biomes
//////////////////////////////////////////////////////////// 


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{

    // int for the water threshold (water surface at specific height)
    public int waterThreshold = 50;

    // reference to the noise settings called biomeNoiseSettings
    public NoiseSettings biomeNoiseSettings;

    // rreference to the domain warping script 
    public DomainWarping domainWarping;

    // bool for using the domain warping or not
    public bool useDomainWarping = true;

    // blockLayerHandler called startLayerHandler
    public BlockLayerHandler startLayerHandler;

    // reference to the normal tree generator
    public TreeGenerator treeGenerator;

    // reference to the cactus generator
    public CactusGenerator cactusGenerator;

    // reference to the snow tree generator
    public SnowTreeGenerator snowTreeGenerator;

    // reference to the presents generator
    public PresentsGenerator presentsGenerator;

    // reference to the jungle tree generator
    public JungleTreeGenerator jungleTreeGenerator;

    // reference to the lillypad generator
    public LilyPadGenerator lilyPadGenerator;

    // reference to the sugarcane generator
    public SugarCaneGenerator sugarCaneGenerator;

    // reference to the cursed tree generator
    public CursedTreeGenerator cursedTreeGenerator;

    // reference to the pumpkin generator
    public PumpkinGenerator pumpkinGenerator;

    // reference to the tomato generator
    public TomatoGenerator tomatoGenerator;

    // reference to the red mushroom tree generator
    public RMushroomTreeGenerator rMushroomTreeGenerator;

    // reference to the white mushroom tree generator
    public WMushroomTreeGenerator wMushroomTreeGenerator;

    // reference to the blue bush generator
    public BlueBushGenerator blueBushGenerator;

    // reference to the green bush generator
    public GreenBushGenerator greenBushGenerator;

    // reference to the light blue bush generator
    public LightBlueBushGenerator lightBlueBushGenerator;

    // reference to the purple bush generator
    public PurpleBushGenerator purpleBushGenerator;

    // reference to the red bush generator
    public RedBushGenerator redBushGenerator;

    // reference to the yellow bush generator
    public YellowBushGenerator yellowBushGenerator;

    // reference to the melon generator
    public MelonGenerator melonGenerator;

    // reference to the cola cube generator
    public ColaCubeGenerator colaCubeGenerator;

    // reference to the grape cube generator
    public GrapeCubeGenerator grapeCubeGenerator;

    // reference to the orange cube generator
    public OrangeCubeGenerator orangeCubeGenerator;

    // reference to the pineapple cube generator
    public PineappleCubeGenerator pineappleCubeGenerator;

    // reference to the smore generator
    public SmoreGenerator smoreGenerator;

    // reference to the redcane generator
    public RedCaneGenerator redCaneGenerator;

    // reference to the greencane generator
    public GreenCaneGenerator greenCaneGenerator;

    // reference to the candy tree generator
    public CandyTreeGenerator candyTreeGenerator;


    // Get the tree data
    internal TreeData GetTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if tree generator = null (Don't generate trees)
        if (treeGenerator == null) 

            // return tree data values which are 0
            return new TreeData();

        // return generate tree with data and map seed offset for positions
        return treeGenerator.GenerateTreeData(data, mapSeedOffset);

    }

    // Get the cactus data
    internal CactusData GetCactusData(ChunkData data, Vector2Int mapSeedOffset) {

        // if cactus generator = null (Don't generate cactuses)
        if (cactusGenerator == null)

            // return cactus data values which are 0
            return new CactusData();

        // return generate cactus with data and map seed offset for positions
        return cactusGenerator.GenerateCactusData(data, mapSeedOffset);

    }

    //  get the snow tree data
    internal SnowTreeData GetSnowTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if snow tree generator = null (Don't generate snow tree)
        if (snowTreeGenerator == null)

            // return snow tree data values which are 0
            return new SnowTreeData();

        // return generate snow tree with data and map seed offset for positions
        return snowTreeGenerator.GenerateSnowTreeData(data, mapSeedOffset);

    }

    //  get the presents data
    internal PresentsData GetPresentsData(ChunkData data, Vector2Int mapSeedOffset) {

        // if presents generator = null (Don't generate presents)
        if (presentsGenerator == null)

            // return presents data values which are 0
            return new PresentsData();

        // return generate presents with data and map seed offset for positions
        return presentsGenerator.GeneratePresentsData(data, mapSeedOffset);

    }

    //  get the jungle tree data
    internal JungleTreeData GetJungleTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if jungle tree generator = null (Don't generate jungle tree)
        if (jungleTreeGenerator == null)

            // return jungle tree data values which are 0
            return new JungleTreeData();

        // return generate jungle tree with data and map seed offset for positions
        return jungleTreeGenerator.GenerateJungleTreeData(data, mapSeedOffset);

    }

    //  get the lillypad data
    internal LilyPadData GetLilyPadData(ChunkData data, Vector2Int mapSeedOffset) {

        // if lillypad generator = null (Don't generate lillypad)
        if (lilyPadGenerator == null)

            // return lillypad data values which are 0
            return new LilyPadData();

        // return generate lillypad with data and map seed offset for positions
        return lilyPadGenerator.GenerateLilyPadData(data, mapSeedOffset);

    }

    //  get the sugarcane data
    internal SugarCaneData GetSugarCaneData(ChunkData data, Vector2Int mapSeedOffset) {

        // if sugarcane generator = null (Don't generate sugarcane)
        if (sugarCaneGenerator == null)

            // return sugarcane data values which are 0
            return new SugarCaneData();

        // return generate sugarcane with data and map seed offset for positions
        return sugarCaneGenerator.GenerateSugarCaneData(data, mapSeedOffset);

    }

    //  get the cursed tree data
    internal CursedTreeData GetCursedTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if cursed tree generator = null (Don't generate cursed tree)
        if (cursedTreeGenerator == null)

            // return cursed tree data values which are 0
            return new CursedTreeData();

        // return generate cursed tree with data and map seed offset for positions
        return cursedTreeGenerator.GenerateCursedTreeData(data, mapSeedOffset);

    }

    //  get the pumpkin data
    internal PumpkinData GetPumpkinData(ChunkData data, Vector2Int mapSeedOffset) {

        // if pumpkin generator = null (Don't generate pumpkin)
        if (pumpkinGenerator == null)

            // return pumpkin data values which are 0
            return new PumpkinData();

        // return generate pumpkin with data and map seed offset for positions
        return pumpkinGenerator.GeneratePumpkinData(data, mapSeedOffset);

    }

    //  get the tomato data
    internal TomatoData GetTomatoData(ChunkData data, Vector2Int mapSeedOffset) {

        // if tomato generator = null (Don't generate tomato)
        if (tomatoGenerator == null)

            // return tomato data values which are 0 
            return new TomatoData();

        // return generate tomato with data and map seed offset for positions
        return tomatoGenerator.GenerateTomatoData(data, mapSeedOffset);

    }

    //  get the red mushroom tree data
    internal RMushroomTreeData GetRMushroomTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if red mushroom tree generator = null (Don't generate red mushroom tree)
        if (rMushroomTreeGenerator == null)

            // return red mushroom tree data values which are 0
            return new RMushroomTreeData();

        // return generate red mushroom tree with data and map seed offset for positions
        return rMushroomTreeGenerator.GenerateRMushroomTreeData(data, mapSeedOffset);

    }

    //  get the white mushroom tree data
    internal WMushroomTreeData GetWMushroomTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if white mushroom tree generator = null (Don't generate white mushroom tree)
        if (wMushroomTreeGenerator == null)

            // return white mushroom tree data values which are 0
            return new WMushroomTreeData();

        // return generate white mushroom tree with data and map seed offset for positions
        return wMushroomTreeGenerator.GenerateWMushroomTreeData(data, mapSeedOffset);

    }

    //  get the blue bush data
    internal BlueBushData GetBlueBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if blue bush generator = null (Don't generate blue bush)
        if (blueBushGenerator == null)

            // return blue bush data values which are 0
            return new BlueBushData();

        // return generate bluebush with data and map seed offset for positions
        return blueBushGenerator.GenerateBlueBushData(data, mapSeedOffset);

    }

    //  get the green bush data
    internal GreenBushData GetGreenBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if green bush generator = null (Don't generate green bush)
        if (greenBushGenerator == null)

            // return green bush data values which are 0
            return new GreenBushData();

        // return generate greenbush with data and map seed offset for positions
        return greenBushGenerator.GenerateGreenBushData(data, mapSeedOffset);

    }

    //  get the light blue bush data
    internal LightBlueBushData GetLightBlueBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if light blue bush generator = null (Don't generate light blue bush)
        if (lightBlueBushGenerator == null)

            // return light blue bush data values which are 0
            return new LightBlueBushData();

        // return generate lightbluebush with data and map seed offset for positions
        return lightBlueBushGenerator.GenerateLightBlueBushData(data, mapSeedOffset);

    }

    //  get the purple bush data
    internal PurpleBushData GetPurpleBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if purple bush generator = null (Don't generate purple bush)
        if (purpleBushGenerator == null)

            // return purple bush data values which are 0
            return new PurpleBushData();

        // return generate purplebush with data and map seed offset for positions
        return purpleBushGenerator.GeneratePurpleBushData(data, mapSeedOffset);

    }

    //  get the red bush data
    internal RedBushData GetRedBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if red bush generator = null (Don't generate red bush)
        if (redBushGenerator == null)

            // return red bush data values which are 0
            return new RedBushData();

        // return generate redbush with data and map seed offset for positions
        return redBushGenerator.GenerateRedBushData(data, mapSeedOffset);

    }

    //  get the yellow bush data
    internal YellowBushData GetYellowBushData(ChunkData data, Vector2Int mapSeedOffset) {

        // if yellow bush generator = null (Don't generate yellow bush)
        if (yellowBushGenerator == null)

            // return yellow bush data values which are 0
            return new YellowBushData();

        // return generate yellowbush with data and map seed offset for positions
        return yellowBushGenerator.GenerateYellowBushData(data, mapSeedOffset);

    }

    //  get the melon data
    internal MelonData GetMelonData(ChunkData data, Vector2Int mapSeedOffset) {

        // if melon generator = null (Don't generate melon)
        if (melonGenerator == null)

            // return melon data values which are 0
            return new MelonData();

        // return generate melon with data and map seed offset for positions
        return melonGenerator.GenerateMelonData(data, mapSeedOffset);

    }

    //  get the cola cube data
    internal ColaCubeData GetColaCubeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if cola cube generator = null (Don't generate cola cube)
        if (colaCubeGenerator == null)

            // return cola cube data values which are 0
            return new ColaCubeData();

        // return generate colacube with data and map seed offset for positions
        return colaCubeGenerator.GenerateColaCubeData(data, mapSeedOffset);

    }

    //  get the grape cube data
    internal GrapeCubeData GetGrapeCubeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if grape cube generator = null (Don't generate grape cube)
        if (grapeCubeGenerator == null)

            // return grape cube data values which are 0
            return new GrapeCubeData();

        // return generate grapecube with data and map seed offset for positions
        return grapeCubeGenerator.GenerateGrapeCubeData(data, mapSeedOffset);

    }

    //  get the orange cube data
    internal OrangeCubeData GetOrangeCubeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if orange cube generator = null (Don't generate orange cube)
        if (orangeCubeGenerator == null)

            // return orange cube data values which are 0
            return new OrangeCubeData();

        // return generate orangecube with data and map seed offset for positions
        return orangeCubeGenerator.GenerateOrangeCubeData(data, mapSeedOffset);

    }

    //  get the pineapple cube data
    internal PineappleCubeData GetPineappleCubeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if pineapple cube generator = null (Don't generate pineapple cube)
        if (pineappleCubeGenerator == null)

            // return pineapple cube data values which are 0
            return new PineappleCubeData();

        // return generate pineapplecube with data and map seed offset for positions
        return pineappleCubeGenerator.GeneratePineappleCubeData(data, mapSeedOffset);

    }

    //  get the smore data
    internal SmoreData GetSmoreData(ChunkData data, Vector2Int mapSeedOffset) {

        // if smore generator = null (Don't generate smore)
        if (smoreGenerator == null)

            // return smore data values which are 0
            return new SmoreData();

        // return generate smore with data and map seed offset for positions
        return smoreGenerator.GenerateSmoreData(data, mapSeedOffset);

    }

    //  get the red cane data
    internal RedCaneData GetRedCaneData(ChunkData data, Vector2Int mapSeedOffset) {

        // if red cane generator = null (Don't generate red cane)
        if (redCaneGenerator == null)

            // return red cane data values which are 0
            return new RedCaneData();

        // return generate redcane with data and map seed offset for positions
        return redCaneGenerator.GenerateRedCaneData(data, mapSeedOffset);

    }

    //  get the green cane data
    internal GreenCaneData GetGreenCaneData(ChunkData data, Vector2Int mapSeedOffset) {

        // if green cane generator = null (Don't generate green cane)
        if (greenCaneGenerator == null)

            // return green cane data values which are 0
            return new GreenCaneData();

        // return generate greencane with data and map seed offset for positions
        return greenCaneGenerator.GenerateGreenCaneData(data, mapSeedOffset);

    }

    //  get the candy tree data
    internal CandyTreeData GetCandyTreeData(ChunkData data, Vector2Int mapSeedOffset) {

        // if candy tree generator = null (Don't generate candy tree)
        if (candyTreeGenerator == null)

            // return candy tree data values which are 0
            return new CandyTreeData();

        // return generate candy tree with data and map seed offset for positions
        return candyTreeGenerator.GenerateCandyTreeData(data, mapSeedOffset);

    }

    // list of the blocklayerhandlers called additionLayerHandlers
    public List<BlockLayerHandler> additionalLayerHandlers;

    // Generate the chunk data using the data and a int for the x, z and a vector 2 for the mapSeedOffset
    public ChunkData ProcessChunkColumn(ChunkData data, int x, int z, Vector2Int mapSeedOffset, int? terrainHeightNoise) {

        // noise settings world offset = mapSeedOffset
        biomeNoiseSettings.worldOffset = mapSeedOffset;

        // int for ground position where dirt and grass will be generated, above = air, below = water etc
        int groundPosition;

        // if terrain height noise has value = false
        if(terrainHeightNoise.HasValue == false) {

            // ground position = surface height noise (X, Y and Z)
            groundPosition = GetSurfaceHeightNoise(data.worldPosition.x + x, data.worldPosition.z + z, data.chunkHeight);

        }
        else {

            // ground position = terrain height noise value
            groundPosition = terrainHeightNoise.Value;

        }

        // look for each z local coordinate from 0 - chunkHeight
        // for chunks above the ground
        for (int y = data.worldPosition.y; y < data.worldPosition.y + data.chunkHeight; y++){ //0; y < data.chunkHeight; y++) {

            //start handling the layers with the inputs
            startLayerHandler.Handle(data, x, y, z, groundPosition, mapSeedOffset);

        }

        // for each layer in additionalLayersHandlers
        foreach (var layer in additionalLayerHandlers) {

            // Handle the layer, pass through data, x position, data.worldPosition.y, z position, groundPosition and mapSeedOffset 
            layer.Handle(data, x, data.worldPosition.y, z, groundPosition, mapSeedOffset);

        }

        // Return the data
        return data;

    }

    // get the surface height noise using ints for x, z and the chunk height for y
    public int GetSurfaceHeightNoise(int x, int z, int chunkHeight) {

        // float terrainHeight
        float terrainHeight;

        // if use domain warping is false
        if (useDomainWarping == false) {

            // float for the terrain height = MyNoise script.OctavePerlin (Amplitude + frequency etc)
            // and x,z and biomenoise settings for y
            terrainHeight = MyNoise.OctavePerlin(x, z, biomeNoiseSettings);

        } else {

            // float for the terrain height = domain warping generateDomainNoise using the x, z, and biome noise settings
            terrainHeight = domainWarping.GenerateDomainNoise(x, z, biomeNoiseSettings);

        }

        // terrain height = redistribution passing in the terrain height and the noise settings
        terrainHeight = MyNoise.Redistribution(terrainHeight, biomeNoiseSettings);

        // surface height = remapped value int from 0-1 passing in the terrainHeight(x), 0(z), chunkheight(y)
        int surfaceHeight = (int)MyNoise.RemapValue01ToInt(terrainHeight, 0, chunkHeight);

        // return the surface height
        return surfaceHeight;

    }

}
