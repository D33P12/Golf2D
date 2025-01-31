using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public class Field : Singleton<Field>
{
    private List<GameObject> tileList = new List<GameObject>();
    public Vector3Int firstTileInMap;
    [SerializeField] private RecordOverlappedTiles recordingMoleObject;
    [SerializeField] private GameObject playerGolfBallObject;

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
        DropGridFromMole();
    }

    public void DropGrid(List<Vector3Int> list)
    {
        foreach (Vector3Int gridLocation in list)
        {
            BreakGrid(gridLocation);
        }
        
    }

    public void DropGridFromMole()
    { 
        DropGrid(recordingMoleObject.GetRecord());
        //Debug.Log(recordingMoleObject.GetRecord()[0]);
        recordingMoleObject.clearRecord();
        TurnAllTilesDynamicThenStatic();
    }

    public void TurnAllTilesDynamicThenStatic()
    {

        for (int i = 0; i < tileList.Count; i++)
        {
            tileList[i].GetComponent<GroundTiles>().TurnDynamicThenStatic();
        }
    }
}
