using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private AudioSource damageSound;

    private EnemyMovement enemyMovement;
    [SerializeField] private Animator animator;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageSound.Play();
        if (enemyMovement.isFacingRight)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), transform.rotation);
        }
        else if (enemyMovement.isFacingLeft)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), transform.rotation);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Handle enemy death (e.g., play animation, disable controls, etc.)
        Debug.Log("Enemy has died.");
        Destroy(gameObject); // Destroy the enemy game object
    }
}
