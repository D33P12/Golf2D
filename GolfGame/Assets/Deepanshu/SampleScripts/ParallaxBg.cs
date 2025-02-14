using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; 
        public float speedMultiplier;
    }

    public ParallaxLayer[] layers;
    public Camera mainCamera;
    private float previousCameraX;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        previousCameraX = mainCamera.transform.position.x;
    }

    private void Update()
    {
        float cameraDeltaX = mainCamera.transform.position.x - previousCameraX;

        if (Mathf.Abs(cameraDeltaX) > 0.001f) 
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].layerTransform != null)
                {
                    float moveAmount = -cameraDeltaX * layers[i].speedMultiplier;
                    layers[i].layerTransform.position += new Vector3(moveAmount, 0, 0); 
                }
            }
        }

        previousCameraX = mainCamera.transform.position.x;
    }
}
