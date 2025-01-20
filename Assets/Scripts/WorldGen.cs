using UnityEngine;
using static FastNoiseLite;

public class WorldGen : MonoBehaviour
{
    private int worldX;
    private int worldY;
    public GameObject[] biomes;
    public float biomeVaritaion;
    private float biomeXOrg;
    private float biomeYOrg;
    private float[,] heightMap;
    private float[,] caveMap;

    private void fillNoiseMap(float xOrigin, float yOrigin, float variation, FastNoiseLite noise, float[,] map){
        int rows = map.GetLength(0);
        int columns = map.GetLength(0);
        for(int x = 0; x < rows; x++){
            for(int y = 0; y < columns; y++){
                map[x,y] = Mathf.Clamp(noise.GetNoise(xOrigin + x*variation, yOrigin + y*variation),0,1f);
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

    private void InitBiomes(){
        for(int i = 0; i < biomes.Length; i++){
            float xO = Random.value*10000;
            float yO = Random.value*10000;
            biomes[i].GetComponent<Biome>().setOrigin(xO, yO);
        }
    }

    public void GenerateWorld(int worldX, int worldY, int worldSeed){
        FastNoiseLite heightNoise = new FastNoiseLite(worldSeed);
        FastNoiseLite caveNoise = new FastNoiseLite(worldSeed);
        heightNoise.SetNoiseType(NoiseType.OpenSimplex2);
        heightNoise.SetFractalType(FractalType.Ridged);
        heightNoise.SetFractalOctaves(10);
        heightNoise.SetDomainWarpAmp(1);

        Random.InitState(worldSeed);
        float heightXOrg = Random.value*10000;
        float heightYOrg = Random.value*10000;

        heightMap = new float[worldX,worldY];
        caveMap = new float[worldX,worldY];

        fillNoiseMap(heightXOrg, heightYOrg, 0.05f, heightNoise, heightMap);

        this.worldX = worldX;
        this.worldY = worldY;
        InitBiomes();
        PlaceBiomes();
    }
}
