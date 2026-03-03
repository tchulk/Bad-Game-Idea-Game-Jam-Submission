using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
     private PlayerInputActions playerControls;
    private CharacterController controller;
    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 5f;
    private Camera mainCamera;




    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerInputActions();
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Move.performed += ctx => OnMove();
        playerControls.Player.Move.canceled += ctx => OnMove();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f; // Ignore vertical component
        cameraForward.Normalize();
        gameObject.transform.forward = cameraForward;
        controller.Move(moveSpeed * Time.deltaTime * cameraForward * move.z + moveSpeed * Time.deltaTime * mainCamera.transform.right * move.x);
    }

    private void OnDisable()
    {
        playerControls.Player.Move.performed -= ctx => OnMove();
        playerControls.Player.Move.canceled -= ctx => OnMove();
        playerControls.Disable();
    }

    public void OnMove()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        
    }
}
