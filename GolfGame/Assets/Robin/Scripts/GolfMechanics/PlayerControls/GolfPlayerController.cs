using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GolfPlayerController : MonoBehaviour
{   
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }
}
