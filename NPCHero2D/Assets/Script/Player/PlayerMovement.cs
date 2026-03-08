using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction playerInput;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField] private float moveSpeed = 5f;

    public bool isFacingRight;
    public bool isFacingLeft;

    private void Awake()
    {
        playerInput = new PlayerInputAction();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Move.performed += ctx => Movement();
        playerInput.Player.Move.canceled += ctx => movementInput = Vector2.zero; // Stop movement when input is released
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log("Moving Right");
                isFacingRight = true;
            }
            else if (movement.x < 0)
            {
                Debug.Log("Moving Left");
                isFacingLeft = true;
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
    }
}
