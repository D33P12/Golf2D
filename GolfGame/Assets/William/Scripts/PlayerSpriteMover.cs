using UnityEngine;

public class PlayerSpriteMover : MonoBehaviour
{
    [SerializeField] private Transform golfBallTransform;
    public void MovetoPlayer()
    {
        transform.position = golfBallTransform.position;
    }
}
