using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

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
    public Vector3Int forTestingOnly;
    public int secondsToLock;

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

    public void StartTest()
    {
        SeparateTiles(forTestingOnly);
    }
    public void SeparateTiles(Vector3Int Pos) //BFSearch for all the tiles
    {
        var resultPosition = new List<Vector3Int>();
        var resultTile = new List<TileBase>();

        Stack<Vector3Int> toVisit = new Stack<Vector3Int>();
        

        if (tilemap.GetTile(Pos) != null)
        {
            Vector3Int firstPos = Pos;
            resultPosition.Add(firstPos);
            resultTile.Add(tilemap.GetTile(tilemap.WorldToCell(Pos)));
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.down);
            toVisit.Push(firstPos + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.down);
            toVisit.Push(firstPos + Vector3Int.right + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.right);
            toVisit.Push(firstPos + Vector3Int.right + Vector3Int.down);
        }
        Debug.Log(toVisit.Count);
        
        while (toVisit.Count > 0)
        {
            Vector3Int visitingPosition = toVisit.Pop();
            if (tilemap.GetTile(visitingPosition) != null && !resultPosition.Contains(visitingPosition))
            {
                resultPosition.Add(visitingPosition);
                resultTile.Add(tilemap.GetTile(visitingPosition));

                toVisit.Push(visitingPosition + Vector3Int.left + Vector3Int.up);
                toVisit.Push(visitingPosition + Vector3Int.left);
                toVisit.Push(visitingPosition + Vector3Int.left + Vector3Int.down);
                toVisit.Push(visitingPosition + Vector3Int.up);
                toVisit.Push(visitingPosition + Vector3Int.down);
                toVisit.Push(visitingPosition + Vector3Int.right + Vector3Int.up);
                toVisit.Push(visitingPosition + Vector3Int.right);
                toVisit.Push(visitingPosition + Vector3Int.right + Vector3Int.down);
            }
        }

        //so if I do this right and it does not infinite loop, result position and tile should have them
        var separatedGrid = new GameObject("Grid").AddComponent<Grid>();
        var separatedTilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        separatedTilemap.AddComponent<TilemapRenderer>();
        separatedTilemap.AddComponent<TilemapCollider2D>();
        separatedTilemap.transform.SetParent(separatedGrid.gameObject.transform);
        separatedTilemap.tileAnchor = new Vector3(0, 1, 0);

        for(int i = 0; i < resultPosition.Count; i++)
        {
            tilemap.SetTile(resultPosition[i], null);
            separatedTilemap.SetTile(resultPosition[i], resultTile[i]);
        }
        var separatedRigidbody2D = separatedTilemap.AddComponent<Rigidbody2D>();
        StartCoroutine(LockInPiece(separatedRigidbody2D));
    }

    IEnumerator LockInPiece(Rigidbody2D toLock)
    {
        yield return new WaitForSeconds(secondsToLock);
        toLock.bodyType = RigidbodyType2D.Static;

    }
   
}
