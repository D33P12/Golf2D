public class GolfStateMachine : BaseStateMachine
{
    #region Keep track of all golf player states
    //Create the 3 golf player states
    private PlayerAiming _playerAimingState;
    private PlayerCharging _playerChargingState;
    private PlayerShoot _playerShootState;

    //Create a public property for each state
    public PlayerAiming PlayerAimingState => _playerAimingState;
    public PlayerCharging PlayerChargingState => _playerChargingState;
    public PlayerShoot PlayerShootState => _playerShootState;
    #endregion

    #region Keep track of all supporting components
    // Actually in this case we use the golf player controller component only
    private GolfPlayerController _playerController;

    // Also we need to create instance for our player controller
    public GolfPlayerController PlayerController => _playerController;
    #endregion

    private void Awake()
    {
        _playerAimingState = new PlayerAiming(this);
        _playerChargingState = new PlayerCharging(this);
        _playerShootState = new PlayerShoot(this);

        _playerController = GetComponent<GolfPlayerController>();
    }

    private void Start()
    {
        // Switch to the default state for the golf player, which will be the aiming state
        SetState(PlayerAimingState);
    }
}
