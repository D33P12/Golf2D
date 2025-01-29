using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public enum TileState //not yet used for another feature later
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
    public int secondsToLock = 5;
    private Transform fieldContainerTransform;
    private Rigidbody2D rb_self;
    [SerializeField] GameObject emptyGridPrefab;
    

    private void Start()
    {
        fieldContainerTransform = Field.Instance.gameObject.transform;
        Field.Instance.AddTiles(this.gameObject);
        tilemap = this.gameObject.GetComponent<Tilemap>();
        rb_self = this.gameObject.GetComponent<Rigidbody2D>();
        TurnDynamicThenStatic();
    }

    public void DeleteTile(Vector3 Pos)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
       // Debug.Log("Converted To:" + cellPosition);
        //Debug.Log(tilemap);
        tilemap.SetTile(cellPosition, null);
    }

    public void SetTile(Vector3 Pos, TileState setTo)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
    }

    public void StartTest(Vector3Int Pos)
    {
        SeparateTiles(Pos);
    }

    public void SeparateTiles(Vector3Int Pos) //BFSearch for all the tiles
    {
        var resultPosition = new List<Vector3Int>();
        var resultTile = new List<TileBase>();
        Stack<Vector3Int> toVisit = new Stack<Vector3Int>();

        tilemap = this.gameObject.GetComponent<Tilemap>();
        if (tilemap.GetTile(Pos) != null)
        {
            Vector3Int firstPos = Pos;
            resultPosition.Add(firstPos);
            resultTile.Add(tilemap.GetTile(Pos));
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.left);
            toVisit.Push(firstPos + Vector3Int.left + Vector3Int.down);
            toVisit.Push(firstPos + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.down);
            toVisit.Push(firstPos + Vector3Int.right + Vector3Int.up);
            toVisit.Push(firstPos + Vector3Int.right);
            toVisit.Push(firstPos + Vector3Int.right + Vector3Int.down);
        }
        
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
        if (resultPosition.Count != 0)
        {
            /*var separatedGrid = new GameObject("Grid").AddComponent<Grid>();
            separatedGrid.cellSize = new Vector3(1, 1, 0);
            separatedGrid.transform.SetParent(fieldContainerTransform);

            var separatedTilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
            separatedTilemap.AddComponent<TilemapRenderer>();
            

            separatedTilemap.gameObject.layer = LayerMask.NameToLayer("Ground");
            separatedTilemap.transform.SetParent(separatedGrid.gameObject.transform);
            separatedTilemap.tileAnchor = new Vector3(0, 1, 0);
            */

            GameObject separatedGrid = Instantiate(emptyGridPrefab, Vector3.zero, Quaternion.identity);
            var separatedTilemap = separatedGrid.GetComponentInChildren<Tilemap>();

            separatedTilemap.ClearAllTiles();

            for (int i = 0; i < resultPosition.Count; i++)
            {
                separatedTilemap.SetTile(resultPosition[i], resultTile[i]);
                tilemap.SetTile(resultPosition[i], null);
            }
            /*var separatedRigidbody2D = separatedTilemap.AddComponent<Rigidbody2D>();
            separatedTilemap.AddComponent<GroundTiles>();
            separatedTilemap.AddComponent<TilemapCollider2D>();*/
            //var separatedRigidbody2D = separatedTilemap.gameObject.GetComponent<Rigidbody2D>();
            //rb_self.bodyType = RigidbodyType2D.Dynamic;



            //StartCoroutine(LockInPiece(separatedRigidbody2D));
            //StartCoroutine(LockInPiece(rb_self));
        }

        
        
        
    }

    public void TurnDynamicThenStatic()
    {
        rb_self.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(LockInPiece(rb_self));
    }

    IEnumerator LockInPiece(Rigidbody2D toLock)
    {
        yield return new WaitForSeconds(secondsToLock);
        toLock.bodyType = RigidbodyType2D.Static;
        
    }
   
}
