using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MoleClaws : MonoBehaviour
{
    [SerializeField] private Tilemap origionalTilemap;

    [SerializeField] private LayerMask inPlatform;

    //this is a copy of the other one, the reason for this is for a feature this will do something else



    //private bool 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D overCollider2D = Physics2D.OverlapCircle(this.gameObject.transform.position, 0.2f, inPlatform);
        if (overCollider2D != null)
        {
            overCollider2D.transform.GetComponent<GroundTiles>().DeleteTile(this.gameObject.transform.position);
        }

    }


   
}
