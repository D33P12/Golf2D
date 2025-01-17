using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GolfInput golfInput;

    private static InputManager _instance;

    #region Instance Setup
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        golfInput = new GolfInput();
    }

    private void OnEnable()
    {
        golfInput.Enable();
    }

    private void OnDisable()
    {
        golfInput.Disable();
    }
    #endregion

    #region GolfBall Inputs
    public float GetShootDirection()
    {
        return (float)golfInput.GolfBall.ShootDirectionChange.ReadValue<double>();
    }

    public InputAction GetShoot()
    {
        return golfInput.GolfBall.Shoot;
    }
    #endregion
}