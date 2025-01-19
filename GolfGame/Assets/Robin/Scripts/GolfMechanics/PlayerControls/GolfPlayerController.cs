using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI.Table;

public class GolfPlayerController : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private Transform shootTrans;
    [SerializeField] private float startingAngle = 0f;
    
    private float currentAngle;

    [SerializeField] private float minAim = 0f;
    [SerializeField] private float maxAim = 180f;

    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;
    }

    public void StartAiming()
    {
        //Set initial shoot direction
        currentAngle = startingAngle;
    }

    public void AimingHandling()
    {
        //Handling the function of player aiming
        Vector2 shootDirectionInput = inputManager.GetShootDirection();
        currentAngle = currentAngle - shootDirectionInput.x * aimSpeed * Time.deltaTime; //Always updating the currentAngle
        shootTrans.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, minAim, maxAim));
    }
}
