using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [Header("Roaming Setting")]
    [SerializeField] private float movementRedius = 50f;
    [SerializeField] private float roamingTimerMax = 2f;
    private float roamingTimer = 0f;

    // Variables that change an enemies state
    private bool isRoaming = true;
    private bool isChasing = false;
    private bool isAttacking = false;
    private BoxCollider boxCollider;


    [Header("Chasing Setting")]
    [SerializeField] private GameObject player;
    private Transform playerTransform;

    [Header("Attacking Setting")]
    private EnemyAttacking enemyAttacking;
    [SerializeField] private float attackRange = 2f;
    private float distanceToPlayer = Mathf.Infinity;
    private Vector3 directionOfPlayer;



    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();
        enemyAttacking = GetComponent<EnemyAttacking>();
        playerTransform = player.transform;
        roamingTimer = roamingTimerMax;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        roamingTimer -= Time.deltaTime;
        if (roamingTimer <= 0f & isRoaming == true)
        {
            Roming();
        }
        if (isChasing == true)
        {
            Chasing();
        }
        if (isAttacking == true)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer >= attackRange)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(playerTransform.position);
                isChasing = true;
                isAttacking = false;
            }
            Attacking();
        }

    }

    private Vector3 GetRandomNavMeshPoint(Vector3 Center)
    {
        Vector3 RandomPoint = Center + new Vector3(Random.Range(-movementRedius, movementRedius), 0, Random.Range(-movementRedius, movementRedius));
        NavMeshHit hit;

        if (NavMesh.SamplePosition(RandomPoint, out hit, movementRedius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return GetRandomNavMeshPoint(Center);
        }
    }

    private void Roming()
    {
        Vector3 newDestination = GetRandomNavMeshPoint(transform.position);
        navMeshAgent.SetDestination(newDestination);
        roamingTimer = roamingTimerMax;
    }

    private void OnTriggerEnter(Collider other )
    {
        if (other.gameObject == player)
        {
            isRoaming = false;
            isChasing = true;
        }
    }

    private void Chasing()
    {
        navMeshAgent.SetDestination(playerTransform.position);
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        directionOfPlayer = (playerTransform.position - transform.position).normalized;
        if (distanceToPlayer <= attackRange)
        {
            navMeshAgent.isStopped = true;
            isChasing = false;
            isAttacking = true;
        }
    }

    private void Attacking()
    {
        enemyAttacking.Attacking(directionOfPlayer);
    }
}
