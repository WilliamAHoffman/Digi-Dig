using UnityEngine;
using UnityEngine.Tilemaps;

public class WallManager : MonoBehaviour
{
    public Tilemap walls;
    private int[,] wallHealth;
    private int[,] wallDrop;
    public bool[,] walkable;

    public void hit(int damage, Vector3 pos){
        Vector3Int tilePoint = walls.WorldToCell(pos);
        if(walls.GetTile(tilePoint) != null){
            if(wallHealth[tilePoint.x,tilePoint.y] != -1){
                wallHealth[tilePoint.x,tilePoint.y] -= damage;
                if(wallHealth[tilePoint.x,tilePoint.y] <= 0){
                    walls.SetTile(tilePoint, null);
                    walkable[(int)pos.x, (int)pos.y] = true;
                }
            }
        }
    }

    public void setWorld(int x, int y){
        wallHealth = new int[x,y];
        wallDrop = new int[x,y];
        walkable = new bool[x,y];

        for(int i = 0; i < x; i++){
            for(int j = 0; j < y; j++){
                wallHealth[i,j] = 0;
                wallDrop[i,j] = 0;
                walkable[i,j] = true;
            }
        }
        
    }
    public void SetWorldTile(WallData tileData, int x, int y){
        walls.SetTile(new Vector3Int(x, y, 0), tileData.tile);
        wallHealth[x,y] = tileData.health;
        wallDrop[x,y] = tileData.drop;
        walkable[x,y] = false;
    }
    
    public void RemoveWorldTile(int x, int y){
        walls.SetTile(new Vector3Int(x, y, 0), null);
        wallHealth[x,y] = 0;
        walkable[x,y] = true;
    }
}
