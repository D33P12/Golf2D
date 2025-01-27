using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Field : Singleton<Field>
{
    private List<GameObject> tileList = new List<GameObject>();
    public Vector3Int forTestingOnly;

    public void AddTiles(GameObject tileToAdd)
    {
        tileList.Add(tileToAdd);
    }

    public void BreakGrid(Vector3Int cellPosition)
    {
        Debug.Log(tileList.Count);
        for (int i = 0; i < tileList.Count; i++)
        {
           tileList[i].GetComponent<GroundTiles>().StartTest(cellPosition);
        }
    }

    public void DoTest()
    {
        BreakGrid(forTestingOnly);
    }
}
