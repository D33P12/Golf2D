using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GolfPlayerController : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Player Aiming")]
    private float shootAngle = 0f;
    private Vector2 _shootDirection = Vector2.right;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    public void StartAiming()
    {
        _shootDirection = Vector2.right;
    }

    public void Aim()
    {
        shootAngle = inputManager.GetShootDirection().x;
        _shootDirection = Quaternion.AngleAxis(shootAngle * Time.deltaTime, Vector3.forward) * _shootDirection;
    }
}
