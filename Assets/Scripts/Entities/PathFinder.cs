using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
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

            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1), (1,1), (-1,-1), (-1,1), (1,-1) })
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