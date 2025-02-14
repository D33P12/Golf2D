using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    [SerializeField] private Image forceBarImage;
    [SerializeField] private GolfPlayerController golfPlayerController;

    private void Update()
    {
        if (forceBarImage == null || golfPlayerController == null) return;

        float normalizedForce = Mathf.Clamp(golfPlayerController.currentForce / golfPlayerController.maxForce, 0, 1);

        forceBarImage.fillAmount = normalizedForce;
    }
    
}
