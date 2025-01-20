using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Biome : MonoBehaviour
{
    public Tilemap floorMap;
    public GameObject wallManager;
    public Tile[] floors;
    public TileData[] walls;
    public string biome;
    public float variation;
    private float xOrigin;
    private float yOrigin;
    public void GenerateTile(int x, int y){
        //Debug.Log("Biome: " + biome + ", Height: " + height + ", Max: " + maxHeight + " at: " + x + " , " + y);
        if(floors.Length > 0){
            int floorIndex = (int)Math.Round(getHeight(x, y)*(floors.Length-1));
            floorMap.SetTile(new Vector3Int(x, y, 0), floors[floorIndex]);
        }
        if(walls.Length > 0){
            int wallIndex = (int)Math.Round(getHeight(x, y)*(walls.Length-1));
            wallManager.GetComponent<WallManager>().SetWorldTile(walls[wallIndex],x,y);
        }
    }

    private float getHeight(int x, int y){
        float sample = Mathf.PerlinNoise(xOrigin + (x*variation),yOrigin + (y*variation));
        sample = Mathf.Clamp(sample, 0, 0.99f);
        return sample;
    }

    public void setOrigin(float xOrigin, float yOrigin){
        this.xOrigin = xOrigin;
        this.yOrigin = yOrigin;
    }
}
