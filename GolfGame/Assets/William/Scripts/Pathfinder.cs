using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private GameObject golfBallRef;
    [SerializeField] private GameObject golfHoleRef;
    [SerializeField] private float maxDigHeight;
    //[SerializeField] private float minDigHeight;
    [SerializeField] private float minDigX;
    [SerializeField] private float maxDigX;
    [SerializeField] private float maxDigXB;
    [SerializeField] private Transform pathfinderTransform;

    //temp values
    [SerializeField] private float randomTempX;
    [SerializeField] private float randomTempY;
    [SerializeField] private Vector3 randomVector;

    [SerializeField] private Vector3 pathfindPointAA;
    [SerializeField] private Vector3 pathfindPointAB;
    [SerializeField] private Vector3 pathfindPointBA;
    [SerializeField] private Vector3 PathfindPointBB;

    [SerializeField] private Mole output;

    private bool finishedRunning;

    public void Pathfind()
    {
        //rules: points picked must not be below golfball directly, and be in front of golf hole
        //lets do the digging down first, so I need a point that is a tile with a null above. avoiding the golfball
        finishedRunning = false;
        randomTempX = Random.Range(minDigX, maxDigX);

        /*while (Mathf.Abs(randomTempX - golfBallRef.transform.position.x) < 2 && (randomTempX < golfHoleRef.transform.position.x - 2 || randomTempX > golfHoleRef.transform.position.x + 2))
        {
            randomTempX = Random.Range(minDigX, maxDigX);
        }*/
        while ((randomTempX < golfHoleRef.transform.position.x - 2 || randomTempX > golfHoleRef.transform.position.x + 2))
        {
            randomTempX = Random.Range(minDigX, maxDigX);
        }
        randomVector = new Vector3(randomTempX, maxDigHeight, 0);
        randomTempY = maxDigHeight;
        

        randomVector = new Vector3(randomTempX, randomTempY, 0);
        
        pathfinderTransform.position = randomVector;

        StartCoroutine(RecordLocation((pathPoint)=> {
            pathfindPointAA = pathPoint;
            PathFindPointB();
        }));

        
    }

    IEnumerator RecordLocation(System.Action<Vector3> callback)
    {
        yield return new WaitForSeconds(3);
        callback(pathfinderTransform.position);

    }

    public void PathFindPointB()
    {
        randomTempX = Random.Range(pathfindPointAA.x + 2f, maxDigXB);

        while (Mathf.Abs(randomTempX - golfBallRef.transform.position.x) < 2 || Mathf.Abs(randomTempX - golfHoleRef.transform.position.x) < 3)
        {
            randomTempX = Random.Range(pathfindPointAA.x + 2f, maxDigXB);
        }

        randomVector = new Vector3(randomTempX, maxDigHeight, 0);
        randomTempY = maxDigHeight;


        randomVector = new Vector3(randomTempX, randomTempY, 0);

        pathfinderTransform.position = randomVector;

        StartCoroutine(RecordLocation((pathPoint) => {
            pathfindPointBA = pathPoint;
            CalculateRemainPoints();
        }));

    }

    public void CalculateRemainPoints()
    {

        randomTempX = Random.Range(4, maxDigHeight);
        pathfindPointAB = new Vector3(pathfindPointAA.x +2f, pathfindPointAA.y - randomTempX, 0);
        PathfindPointBB = new Vector3(pathfindPointBA.x +2f, pathfindPointBA.y - randomTempX, 0);
        output.SetPoints(pathfindPointAA, pathfindPointAB, pathfindPointBA, PathfindPointBB);
        finishedRunning = true;

    }

    public bool FinishedRunning()
    { 
        return finishedRunning;
    }
}
