using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isRoaming;
    private bool isChasing;
    private bool isAttacking;

    public bool isFacingRight;
    public bool isFacingLeft;
    private Vector2 currentDirection;
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D trigger;
    private Rigidbody2D rd;
    private GameObject player;

    private float randomTimer;

    private bool DoNotCheckLeft = false;
    private bool DoNotCheckRight = false;
    private EnemyAttacking enemyAttacking;

    [SerializeField] private float AttackRange = 2;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        int Ran = Random.Range(1, 3);
        rd = GetComponent<Rigidbody2D>();
        enemyAttacking = GetComponent<EnemyAttacking>();
        // ensure only one facing flag is set
        isFacingRight = false;
        isFacingLeft = false;
        switch (Ran)
        {
            case 1:
                currentDirection = new Vector2(-1, 0);
                gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                animator.SetBool("IsMoving", true);
                isFacingLeft = true;
                break;
            case 2:
                currentDirection = new Vector2(1, 0);
                gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 0));
                animator.SetBool("IsMoving", true);
                isFacingRight = true;
                break;
            default:
                currentDirection = new Vector2(1, 0);
                gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 0));
                animator.SetBool("IsMoving", true);
                isFacingRight = true;
                break;
        }
        randomTimer = Random.Range(1, 6);
        isRoaming = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        randomTimer -= Time.deltaTime;

        if (randomTimer <= 0 && isRoaming)
        {
            Roaming();
        }

        if (isChasing)
        {
            Chasing();
        }

        if (isAttacking)
        {
            Attacking();
        }
    }

    private void FixedUpdate()
    {
        // Move using Rigidbody2D to respect physics and avoid transform/physics conflicts
        if (rd != null)
        {
            Vector2 movement = currentDirection * speed * Time.fixedDeltaTime;
            rd.MovePosition(rd.position + movement);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRoaming = false;
            isChasing = true;
        }
    }
    private void Roaming()
    {
        if (isFacingRight)
        {
            currentDirection = new Vector2(-1, 0);
            animator.SetBool("IsMoving", true);
            isFacingLeft = true;
            isFacingRight = false;
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
            randomTimer = Random.Range(1, 6);
            return;
        }
        if (isFacingLeft)
        {
            currentDirection = new Vector2(1, 0);
            animator.SetBool("IsMoving", true);
            isFacingRight = true;
            isFacingLeft = false;
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 0));
            randomTimer = Random.Range(1, 6);
            return;
        }
    }

    private void Chasing()
    {
        if (player == null) return;

        Vector3 directionOfPlayer = player.transform.position - transform.position;

        float DistanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        

        if (DistanceFromPlayer <= AttackRange)
        {
            isChasing = false;
            isAttacking = true;
        }

        if (directionOfPlayer.x > 0f && DoNotCheckLeft != true)
        {
            currentDirection = new Vector2(1, 0);
            isFacingRight = true;
            isFacingLeft = false;
            gameObject.transform.Rotate(new Vector3(0, 180, 0));
            DoNotCheckRight = true;
        }
        if (directionOfPlayer.x < 0f && DoNotCheckRight != true)
        {
            currentDirection = new Vector2(-1, 0);
            isFacingLeft = true;
            isFacingRight = false;
            gameObject.transform.Rotate(new Vector3(0, 0, 0));
            DoNotCheckLeft = true;
        }
    }

    private void Attacking()
    {
        enemyAttacking.Attacking();
    }
}
