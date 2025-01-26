using UnityEngine;

public class GolfHole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You Win, next stage");
    }
}
