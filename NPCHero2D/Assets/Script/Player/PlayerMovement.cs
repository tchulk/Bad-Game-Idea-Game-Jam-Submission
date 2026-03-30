using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction playerInput;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField] private float moveSpeed = 5f;

    public bool isFacingRight;
    public bool isFacingLeft;

    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        playerInput = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Move.performed += ctx => Movement();
        playerInput.Player.Move.canceled += ctx => StopMovement();
        playerInput.Player.Exit.performed += ctx => Exit();
    }

    private void FixedUpdate()
    {
        // Handle physics-based movement here if needed
        if (movementInput != null)
        {
            Vector2 movement = movementInput * moveSpeed * Time.fixedDeltaTime;

            // Check horizontal movement
            if (movement.x > 0)
            {
                isFacingRight = true;
                isFacingLeft = false;
                spriteRenderer.flipX = false;
            }
            else if (movement.x < 0)
            {
                isFacingLeft = true;
                isFacingRight = false;
                spriteRenderer.flipX = true;
            }

            rb.MovePosition(rb.position + movement);
            
        }
     }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= ctx => Movement();
        playerInput.Player.Move.canceled -= ctx => movementInput = Vector2.zero;
        playerInput.Disable();
    }

    private void Movement()
    {
        // Use movementInput to move the player character
        movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        animator.SetBool("IsMoving", true);
    }

    private void StopMovement()
    {
        movementInput = Vector2.zero; // Stop movement when input is released
        animator.SetBool("IsMoving", false);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        // This quits the game application when built
#else
            Application.Quit();
#endif
    }
}
