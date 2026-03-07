using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 50;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
