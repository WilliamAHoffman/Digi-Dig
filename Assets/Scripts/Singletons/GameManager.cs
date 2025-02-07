using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject WorldGen;
    public GameObject wallManager;
    public GameObject player;
    public int worldX;
    public int worldY;
    public int Seed = 0;
    private int RandomRange = 200000;

    // Start is called before the first frame update
    void Start()
    {
        if(Seed == 0){
            Seed = (int)(Random.value*RandomRange);
        }
        wallManager.GetComponent<WallManager>().setWorld(worldX,worldY);
        WorldGen.GetComponent<WorldGen>().GenerateWorld(worldX, worldY, Seed);
        //SpawnPlayer();
    }

    void SpawnPlayer(){
        int randomX = (int)(Random.value * worldX);
        int randomY = (int)(Random.value * worldY);
        Vector3Int randomSpawn = new Vector3Int(randomX,randomY,0);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x, randomSpawn.y);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x+1, randomSpawn.y);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x, randomSpawn.y+1);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x+1, randomSpawn.y+1);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x-1, randomSpawn.y);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x, randomSpawn.y-1);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x-1, randomSpawn.y-1);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x-1, randomSpawn.y+1);
        wallManager.GetComponent<WallManager>().RemoveWorldTile(randomSpawn.x+1, randomSpawn.y-1);
        player.transform.position = randomSpawn;
    }
}
