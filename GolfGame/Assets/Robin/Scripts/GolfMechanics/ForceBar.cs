using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    [SerializeField] private Transform forceBar;
    private float _maxUILength;

    private void Start()
    {
        _maxUILength = forceBar.localScale.x;
    }

    private void Update()
    {
        GolfPlayerController golfPlayerController = GetComponent<GolfPlayerController>();
        forceBar.localScale = new Vector2 (Mathf.Clamp((golfPlayerController.currentForce/ golfPlayerController.maxForce), 0, _maxUILength), 1);
    }
}
