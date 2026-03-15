using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    private PlayerInputAction playerInput;
    [SerializeField] private LayerMask layerMask;

    private PlayerMovement playerMovement;
    private Vector3 directionofRay;

    private void Awake()
        {
            playerInput = new PlayerInputAction();
            playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Interact.performed += ctx => Interact();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        playerInput.Player.Interact.performed -= ctx => Interact();
        playerInput.Player.Disable();
    }

    public void Interact()
    {
        if (playerMovement.isFacingRight)
        {
            directionofRay = transform.right;
        }
        if (playerMovement.isFacingLeft)
        {
            directionofRay = -transform.right;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionofRay, 2f, layerMask);
        Debug.DrawRay(transform.position, directionofRay * 2f, Color.blue, 1);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<ObjectInteractionManager>() != null)
            {
                hit.collider.GetComponent<ObjectInteractionManager>().Interact();
            }
        }
    }
}
