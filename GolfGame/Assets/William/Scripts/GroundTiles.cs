using UnityEngine;
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
        tilemap.SetTile(cellPosition, null);
    }

    public void SetTile(Vector3 Pos, TileState setTo)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Pos);
        
    }
}
