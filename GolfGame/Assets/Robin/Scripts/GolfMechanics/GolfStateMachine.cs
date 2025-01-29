using UnityEngine;
using System.Collections;

public class GolfStateMachine : BaseStateMachine
{

    //Note by Will: next time doing this, separate the state machine out of the golfball and move it to a different gameobject. 
    #region Keep track of all golf player states
    //Create the 3 golf player states
    private PlayerAiming _playerAimingState;
    private PlayerCharging _playerChargingState;
    private PlayerShoot _playerShootState;

    //3 states for mole
    private MoleSetPosition _moleSetPositionState;
    private MoleDig _moleDigState;
    private LandFall _landFallState;

    //Create a public property for each state
    public PlayerAiming PlayerAimingState => _playerAimingState;
    public PlayerCharging PlayerChargingState => _playerChargingState;
    public PlayerShoot PlayerShootState => _playerShootState;
    public MoleSetPosition MoleSetPositionState => _moleSetPositionState;
    public MoleDig MoleDigState => _moleDigState;
    public LandFall LandFallState => _landFallState;
    #endregion

    #region Keep track of all supporting components
    // Actually in this case we use the golf player controller component only
    private GolfPlayerController _playerController;

    //I needed some extra stuff so I'll grab them here
    [SerializeField] private Mole _moleObject;
    [SerializeField] private Pathfinder _pathfinderObject;
    [SerializeField] private Field _fieldObject;
    [SerializeField] private GameObject _uiMoleThinkingText;

    // Also we need to create instance for our player controller
    public GolfPlayerController PlayerController => _playerController;

    public Mole MoleObject => _moleObject;
    public Pathfinder PathfinderObject => _pathfinderObject;
    public Field FieldObject => _fieldObject;
    public GameObject UIMoleThinkingText => _uiMoleThinkingText;
    #endregion

    private void Awake()
    {
        _playerAimingState = new PlayerAiming(this);
        _playerChargingState = new PlayerCharging(this);
        _playerShootState = new PlayerShoot(this);
        _moleSetPositionState = new MoleSetPosition(this);
        _moleDigState = new MoleDig(this);
        _landFallState = new LandFall(this);

        _playerController = GetComponent<GolfPlayerController>();
    }

    private void Start()
    {
        // Switch to the default state for the golf player, which will be the aiming state
        SetState(MoleSetPositionState);
    }
}
