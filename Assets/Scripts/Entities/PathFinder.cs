using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    float time = 1;
    public Tile searchPath;
    public Tile finalPath;
    public Tilemap tilemap;
    public int endx;
    public int endy;
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0){
            bool[,] grid = GameObject.Find("WallManager").GetComponent<WallManager>().walkable;
            PrintPath(grid, new Vector2Int(1,1) ,new Vector2Int(endx,endy));
            time = 1;
        }
    }
    void PrintPath(bool[,] grid, Vector2Int start, Vector2Int goal)
    {

        var path = BFS(grid, start, goal);

        if (path != null)
        {
            Debug.Log("Path:");
            foreach (var step in path)
            {
                tilemap.SetTile(new Vector3Int(step.x, step.y, 0), finalPath);
                Debug.Log($"({step.x}, {step.y})");
            }
        }
        else
        {
            Debug.Log("No path found.");
        }
    }

        List<Vector2Int> BFS(bool[,] grid, Vector2Int start, Vector2Int end)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        Debug.Log(rows + " " + cols);
        var queue = new Queue<Vector2Int>();
        var visited = new HashSet<Vector2Int>();
        var parent = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);
        parent[start] = start;

        Vector2Int[] directions = {
            new Vector2Int(0,1), new Vector2Int(1,0), new Vector2Int(-1,0), new Vector2Int(0,-1)
        };

        while (queue.Count > 0)
        {
            Debug.Log(queue.Count);
            var current = queue.Dequeue();
            if (current.x == end.x && current.y == end.y)
            {
                Debug.Log("found path constuction");
                return ConstructPath(parent, start, end);
            }

            foreach (var direction in directions)
            {
                int newX = current.x + direction.x;
                int newY = current.y + direction.y;
                var neighbor = new Vector2Int(newX, newY);

                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols && !visited.Contains(neighbor) && grid[newX, newY])
                {
                    tilemap.SetTile(new Vector3Int(neighbor.x, neighbor.y, 0), searchPath);
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    parent[neighbor] = current;
                }
            }
        }

        return null;
    }

        static List<Vector2Int> ConstructPath(Dictionary<Vector2Int, Vector2Int> parent, Vector2Int start, Vector2Int end)
    {
        var path = new List<Vector2Int>();
        for (var current = end; current != start; current = parent[current])
        {
            path.Add(current);
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    public List<Vector2Int> AStar(bool[,] grid, Vector2Int start, Vector2Int goal)
    {
        var openSet = new SortedSet<(int, Vector2Int)>(new Comparer());
        openSet.Add((0, start));

        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, int> { { start, 0 } };
        var fScore = new Dictionary<Vector2Int, int> { { start, Heuristic(start, goal) } };

        while (openSet.Count > 0)
        {
            var current = openSet.Min.Item2;
            openSet.Remove(openSet.Min);

            if (current == goal)
            {
                var path = new List<Vector2Int>();
                while (cameFrom.ContainsKey(current))
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Add(start);
                path.Reverse();
                return path;
            }

            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                var neighbor = new Vector2Int(current.x + dx, current.y + dy);

                if (neighbor.x >= 0 && neighbor.x < grid.GetLength(0) && neighbor.y >= 0 && neighbor.y < grid.GetLength(1) && grid[neighbor.x, neighbor.y])
                {
                    var tentativeGScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = tentativeGScore + Heuristic(neighbor, goal);

                        openSet.Add((fScore[neighbor], neighbor));
                    }
                }
            }
        }

        return null;
    }

    private static int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private class Comparer : IComparer<(int, Vector2Int)>
    {
        public int Compare((int, Vector2Int) x, (int, Vector2Int) y)
        {
            var result = x.Item1.CompareTo(y.Item1);
            return result != 0 ? result : (x.Item2.x.CompareTo(y.Item2.x) != 0 ? x.Item2.x.CompareTo(y.Item2.x) : x.Item2.y.CompareTo(y.Item2.y));
        }
    }
}