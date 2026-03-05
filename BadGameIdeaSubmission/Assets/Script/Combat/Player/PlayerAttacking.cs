using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    [SerializeField] private LayerMask layerMask;
    private float attackTimer = 0f;
   [SerializeField] private float attackTimerMax = 1f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        attackTimer = attackTimerMax;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += ctx => Attack();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Attack.performed -= ctx => Attack();
        playerInputActions.Player.Disable();
    }

    public void Attack()
    {
        Debug.Log("Player Attacked");
      if (attackTimer > 0f)
        {
            return; // Attack is still on cooldown
        }
        else if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 2f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * 3f, Color.green, 100f);
            if (hitInfo.collider.transform.parent.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hitInfo.collider.transform.parent.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(10); // Example damage value
                }
            }
                attackTimer = attackTimerMax; // Reset the attack timer
        }
        else
        {

            Debug.DrawRay(transform.position, transform.forward * 3f, Color.red, 100f);
        }
        // Implement attack logic here (e.g., damage enemies, play attack animation, etc.)
    }
}
