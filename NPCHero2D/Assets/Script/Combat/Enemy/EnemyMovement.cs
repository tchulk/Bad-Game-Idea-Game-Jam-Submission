using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isRoaming;
    private bool isChasing;
    private bool isAttacking;

    private bool isFacingRight = true;
    private bool isFacingLeft = true;
    private Vector2 currentDirection;
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D trigger;
    private Rigidbody2D rd;
    private GameObject player;

    private float randomTimer;

    private void Awake()
    {
        int Ran = Random.Range(1, 3);
        rd = GetComponent<Rigidbody2D>();
        switch (Ran)
        {
            case 1:
                currentDirection = new Vector2(-1, 0);
                isFacingLeft = true;
                break;
            case 2:
                currentDirection = new Vector2(1, 0);
                isFacingRight = true;
                break;
            default:
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
        transform.Translate(currentDirection * speed * Time.deltaTime);
        if (randomTimer <= 0 & isRoaming)
        {
            Roaming();
        }
        if (isChasing)
        {
            Chasing();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
            isFacingLeft = true;
            isFacingRight = false;
            randomTimer = Random.Range(1, 6);
            return;
        }
        if (isFacingLeft)
        {
            currentDirection = new Vector2(1, 0);
            isFacingRight = true;
            isFacingLeft = false;
            randomTimer = Random.Range(1, 6);
            return;
        }
    }

    private void Chasing()
    {
        currentDirection = -player.transform.position.normalized;
    }
}
