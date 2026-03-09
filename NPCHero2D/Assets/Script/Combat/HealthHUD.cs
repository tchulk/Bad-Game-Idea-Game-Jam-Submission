using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Awake()
    {
       playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
            healthSlider.value = playerHealth.currentHealth;

        else
            Debug.LogWarning("PlayerHealth component not found on the player object.");
    }
}
