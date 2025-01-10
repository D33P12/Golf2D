using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float chargeTime = 2f;
   
    public LayerMask groundLayer;
    public float chargeRange = 2f;
    public TextMeshProUGUI chargeText;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _isCharging;
  
    private float _chargeAmount; 
    private PlayerInput _input;
    private Vector2 _moveInput;
    
    private GameObject _ball;
    private bool _isNearBall;
    private Vector2 _shootDirection = Vector2.right;
    private float _horizontalInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = new PlayerInput();
        _ball = GameObject.Find("Ball");

        _input.PlayerMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _input.PlayerMovement.Move.canceled += ctx =>  _moveInput = Vector2.zero;
        _input.PlayerMovement.Jump.performed += ctx => Jump();
        _input.PlayerMovement.ChargePower.performed += ctx => StartCharge();
        _input.PlayerMovement.ChargePower.canceled += ctx => ShootBall();
        _input.PlayerMovement.ShootHeight.performed += ctx => AdjustShootHeight(ctx.ReadValue<Vector2>());
        _input.PlayerMovement.CancelCharge.performed += ctx => CancelShoot();
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        CheckGround();
    }
    private void MovePlayer()
    {
        _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);
    }
    private void Update()
    {
   
        _isNearBall = Vector2.Distance(transform.position, _ball.transform.position) <= chargeRange;

        if (_isCharging)
        {
            _chargeAmount = Mathf.PingPong(Time.time * (100 / chargeTime), 100);
            UpdateChargeUI(); 
        }

        AdjustShootHeight(_input.PlayerMovement.ShootHeight.ReadValue<Vector2>());
    }
    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }
    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void StartCharge()
    {
        if (_isGrounded && _isNearBall)
        {
            _isCharging = true;
        }
    }
    private void ShootBall()
    {
        if (_isCharging)
        {
            float launchForce = _chargeAmount / 100f * 50f; 
            Rigidbody2D ballRb = _ball.GetComponent<Rigidbody2D>();

            if (ballRb != null)
            {
                ballRb.AddForce(_shootDirection * launchForce, ForceMode2D.Impulse);
            }

            _isCharging = false;
            _chargeAmount = 0f;
            UpdateChargeUI(); 
        }
    }
    private void AdjustShootHeight(Vector2 input)
    {
        if (_isCharging)
        {
            float angleStep = 2f;

            if (input.y > 0)
            {
                _shootDirection = Quaternion.AngleAxis(angleStep * Time.deltaTime, Vector3.forward) * _shootDirection;
            }
            else if (input.y < 0)
            {
                _shootDirection = Quaternion.AngleAxis(-angleStep * Time.deltaTime, Vector3.forward) * _shootDirection;
            }

            float currentAngle = Vector2.SignedAngle(Vector2.right, _shootDirection);
            currentAngle = Mathf.Clamp(currentAngle, 0f, 180f);
        }
    }
    private void CancelShoot()
    {
        _isCharging = false;
        _chargeAmount = 0f;
        UpdateChargeUI(); 
    }
    private void UpdateChargeUI()
    {
        if (chargeText != null)
        {
            chargeText.text = $"Charge: {_chargeAmount:0}%";
        }
    }
    private void OnDrawGizmos()
    {
        if (_isCharging)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)_shootDirection * 2f);
        }
    }
}
