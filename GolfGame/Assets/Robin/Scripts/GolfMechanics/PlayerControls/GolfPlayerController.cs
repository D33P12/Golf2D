using System;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    #region PlayerControlVariables
    //Common variables
    [NonSerialized] public InputManager inputManager;  //We use input manager to control the golf ball
    [SerializeField] private Transform coreTrans;
    private Vector2 _shootDirection = Vector2.right;

    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private float startingAngle = 0f;

    [SerializeField] private float minAim = 0f;
    [SerializeField] private float maxAim = 180f;

    private float currentAngle;
    private bool _canChangeDirection = true;

    [Header("Player Charging")]
    [SerializeField] private float chargeSpeed = 50f;

    [SerializeField] private float initialForce = 0f;
    [SerializeField] private float maxForce = 100f;

    [NonSerialized] public bool isCharging;
    private float currentForce;

    [Header("Player Shoot")]
    [SerializeField] private float friction = 2.0f;
    private Rigidbody2D ballRb;
    #endregion

    #region Initialization
    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;

        inputManager.GetShoot().performed += x => IsCharging();
        inputManager.GetShoot().canceled += x => IsNotCharging();

        ballRb = gameObject.GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Player Aiming
    private void FixedUpdate()
    {
        if (_canChangeDirection)
            HandlingAiming();
    }

    public void StartAiming()
    {
        //Set initial shoot direction
        currentAngle = startingAngle;
        //Show the shooting direction
        coreTrans.gameObject.SetActive(true);
        CanChangeDirection(true);
    }

    private void HandlingAiming()
    {        
        //Handling the function of player aiming
        Vector2 shootDirectionInput = inputManager.GetShootDirection();
        currentAngle = currentAngle - shootDirectionInput.x * aimSpeed * Time.deltaTime; //Always updating the currentAngle
        coreTrans.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, minAim, maxAim));
    }

    public void CanChangeDirection(bool value)
    {
        _canChangeDirection = value;
    }
    #endregion

    #region Player Charging
    public void StartCharging()
    {
        currentForce = initialForce;
    }

    public void HandlingCharging()
    {
        currentForce = currentForce + chargeSpeed * Time.deltaTime; //charging by using delta time

        if (currentForce > maxForce)    // if current force is higher than the max force the player can reach
            currentForce = maxForce;    // keep it at the maximum force

        Debug.Log(currentForce);
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
        if (ballRb == null) return;

        _shootDirection = Quaternion.AngleAxis(currentAngle, Vector3.forward) * _shootDirection;
        ballRb.AddForce(_shootDirection * currentForce / friction, ForceMode2D.Impulse);
    }

    public bool isBallStopped()
    {
        if (ballRb == null) return true;

        if (ballRb.IsSleeping()) 
            return true;
        else
            return false;
    }
    #endregion
}
