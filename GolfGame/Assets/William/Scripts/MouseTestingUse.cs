using UnityEngine;

public class MouseTestingUse : MonoBehaviour
{
    Vector3 _mousePosition;
    [SerializeField] private LayerMask inPlatform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_mousePosition, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D overCollider2D = Physics2D.OverlapCircle(_mousePosition, 0.01f, inPlatform);
            if (overCollider2D != null)
            {
                overCollider2D.transform.GetComponent<GroundTiles>().DeleteTile(_mousePosition);
            }
            Debug.Log("Mouse Position:" + Input.mousePosition);
        }
    }
}
