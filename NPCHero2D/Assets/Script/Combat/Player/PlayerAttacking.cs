using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    private PlayerInputAction playerInput;
    [SerializeField] private LayerMask layerMask;

    private float attackTimer;
    [SerializeField] private float attackTimerMax = 1f;
    [SerializeField] private int damage = 10;
    private PlayerMovement playerMovement;
    private Vector3 directionofAttack;

    private void Awake()
    {
        playerInput = new PlayerInputAction();
        attackTimer = attackTimerMax;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Attack.performed += ctx => Attacking(); 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (playerMovement.isFacingRight)
        {
            directionofAttack = transform.right;
        }
        if (playerMovement.isFacingLeft)
        {
            directionofAttack = -transform.right;
        }

    }

    private void OnDisable()
    {
        
        playerInput.Player.Attack.performed -= ctx => Attacking();
        playerInput.Player.Disable();
    }

    public void Attacking()
    {
        if (attackTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionofAttack, 2f, layerMask);

            if (hit)
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("Hit");
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(damage);
                }
            }
            Debug.DrawRay(transform.position, directionofAttack * 2f, Color.green, 100f);
            attackTimer = attackTimerMax;
        }
    }
       
}
