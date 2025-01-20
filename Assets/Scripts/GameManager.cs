using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WorldGen;
    public GameObject wallManager;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
