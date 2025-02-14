using UnityEngine;
using System.Collections;
using System;

public class GolfStateMachine : BaseStateMachine
{
    [SerializeField] public bool moleDigsInThisLevel;

    #region Keep track of all golf states
    //Create the 3 golf player states
    private PlayerAiming _playerAimingState;
    private PlayerLooking _playerLookingState;
    private PlayerCharging _playerChargingState;
    private PlayerShoot _playerShootState;
    private PlayerStart _playerStartState;

    //3 states for mole
    private MoleSetPosition _moleSetPositionState;
    private MoleDig _moleDigState;
    private LandFall _landFallState;
    #endregion

    #region Referencing all of golf states
    //Golf Player
    public PlayerAiming PlayerAimingState => _playerAimingState;
    public PlayerLooking PlayerLookingState => _playerLookingState;
    public PlayerCharging PlayerChargingState => _playerChargingState;
    public PlayerShoot PlayerShootState => _playerShootState;
    public PlayerStart PlayerStartState => _playerStartState;

    //Mole
    public MoleSetPosition MoleSetPositionState => _moleSetPositionState;
    public MoleDig MoleDigState => _moleDigState;
    public LandFall LandFallState => _landFallState;
    #endregion

    #region Keep track of all supporting components
    //golf player controller
    [SerializeField] private GolfPlayerController _playerController;

    //I needed some extra stuff so I'll grab them here
    [SerializeField] private Mole _moleObject;
    [SerializeField] private Pathfinder _pathfinderObject;
    [SerializeField] private Field _fieldObject;
    [SerializeField] private GameObject _uiMoleThinkingText;
    [SerializeField] private PlayerSpriteMover _playerSpriteMover;

    // Also we need to create instance for our player controller
    public GolfPlayerController PlayerController => _playerController;

    //Mole supporting components
    public Mole MoleObject => _moleObject;
    public Pathfinder PathfinderObject => _pathfinderObject;
    public Field FieldObject => _fieldObject;
    public GameObject UIMoleThinkingText => _uiMoleThinkingText;
    public PlayerSpriteMover PlayerSpriteMover => _playerSpriteMover;
    #endregion

    private void Awake()
    {
        //Golf Player
        _playerAimingState = new PlayerAiming(this);
        _playerLookingState = new PlayerLooking(this);
        _playerChargingState = new PlayerCharging(this);
        _playerShootState = new PlayerShoot(this);
        _playerStartState = new PlayerStart(this);

        //Mole
        _moleSetPositionState = new MoleSetPosition(this);
        _moleDigState = new MoleDig(this);
        _landFallState = new LandFall(this);
    }

    private void Start()
    {
        // Switch to the default state for the golf player, which will be the start state
        SetState(PlayerStartState);
    }
}
