using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Tilemaps;

public enum TileState
{
    Empty,
    RegularFull,
    RegularLT,
    RegularRT,
    RegularLB,
    RegularRB,
    SandFull,
    SandLT,
    SandRT,
    SandLB,
    SandRB
};
public class GroundTiles : MonoBehaviour
{
    public Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void DeleteTile(Vector3 Pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
        Debug.Log("Converted To:" + cellPosition);
        tilemap.SetTile(cellPosition, null);
    }

    public void SetTile(Vector3 Pos, TileState setTo)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
        
    }

    public void SeparateTiles(Vector3 Pos) //BFSearch for all the tiles
    {
        var resultPosition = new List<Vector3Int>();
        var resultTile = new List<TileBase>();

        Stack<Vector3Int> toVisit = new Stack<Vector3Int>();
        

        if (tilemap.WorldToCell(Pos) != null)
        {
            Vector3Int firstPos = tilemap.WorldToCell(Pos);
            resultPosition.Add(firstPos);
            resultTile.Add(tilemap.GetTile(tilemap.WorldToCell(Pos)));
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
        }

        
        while (toVisit.Count > 0)
        {
            Vector3Int visitingPosition = toVisit.Pop();
            if (tilemap.WorldToCell(visitingPosition) != null)
            {
                resultPosition.Add(tilemap.WorldToCell(Pos));
                resultTile.Add(tilemap.GetTile(tilemap.WorldToCell(Pos)));

            }
            else {
                
            }
        }
    }

    //credit to @dmgregory for this code, https://gamedev.stackexchange.com/questions/195065/how-to-separate-tilemaps-unity
    /*public List<List<Vector3Int>> FindConnectedGroups(Tilemap world)
    {
        var visited = new List<Vector3Int>();
        var dirs = new List<Vector3Int> { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };
        var groups = new List<List<Vector3Int>>();
        for (int x = world.cellBounds.xMin; x < world.cellBounds.xMax; x++)
        {
            for (int y = world.cellBounds.yMin; y < world.cellBounds.yMax; y++)
            {
                var group = new List<Vector3Int>();
                var visit = new List<Vector3Int>();
                var tile = new Vector3Int(x, y, 0);
                if (!visited.Contains(tile))
                {
                    visit.Add(tile);
                    for (int z = 0; z < visit.Count; z++)
                    {
                        var t = visit[z];
                        if (!visited.Contains(t) && world.GetTile(t))
                        {
                            visited.Add(t);
                            group.Add(t);
                            foreach (var d in dirs)
                            {
                                if (!visited.Contains(t + d))
                                {
                                    visit.Add(t + d);
                                }
                            }
                        }
                        else if (!world.GetTile(t))
                        {
                            visit.Remove(t);
                        }
                    }
                }
                if (group.Count > 0)
                {
                    groups.Add(group);
                }
            }
        }
        visited.Clear();
        return groups;

    }*/

}
