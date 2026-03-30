using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private AudioSource damageSound;

    private EnemyMovement enemyMovement;
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageSound.Play();
        enemyMovement.animator.SetTrigger("Hurt");
        if (enemyMovement.isFacingRight)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), transform.rotation);
        }
        else if (enemyMovement.isFacingLeft)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z), transform.rotation);
        }
    }
    private void Die()
    {
        // Handle enemy death (e.g., play animation, disable controls, etc.)
        Debug.Log("Enemy has died.");
        enemyMovement.animator.SetBool("HasDied", true); 
        
        AnimatorStateInfo stateInfo = enemyMovement.animator.GetCurrentAnimatorStateInfo(0);

        // Check if we are in the correct state and the time is >= 1 (finished)
        if (stateInfo.IsName("GoblinKill") && stateInfo.normalizedTime >= 1.0f)
        {
            // Animation is finished
            Destroy(gameObject); // Destroy the enemy game object
        }


    }

    public void Destroy()
    {
        Destroy(gameObject); // Destroy the enemy game object
    }
}
