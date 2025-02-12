using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Sprite sprite;
        public float speedMultiplier;
    }
    public ParallaxLayer[] layers;
    public float parallaxSpeed = 1f;
    public Camera mainCamera;
    private GameObject[] layerObjects; private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        layerObjects = new GameObject[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            GameObject layerObj = new GameObject("ParallaxLayer_" + i);
            layerObj.transform.parent = transform;
            layerObj.transform.position = new Vector3(0, 0, 10 - i);

            SpriteRenderer sr = layerObj.AddComponent<SpriteRenderer>();
            sr.sprite = layers[i].sprite;
            sr.sortingOrder = i;

            layerObjects[i] = layerObj;
        }
    }
    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (layerObjects[i] != null)
            {
                float moveSpeed = parallaxSpeed * layers[i].speedMultiplier * Time.deltaTime;
                layerObjects[i].transform.position += new Vector3(moveSpeed, 0, 0);
            }
        }
    }
}
