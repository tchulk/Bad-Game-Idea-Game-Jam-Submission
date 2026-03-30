using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private Vector3 directionofAttack;
    private EnemyMovement enemyMovement;
    [SerializeField] private AudioSource HittingSound;

    [SerializeField] private LayerMask layerMask;
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (hit.collider != null)
            {
            Debug.Log("Hit " + hit.collider.name);
                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                HittingSound.Play();
                playerHealth.TakeDamage(damage);
                }
            }
            
    }
}
