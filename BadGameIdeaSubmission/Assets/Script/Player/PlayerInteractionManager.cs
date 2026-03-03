using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerInteractionManager : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private PlayerInput playerInput;
    [SerializeField] private LayerMask layerMask;
    private Camera mainCamera;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.Interact.performed += ctx => OnInteraction();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        playerControls.Player.Interact.performed -= ctx => OnInteraction();
        playerControls.Disable();
    }
    public void OnInteraction()
    {
        // Define the origin and direction of the ray from the center of the viewport
        // Viewport coordinates are normalized (0,0 is bottom-left, 1,1 is top-right)
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.3f, 0f));
        RaycastHit hit;
        

        // Perform the physics raycast
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            // The ray hit an object
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 100f);
        }
        else
        {
            // The ray did not hit any object
            Debug.Log("No object hit by the raycast.");
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 100f);
        }

    }
}
