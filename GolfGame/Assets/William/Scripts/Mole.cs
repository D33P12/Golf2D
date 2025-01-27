using UnityEngine;

public class Mole : MonoBehaviour
{

    [SerializeField] private GameObject golfBallRef;
    [SerializeField] private GameObject golfHoleRef;
    [SerializeField] private float maxDigHeight;
    [SerializeField] private float minDigHeight;
    [SerializeField] private float minDigX;
    [SerializeField] private float maxDigX;
    [SerializeField] private Transform moleTransform;

    //temp values
    [SerializeField] private float randomTempX;
    [SerializeField] private float randomTempY;
    [SerializeField] private Vector3 randomVector;

    [SerializeField] Vector3 DigPointA;
    [SerializeField] Vector3 DigPointB;
    [SerializeField] Vector3 PointAHeightDepth; //these are the two extra points for bezier curve.
    [SerializeField] Vector3 PointBHeightDepth;

    [SerializeField] private LayerMask inPlatform;


    public void SetDiggingPoints()
    {
        //rules: points picked must not be below golfball directly, and be in front of golf hole
        //lets do the digging down first, so I need a point that is a tile with a null above. avoiding the golfball

        randomTempX = Random.Range(minDigX, maxDigX);

        while (Mathf.Abs(randomTempX - golfBallRef.transform.position.x) < 2 && (randomTempX < golfHoleRef.transform.position.x - 2|| randomTempX > golfHoleRef.transform.position.x + 2))
        {
            randomTempX = Random.Range(minDigX, maxDigX);
        }

        randomVector = new Vector3(randomTempX,maxDigHeight,0);
        randomTempY = maxDigHeight;
        Collider2D overCollider2D = Physics2D.OverlapCircle(randomVector, 0.01f, inPlatform);
        while (overCollider2D == null)
        {
            randomTempY--;
            randomVector = new Vector3(randomTempX, randomTempY, 0);
            overCollider2D = Physics2D.OverlapCircle(randomVector, 0.01f, inPlatform);
        }

        DigPointA = randomVector;
        moleTransform.position = DigPointA;
    }
    public void DiggingPrepRoute()
    { 
        //draw circle or bezier curve

    }

    public void StartDigging()
    { 
        //move mole transform
    }
}
