using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacking : MonoBehaviour
{
    private float attackTimer;
    [SerializeField] private float attackTimerMax = 2;
    private GameObject player;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        attackTimer = attackTimerMax;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
    public void Attacking(Vector3 directionOfPlayer)
    {
      if(attackTimer <= 0) 
        {
            if (Physics.Raycast(transform.position, directionOfPlayer, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.transform.parent.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Attacking");
                    playerHealth.TakeDamage(20);
                }
            }
            attackTimer = attackTimerMax;
        }
    }
}
