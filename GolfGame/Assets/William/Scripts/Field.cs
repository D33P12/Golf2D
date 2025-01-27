using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Field : Singleton<Field>
{
    private List<GroundTiles> tileList = new List<GroundTiles>();
    public Vector3Int forTestingOnly;

    public void AddTiles(GroundTiles tileToAdd)
    {
        tileList.Add(tileToAdd);
    }

    public void BreakGrid(Vector3Int cellPosition)
    {
        /*foreach (GroundTiles tileToBreak in tileList)
        {
            tileToBreak.SeparateTiles(cellPosition);
        }*/
        Debug.Log("this should be 1" + tileList.Count);
        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].SeparateTiles(cellPosition);
        }
    }

    public void DoTest()
    {
        BreakGrid(forTestingOnly);
    }
}
