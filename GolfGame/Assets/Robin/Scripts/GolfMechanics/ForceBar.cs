using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    [SerializeField] private Transform forceBar;

    private void Update()
    {
        GolfPlayerController golfPlayerController = GetComponent<GolfPlayerController>();
        forceBar.localScale = new Vector2 (Mathf.Clamp((golfPlayerController.currentForce/ golfPlayerController.maxForce), 0, 1), 1);
    }
}
