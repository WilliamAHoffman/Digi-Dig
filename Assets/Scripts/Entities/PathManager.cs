using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathManager : MonoBehaviour
{
    public float updatePathTime = 1;
    public Tile pathTile;
    private PathFinder pathFinder;
    private PathFollower pathFollower;
    private Agression agression;
    private List<Vector2Int> currentPath;
    private Tilemap tilemap;
    private float updatePathTimer;
    bool[,] walkableGrid;

    void Start()
    {
        pathFinder = gameObject.GetComponent<PathFinder>();
        pathFollower = gameObject.GetComponent<PathFollower>();
        agression = gameObject.GetComponentInParent<Agression>();
        updatePathTimer = 0;
        tilemap = GameObject.Find("Floors").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agression.currentTarget != null)
        {
            updatePathTimer -= Time.deltaTime;
            if(updatePathTimer <= 0){
                Debug.Log("looking for path");
                updatePathTimer = updatePathTime;
                walkableGrid = GameObject.Find("WallManager").GetComponent<WallManager>().walkable;
                currentPath = pathFinder.AStar(walkableGrid,
                    new Vector2Int((int)transform.position.y,(int)transform.position.y),
                    new Vector2Int((int)agression.currentTarget.transform.position.x,
                    (int)agression.currentTarget.transform.position.y));
                PrintPath(currentPath);
            }
        }
    }

    void PrintPath(List<Vector2Int> path)
    {
        if (path != null)
        {
            Debug.Log("Path:");
            foreach (var step in path)
            {
                tilemap.SetTile(new Vector3Int(step.x, step.y, 0), pathTile);
                Debug.Log($"({step.x}, {step.y})");
            }
        }
        else
        {
            Debug.Log("No path found.");
        }
    }
}
