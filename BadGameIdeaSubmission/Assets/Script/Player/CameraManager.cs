using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Settings")]
    private PlayerInputActions playerControls;
    [SerializeField] private Transform player;
    [SerializeField] public bool LockedCursor = true;
    [Header("Rotating")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 4f;
    private float lookAngle;
    [SerializeField] private float tiltMax = 75f;
    [SerializeField] private float tiltMin = 45f;
    private Transform pivot;
    private float tiltAngle;
    private Vector3 pivotEulers;


    private void Awake()
    {
        playerControls = new PlayerInputActions();
        pivot = transform.GetChild(0);
        pivotEulers = pivot.localEulerAngles;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Look.performed += ctx => HandleRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
            Vector3 targetPosition = player.position;
            targetPosition.y = transform.position.y; // Keep the camera's height unchanged
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        playerControls.Player.Look.performed -= ctx => HandleRotation();
        playerControls.Disable();
    }

    public void HandleRotation() // Handle camera rotation based on player input
    {
        lookAngle += playerControls.Player.Look.ReadValue<Vector2>().x * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

        tiltAngle -= playerControls.Player.Look.ReadValue<Vector2>().y * rotationSpeed * Time.deltaTime;
        tiltAngle = Mathf.Clamp(tiltAngle, -tiltMin, tiltMax);
        pivot.localRotation = Quaternion.Euler(pivotEulers.x + tiltAngle, pivotEulers.y, pivotEulers.z);
    }
}
