using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class RecordOverlappedTiles : MonoBehaviour
{
    [SerializeField] private Tilemap origionalTilemap;

    [SerializeField] private LayerMask inPlatform;

    //this is a copy of the other one, the reason for this is for a feature this will do something else

    private List<Vector3Int> recordedVectors = new List<Vector3Int>();

    private Vector3Int recordTemp;
    private Vector3Int recordTempPrevious;

    [SerializeField] private bool firstNull;
    [SerializeField] private int previousTilemap;

    void Start()
    {
        firstNull = true;
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D overCollider2D = Physics2D.OverlapCircle(this.gameObject.transform.position, 0.01f, inPlatform);
        if (overCollider2D != null)
        {
            if (firstNull)
            {
                recordTempPrevious = recordTemp;
            }

            recordTemp = origionalTilemap.WorldToCell(this.gameObject.transform.position);
            firstNull = false;

            if (previousTilemap != overCollider2D.transform.gameObject.GetInstanceID())
            {
               // recordNext = true;
                previousTilemap = overCollider2D.transform.gameObject.GetInstanceID();
            }

        }
        else {
            if (!firstNull) //if(!firstNull && recordNext)
            {
                //recordNext = false;
                recordedVectors.Add(new Vector3Int(recordTemp.x, recordTemp.y, 0));
            }
        }

    }

    public List<Vector3Int> GetRecord()
    {
        recordedVectors.Add(recordTemp);
        return recordedVectors;
    }

    public void clearRecord()
    { 
        recordedVectors.Clear();
        recordTemp = new Vector3Int(0, 0, 0);
        previousTilemap = 0;
        firstNull = true;
    }
}
