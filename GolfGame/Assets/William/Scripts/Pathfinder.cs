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

    [SerializeField] private bool diggingUp;

    private bool finishedRunning;
    private bool tooClose;

    private Rigidbody2D pathfinderRB;
    private void Awake()
    {
        pathfinderRB = this.gameObject.GetComponent<Rigidbody2D>();
        tooClose = false;
    }

    public void Pathfind()
    {
        //rules: points picked must not be below golfball directly, and be in front of golf hole
        //lets do the digging down first, so I need a point that is a tile with a null above. avoiding the golfball
        diggingUp = Random.value < 0.5f;

        finishedRunning = false;

        randomTempX = Random.Range(minDigX, maxDigX);

        if (diggingUp)
        {
            pathfinderRB.gravityScale = -1;
            tooClose = (Mathf.Abs(golfHoleRef.transform.position.x - maxDigXB) < 5f);
            if (tooClose)
            {
                randomTempX = Random.Range(minDigX, golfBallRef.transform.position.x);
            }
        }
        else
        {
            pathfinderRB.gravityScale = 1;
        }

        /*while (Mathf.Abs(randomTempX - golfBallRef.transform.position.x) < 2 && (randomTempX < golfHoleRef.transform.position.x - 2 || randomTempX > golfHoleRef.transform.position.x + 2))
        {
            randomTempX = Random.Range(minDigX, maxDigX);
        }*/
        /*while ((randomTempX < golfHoleRef.transform.position.x - 2 || randomTempX > golfHoleRef.transform.position.x + 2))
        {
            randomTempX = Random.Range(minDigX, maxDigX);
        }*/
        randomVector = new Vector3(randomTempX, maxDigHeight, 0);
        randomTempY = maxDigHeight;


        randomVector = new Vector3(randomTempX, randomTempY, 0);

        

        pathfinderTransform.position = randomVector;

        StartCoroutine(RecordLocation((pathPoint) =>
        {
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
        randomTempX = Random.Range(minDigX, maxDigXB);

        if (diggingUp && tooClose)
        {
            randomTempX = Random.Range(minDigX, golfBallRef.transform.position.x);
        }

        if (pathfindPointAA.x < golfBallRef.transform.position.x && randomTempX > golfBallRef.transform.position.x) 
        {
            randomTempX = Random.Range(minDigX, pathfindPointAA.x);
        }

        /*while (Mathf.Abs(randomTempX - golfBallRef.transform.position.x) < 2 || Mathf.Abs(randomTempX - golfHoleRef.transform.position.x) < 3)
        {
            randomTempX = Random.Range(pathfindPointAA.x + 2f, maxDigXB);
        }*/

        //randomVector = new Vector3(randomTempX, maxDigHeight, 0);
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
        //do a check here instead
        if (pathfindPointBA.x < pathfindPointAA.x)
        {
            Vector3 tempVector3 = new Vector3(pathfindPointAA.x, pathfindPointAA.y, 0);
            pathfindPointAA = new Vector3(pathfindPointBA.x, pathfindPointBA.y, 0);
            pathfindPointBA = new Vector3(tempVector3.x, tempVector3.y, 0);
        }

        randomTempX = Random.Range(4, maxDigHeight);

        if (diggingUp)
        {
            pathfindPointAB = new Vector3(pathfindPointAA.x + 2f, pathfindPointAA.y + randomTempX, 0);
            PathfindPointBB = new Vector3(pathfindPointBA.x + 2f, pathfindPointBA.y + randomTempX, 0);
        }
        else
        {
            pathfindPointAB = new Vector3(pathfindPointAA.x + 2f, pathfindPointAA.y - randomTempX, 0);
            PathfindPointBB = new Vector3(pathfindPointBA.x + 2f, pathfindPointBA.y - randomTempX, 0);
        }

        output.SetPoints(pathfindPointAA, pathfindPointAB, pathfindPointBA, PathfindPointBB);
        finishedRunning = true;

    }

    public bool isDiggingUp()
    {
        return diggingUp;
    }
    public bool FinishedRunning()
    { 
        return finishedRunning;
    }
}
