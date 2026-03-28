using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    private float attackTimer;
    [SerializeField] private float attackTimerMax = 1f;
    [SerializeField] private int damage = 10;
    private Vector3 directionofAttack;
    private EnemyMovement enemyMovement;
    [SerializeField] private AudioSource HittingSound;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        attackTimer = attackTimerMax;
        enemyMovement = GetComponent<EnemyMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;
        if (enemyMovement.isFacingRight)
        {
            directionofAttack = transform.right;
            return;
        }
        if (enemyMovement.isFacingLeft)
        {
            directionofAttack = -transform.right;
            return;
        }
    }

    public void Attacking()
    {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionofAttack, 2f, layerMask);
        Debug.DrawRay(transform.position, directionofAttack * 2f, Color.magenta, 1);
        if (attackTimer <= 0 && hit.collider != null)
            {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsAttack", true);
              Debug.Log("Hit " + hit.collider.name);
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                HittingSound.Play();
                playerHealth.TakeDamage(damage);
                }
                attackTimer = attackTimerMax;
               animator.SetBool("IsAttack", false);
            }
            
    }
}
