using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class GolfPlayerController : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private Transform shootTrans;

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
        shootTrans.rotation = Quaternion.identity;
    }

    public void AimingHandling()
    {
        //Handling the function of player aiming
        Vector2 shootDirectionInput = inputManager.GetShootDirection();
        float shootAngle = -shootDirectionInput.x * aimSpeed * Time.deltaTime;

        shootTrans.Rotate(Vector3.forward, shootAngle);

        //Limit the aim angle
        if (shootTrans.eulerAngles.z < minAim)
        {
            shootTrans.rotation = Quaternion.Euler(0f, 0f, minAim);
        }

        if (shootTrans.eulerAngles.z > maxAim)
        {
            shootTrans.rotation = Quaternion.Euler(0f, 0f, maxAim);
        }
    }
}
