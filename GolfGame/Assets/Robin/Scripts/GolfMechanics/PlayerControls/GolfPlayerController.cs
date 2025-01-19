using System;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    #region PlayerControlVariables
    //Common variables
    [NonSerialized] public InputManager inputManager;  //We use input manager to control the golf ball

    [Header("Player Aiming")]
    [SerializeField] private float aimSpeed = 45f;
    [SerializeField] private Transform shootTrans;
    [SerializeField] private float startingAngle = 0f;

    [SerializeField] private float minAim = 0f;
    [SerializeField] private float maxAim = 180f;

    private float currentAngle;

    [Header("Player Charging")]
    [SerializeField] private float chargeSpeed = 50f;

    [SerializeField] private float initialForce = 0f;
    [SerializeField] private float maxForce = 100f;

    [NonSerialized] public bool isCharging;
    private float currentForce;
    #endregion
    private void Awake()
    {
        //Get input manager instance
        inputManager = InputManager.Instance;

        inputManager.GetShoot().performed += x => IsCharging();
        inputManager.GetShoot().canceled += x => IsNotCharging();
    }

    #region Player Aiming
    public void StartAiming()
    {
        //Set initial shoot direction
        currentAngle = startingAngle;
    }

    public void HandlingAiming()
    {        
        //Handling the function of player aiming
        Vector2 shootDirectionInput = inputManager.GetShootDirection();
        currentAngle = currentAngle - shootDirectionInput.x * aimSpeed * Time.deltaTime; //Always updating the currentAngle
        shootTrans.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentAngle, minAim, maxAim));
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
            currentForce = initialForce;    // set it back to the initial force

        Debug.Log(currentForce);
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
}
