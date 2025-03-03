using System.Collections;
using UnityEngine;
public class PlayerControl : Singlton<PlayerControl>
{
    public bool FlipedSide { get; private set; }

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float dashSpeedMultiplayer = 4f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashTimeRecover = 0.5f;

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private TrailRenderer playerTrail;
    private Knocback knocback;

    private Vector2 movement;

    private bool isDashRecovr = false;

    private const string ANIMATOR_X_FLOAT = "XFloat";
    private const string ANIMATOR_Y_FLOAT = "YFloat";

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerTrail = GetComponentInChildren<TrailRenderer>();
        knocback = GetComponent<Knocback>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void Start()
    {
        playerControls.Movement.dash.performed += _ => Dash();

        ActiveInventory.Instanse.EqipFirstWeapon();
    }

    private void Update()
    {
        if (PlayerHelth.Instanse.IsDead)
        {
            return;
        }

        PlayerInput();
        FaceToMause();
    }

    private void FixedUpdate()
    {
        if (PlayerHelth.Instanse.IsDead)
        {
            return;
        }

        MovePlayer();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.move.ReadValue<Vector2>();

        animator.SetFloat(ANIMATOR_X_FLOAT, movement.x);
        animator.SetFloat(ANIMATOR_Y_FLOAT, movement.y);
    }

    private void MovePlayer()
    {
        if (!knocback.IsKnocBacking)
        {
            Vector2 movePlayerTo = rb.position + (movement * movementSpeed * Time.fixedDeltaTime);
            rb.MovePosition(movePlayerTo);
        }
    }

    private void FaceToMause()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePositionInWorld.x < transform.position.x)
        {
            sprite.flipX = true;
            FlipedSide = true;
        }
        else if (mousePositionInWorld.x > transform.position.x)
        {
            sprite.flipX = false;
            FlipedSide = false;
        }
    }

    private void Dash()
    {
        if (!isDashRecovr && Stamina.Instanse.CurrentStamina != 0)
        {
            isDashRecovr = true;
            Stamina.Instanse.UseStamina();
            StartCoroutine(DashRoutin());
        }
    }

    private IEnumerator DashRoutin()
    {
        playerTrail.emitting = true;
        movementSpeed *= dashSpeedMultiplayer;
        yield return new WaitForSeconds(dashTime);
        movementSpeed /= dashSpeedMultiplayer;
        playerTrail.emitting = false;
        yield return new WaitForSeconds(dashTimeRecover);
        isDashRecovr = false;
    }
}
