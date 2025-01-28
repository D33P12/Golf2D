using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTriggerStayDeleteTile : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);
    }


    [SerializeField] private LayerMask inPlatform;

    void Update()
    {

        Collider2D overCollider2D = Physics2D.OverlapCircle(this.gameObject.transform.position, 0.2f, inPlatform);
        if (overCollider2D != null)
        {
            overCollider2D.transform.GetComponent<GroundTiles>().DeleteTile(this.gameObject.transform.position);
        }

    }
}
