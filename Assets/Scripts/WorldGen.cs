using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using static FastNoiseLite;

public class WorldGen : MonoBehaviour
{
    private int worldX;
    private int worldY;
    public float noodleCaveHeight;
    public float largeCaveHeight;
    public WallData[] ores;
    public float[] oreHeights;
    public Tile[] oreReplacement;
    public GameObject wallManager;
    public GameObject[] biomes;
    private float[,] heightMap;
    private void fillNoiseMap(float xOrigin, float yOrigin, FastNoiseLite noise, float[,] map){
        int rows = map.GetLength(0);
        int columns = map.GetLength(0);
        for(int x = 0; x < rows; x++){
            for(int y = 0; y < columns; y++){
                map[x,y] = Mathf.Clamp(Mathf.Abs(noise.GetNoise(xOrigin + x, yOrigin + y)),0,1f);
            }
        }
    }

    private void PlaceBiomes(){
        for(int i = 0; i < worldX; i++){
            for(int j = 0; j < worldY; j++){
                biomes[Mathf.RoundToInt(heightMap[i,j]*(biomes.Length-1))].GetComponent<Biome>().GenerateTile(i,j);
            }
        }
    }

    private void DigCaves(float[,] map, float caveHeight){
        for(int i = 0; i < worldX; i++){
            for(int j = 0; j < worldY; j++){
                if(map[i,j] > caveHeight){
                    wallManager.GetComponent<WallManager>().RemoveWorldTile(i,j);
                }
            }
        }
    }

    private void InitBiomes(){
        for(int i = 0; i < biomes.Length; i++){
            float xO = Random.value*10000;
            float yO = Random.value*10000;
            biomes[i].GetComponent<Biome>().setOrigin(xO, yO);
        }
    }

    private void PlaceOre(float[,] map, WallData ore, float height, Tile replaceTile){
        for(int i = 0; i < worldX; i++){
            for(int j = 0; j < worldY; j++){
                if(map[i,j] > height){
                    if(wallManager.GetComponent<Tilemap>().GetTile(new Vector3Int(i,j,0)) == replaceTile){
                        wallManager.GetComponent<WallManager>().SetWorldTile(ore,i,j);
                    }
                }
            }
        }
    }

    public void GenerateWorld(int worldX, int worldY, int worldSeed){
        FastNoiseLite heightNoise = new FastNoiseLite(worldSeed);
        FastNoiseLite noodleCaveNoise = new FastNoiseLite(worldSeed);
        FastNoiseLite largeCaveNoise = new FastNoiseLite(worldSeed);
        FastNoiseLite oreNoise = new FastNoiseLite(worldSeed);

        heightNoise.SetNoiseType(NoiseType.OpenSimplex2S);
        heightNoise.SetFractalType(FractalType.Ridged);
        heightNoise.SetFrequency(0.001f);
        heightNoise.SetFractalOctaves(10);

        noodleCaveNoise.SetNoiseType(NoiseType.OpenSimplex2);
        noodleCaveNoise.SetFractalType(FractalType.Ridged);
        noodleCaveNoise.SetFrequency(0.01f);
        noodleCaveNoise.SetFractalOctaves(3);

        largeCaveNoise.SetNoiseType(NoiseType.OpenSimplex2S);
        largeCaveNoise.SetFractalType(FractalType.Ridged);
        largeCaveNoise.SetFrequency(0.005f);
        largeCaveNoise.SetFractalOctaves(10);

        oreNoise.SetNoiseType(NoiseType.OpenSimplex2S);
        oreNoise.SetFractalType(FractalType.Ridged);
        oreNoise.SetFrequency(0.01f);
        oreNoise.SetFractalOctaves(10);

        

        float[,] noodleCaveMap;
        float[,] largeCaveMap;

        Random.InitState(worldSeed);
        float heightXOrg = Random.value*10000;
        float heightYOrg = Random.value*10000;
        float noodleCaveXOrg = Random.value*10000;
        float noodleCaveYOrg = Random.value*10000;
        float largeCaveXOrg = Random.value*10000;
        float largeCaveYOrg = Random.value*10000;

        heightMap = new float[worldX,worldY];
        noodleCaveMap = new float[worldX,worldY];
        largeCaveMap = new float[worldX,worldY];

        fillNoiseMap(heightXOrg, heightYOrg, heightNoise, heightMap);
        fillNoiseMap(noodleCaveXOrg, noodleCaveYOrg, noodleCaveNoise, noodleCaveMap);
        fillNoiseMap(largeCaveXOrg, largeCaveYOrg, largeCaveNoise, largeCaveMap);

        this.worldX = worldX;
        this.worldY = worldY;
        InitBiomes();
        PlaceBiomes();
        DigCaves(noodleCaveMap, noodleCaveHeight);
        DigCaves(largeCaveMap, largeCaveHeight);
        for(int i = 0; i < ores.Length; i++){
            float[,] oreMap = new float[worldX,worldY];
            float yOrg = Random.value * worldY;
            float xOrg = Random.value * worldX;
            fillNoiseMap(xOrg, yOrg, oreNoise, oreMap);
            PlaceOre(oreMap, ores[i], oreHeights[i], oreReplacement[i]);
        }
    }
}
