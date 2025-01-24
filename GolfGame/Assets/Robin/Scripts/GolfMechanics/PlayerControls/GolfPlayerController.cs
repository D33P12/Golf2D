using System;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    #region PlayerControlVariables
    //Common variables
    [NonSerialized] public InputManager inputManager;  //We use input manager to control the golf ball
    [SerializeField] private Transform coreTrans;

    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private float startingAngle = 0f;

    [SerializeField] private float minAim = 0f;
    [SerializeField] private float maxAim = 180f;

    private float _currentAngle;
    private bool _canChangeDirection = true;

    [Header("Player Charging")]
    [SerializeField] private float chargeSpeed = 50f;

    [SerializeField] private float initialForce = 0f;
    public float maxForce = 100f;

    [NonSerialized] public bool isCharging;
    [NonSerialized] public float currentForce;

    [Header("Player Shoot")]
    [SerializeField] private float friction = 2.0f;

    private Vector2 _shootDirection = Vector2.right;
    private Rigidbody2D _ballRb;
    #endregion

    #region Initialization
    private void Start()
    {
        //Set the ball's rigid body 2D
        _ballRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;

        inputManager.GetShoot().performed += x => IsCharging();
        inputManager.GetShoot().canceled += x => IsNotCharging();
    }

    public void Initialization()
    {
        //Initialize current values
        _currentAngle = startingAngle;
        currentForce = initialForce;
        _shootDirection = Vector2.right;
        //Show the shooting direction and start aiming
        coreTrans.gameObject.SetActive(true);
        CanChangeDirection(true);
    }
    #endregion

    #region Player Aiming
    private void FixedUpdate()
    {
        if (_canChangeDirection)
            HandlingAiming();
    }

    private void HandlingAiming()
    {        
        //Handling the function of player aiming
        Vector2 shootDirectionInput = inputManager.GetShootDirection();
        _currentAngle = _currentAngle - shootDirectionInput.x * aimSpeed * Time.deltaTime; //Always updating the currentAngle
        coreTrans.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(_currentAngle, minAim, maxAim));
    }

    public void CanChangeDirection(bool value)
    {
        _canChangeDirection = value;
    }
    #endregion

    #region Player Charging

    public void HandlingCharging()
    {
        currentForce = currentForce + chargeSpeed * Time.deltaTime; //charging by using delta time

        if (currentForce > maxForce)    // if current force is higher than the max force the player can reach
            currentForce = maxForce;    // keep it at the maximum force
    }

    public void FinishCharging()
    {
        coreTrans.gameObject.SetActive(false);
    }

    private void IsCharging()
    {
        isCharging = true;
    }
    private void IsNotCharging()
    {
        isCharging = false;
    }
    #endregion

    #region Player Shoot
    public void ShootBall()
    {
        if (_ballRb == null) return;

        if (currentForce == 0) return;

        Debug.Log("Shoot!");
        _shootDirection = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * _shootDirection;
        _ballRb.AddForce(_shootDirection * currentForce / friction, ForceMode2D.Impulse);
    }

    public bool isBallStopped()
    {
        if (_ballRb == null) return true;

        if (_ballRb.IsSleeping()) 
            return true;
        else
            return false;
    }
    #endregion
}
