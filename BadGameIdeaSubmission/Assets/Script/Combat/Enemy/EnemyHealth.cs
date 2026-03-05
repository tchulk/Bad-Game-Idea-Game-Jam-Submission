using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"Enemy took {damageAmount} damage. Current health: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        // Implement death logic here (e.g., play death animation, drop loot, etc.)
        Destroy(gameObject); // Example: destroy the enemy GameObject
    }
}
