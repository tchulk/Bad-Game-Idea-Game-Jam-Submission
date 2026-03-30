using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private AudioSource damageSound;
    public float currentHealth;
    private PlayerMovement playerMovement;
    [SerializeField] private Slider healthSlider;

    private void Awake()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth / 100;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageSound.Play();
        playerMovement.animator.SetTrigger("Damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., play animation, disable controls, etc.)
        Debug.Log("Player has died.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
