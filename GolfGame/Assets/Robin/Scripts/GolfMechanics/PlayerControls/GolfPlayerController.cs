using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem.XR;
using Cinemachine;

public class GolfPlayerController : MonoBehaviour
{
    #region PlayerControlVariables
    //Common variables
    [NonSerialized] public InputManager inputManager;  //We use input manager to control the golf ball
    [SerializeField] private Transform coreTrans;

    [NonSerialized] public Rigidbody2D ballRb;
    [SerializeField] private Rigidbody2D pathRb;
    public Rigidbody2D landRb;
    //[SerializeField] private PhysicsMaterial2D ballRbMaterial;

    private Vector2 input;
    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private float startingAngle = 0f;

    [SerializeField] private float minAim = 0f;
    [SerializeField] private float maxAim = 180f;

    private float _currentAngle;
    //private bool _canChangeDirection = true;

    [Header("Player Charging")]
    [SerializeField] private float chargeSpeed = 50f;
    [SerializeField] private float initialForce = 0f;
    public float maxForce = 100f;
    [SerializeField] private float warningForce = 125f;
    [SerializeField] private float foulForce = 150f;

    [NonSerialized] public bool isCharging;
    private float calForce;
    [NonSerialized] public float currentForce;
    [NonSerialized] public bool isFouledCharging = false;

    [Header("Player Shoot")]
    [SerializeField] private float friction = 2.0f;
    private Vector2 _shootDirection = Vector2.right;

    [Header("Player Look")]
    [SerializeField] private CinemachineVirtualCamera cameraObject;
    [SerializeField] private float lookSpeed = 6.0f;

    private Vector2 camVelocity;
    [NonSerialized] public bool isLooking;

    [Header("Camera Boundary Setup")]
    //Robin's Note: Make sure the center point's position is 0, and no hypo distances between a boundary point and the center point.
    [SerializeField] private Transform levelCenterPoint;
    [SerializeField] private Transform camTopBound;
    [SerializeField] private Transform camButtonBound;
    [SerializeField] private Transform camLeftBound;
    [SerializeField] private Transform camRightBound;
    #endregion

    #region Initialization
    private void Start()
    {
        //Set the ball's rigid body 2D
        ballRb = gameObject.GetComponent<Rigidbody2D>();
        //Hide the force indicator when start
        coreTrans.gameObject.SetActive(false);
    }

    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;

        //Player shoot control
        inputManager.GetShoot().performed += x => IsCharging();
        inputManager.GetShoot().canceled += x => IsNotCharging();

        //Player look control
        inputManager.CanPlayerLook().performed += x => IsLooking();
        inputManager.CanPlayerLook().canceled += x => IsNotLooking();
    }

    public void AimingInitialization()
    {
        cameraObject.Follow = gameObject.transform; //initialize camera
        //Initialize current values
        _currentAngle = startingAngle;
        calForce = initialForce;
        currentForce = initialForce;
        _shootDirection = Vector2.right;
        //Show the shooting direction and start aiming
        coreTrans.gameObject.SetActive(true);
        //CanChangeDirection(true);
    }

    public void LookingInitialization()
    {
        //initialize look position (stupid virtual camera's z position value is not 0, otherwise the player will see nothing)
        cameraObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, cameraObject.transform.position.z);
        //initialize camera
        cameraObject.Follow = null;
    }
    #endregion
    
    private void FixedUpdate()
    {   
        float cameraPositionX = Mathf.Clamp(cameraObject.transform.position.x, LeftBound(), RightBound()); 
        float cameraPositionY = Mathf.Clamp(cameraObject.transform.position.y, ButtonBound(), TopBound()); 

        cameraObject.transform.position = new Vector3(cameraPositionX, cameraPositionY, cameraObject.transform.position.z);
    }

    #region Player Aiming
    public void HandlingAiming()
    {
        //Handling the function of player aiming
        input = inputManager.GetShootDirection();
        _currentAngle = _currentAngle - input.x * aimSpeed * Time.deltaTime; //Always updating the currentAngle
        coreTrans.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(_currentAngle, minAim, maxAim));
    }

    /*public void CanChangeDirection(bool value)
    {
        _canChangeDirection = value;
    }*/
    #endregion

    #region Player Look
    public void HandlingLooking()
    {
        input = inputManager.PlayerLookAround();

        var cameraMove = new Vector2(input.x, input.y);
        cameraObject.transform.Translate(cameraMove * Time.deltaTime * lookSpeed, Space.World);
    }

    private void IsLooking()
    {
        isLooking = true;
    }
    private void IsNotLooking()
    {
        isLooking = false;
    }
    #endregion

    #region Boundary Calculator
    //In Unity, left and button position should be negative value, top and right should be positive value
    private float LeftBound()
    {
        return -Vector2.Distance(camLeftBound.position, levelCenterPoint.position);
    }

    private float RightBound()
    {
        return Vector2.Distance(camRightBound.position, levelCenterPoint.position);
    }

    private float TopBound()
    {
        return Vector2.Distance(camTopBound.position, levelCenterPoint.position);
    }

    private float ButtonBound()
    {
        return -Vector2.Distance(camButtonBound.position, levelCenterPoint.position);
    }
    #endregion

    #region Player Charging

    public void HandlingCharging()
    {
        calForce = calForce + chargeSpeed * Time.deltaTime; //calculate charging force

        if (calForce > maxForce)    // if current force is higher than the max force the player can reach
        {
            currentForce = maxForce;    // keep it at the maximum force
            if (calForce >= foulForce)
                isFouledCharging = true;
            else if (calForce >= warningForce)
            {
                Debug.Log("Warning, you are trying to get fouled!!");
                isFouledCharging = false;
            }
            else
                isFouledCharging = false;
        }
        else
        {
            currentForce = calForce;    //Set current force
            isFouledCharging = false;   //Golf player's charging is fine, not foul
        }
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

        Debug.Log("Shoot!");
        _shootDirection = Quaternion.AngleAxis(_currentAngle, Vector3.forward) * _shootDirection;
        ballRb.AddForce(_shootDirection * currentForce / friction, ForceMode2D.Impulse);
    }
    #endregion

    #region Player Waiting For Turn
    public void TurnStaticThenDynamic()
    {
        Debug.Log("Freeze the golfBall for a moment");
        if (ballRb == null) return;
        Debug.Log("Freezing");
        ballRb.simulated = false;
        StartCoroutine(BeAbleToMove());

    }

    IEnumerator BeAbleToMove()
    {
        yield return new WaitForSeconds(1);
        ballRb.simulated = true;

    }
    #endregion

    #region UnUsed
    /*public bool isBallStopped()
    {
        if (ballRb == null) return true;

        if (ballRb.IsSleeping()) 
            return true;
        else
            return false;
    }*/

    /*#region Rigid body Handlers
    public void IsLandStopped(bool value)
    {
        if (value == true)
            landRb.Sleep();
        else
            landRb.WakeUp();
    }

    public void EnableBallPhysMaterial()
    {
        ballRb.sharedMaterial = ballRbMaterial;
    }

    public void DisableBallPhysMaterial()
    {
        ballRb.sharedMaterial = null;
    }
    #endregion*/
    #endregion
}
