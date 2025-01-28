using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTriggerStayDeleteTile : MonoBehaviour
{

    [SerializeField] private LayerMask inPlatform;

    void Update()
    {

        Collider2D overCollider2D = Physics2D.OverlapCircle(transform.position, 0.1f, inPlatform);
        if (overCollider2D != null)
        {
            overCollider2D.transform.GetComponent<GroundTiles>().DeleteTile(transform.position);
        }

    }
}
