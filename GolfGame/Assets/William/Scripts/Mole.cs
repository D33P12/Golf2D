using UnityEngine;

public class Mole : MonoBehaviour
{

    /*[SerializeField] private GameObject golfBallRef;
    [SerializeField] private GameObject golfHoleRef;
    [SerializeField] private float maxDigHeight;
    [SerializeField] private float minDigHeight;
    [SerializeField] private float minDigX;
    [SerializeField] private float maxDigX;*/
    [SerializeField] private Transform moleTransform;

    /*//temp values
    [SerializeField] private float randomTempX;
    [SerializeField] private float randomTempY;
    [SerializeField] private Vector3 randomVector;*/

    [SerializeField] Vector3 DigPointA;
    [SerializeField] Vector3 DigPointB;
    [SerializeField] Vector3 PointAHeightDepth; //these are the two extra points for bezier curve.
    [SerializeField] Vector3 PointBHeightDepth;

    [SerializeField] private LayerMask inPlatform;

    [SerializeField] private float TestValue;

    [SerializeField] private float digSpeed;


    public void SetPoints(Vector3 P_AA, Vector3 P_AB, Vector3 P_BA, Vector3 P_BB)
    { 
        DigPointA = P_AA;
        DigPointB = P_BA;
        PointAHeightDepth = P_AB;
        PointBHeightDepth = P_BB;
    }

    public void StartDigging(Vector3 P_1, Vector3 P_2, Vector3 P_3, Vector3 P_4)
    { 
        //move mole transform
        moleTransform.position = Bezier(P_1,P_2,P_3,P_4, TestValue);
        moleTransform.rotation = Quaternion.Euler(0, 0, TestValue * 180f);

    }

    public Vector3 Bezier(Vector3 P_1, Vector3 P_2, Vector3 P_3, Vector3 P_4, float Value)
    { 
        Vector3 A = Vector3.Lerp(P_1, P_2, Value);
        Vector3 B = Vector3.Lerp(P_2, P_3, Value);
        Vector3 C = Vector3.Lerp(P_3, P_4, Value);

        Vector3 D = Vector3.Lerp(A, B, Value);
        Vector3 E = Vector3.Lerp(B, C, Value);

        return Vector3.Lerp(D, E, Value);
    }

    void FixedUpdate()
    {
        if (DigPointA != Vector3.zero && DigPointB != Vector3.zero && PointAHeightDepth != Vector3.zero && PointBHeightDepth != Vector3.zero)
        {
            StartDigging(DigPointA, PointAHeightDepth, PointBHeightDepth, DigPointB);
        }
    }


    IEnumerator DoDig()
    { 
        
    }
}
