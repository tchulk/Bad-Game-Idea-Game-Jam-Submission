using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class PlayerAttacking : MonoBehaviour
{
    private PlayerInputAction playerInput;
    [SerializeField] private LayerMask layerMask;

    private float attackTimer;
    [SerializeField] private float attackTimerMax = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private AudioSource HittingSound;
    private PlayerMovement playerMovement;
    private Vector3 directionofAttack;

    private bool AttackAnimation = false;

    [SerializeField] private GameObject mainCamera;
    private Vector3 originalCameraPosition;
    [SerializeField] private float shakeIntensity = 5f;
    [SerializeField] private float shakeDuration = 1f;
    private bool isShaking = false;

    [SerializeField] private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        playerInput = new PlayerInputAction();
        attackTimer = attackTimerMax;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Attack.performed += ctx => Attacking();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackAnimation == true)
        {
            AnimatorStateInfo stateInfo = playerMovement.animator.GetCurrentAnimatorStateInfo(0);

            // Check if we are in the correct state and the time is >= 1 (finished)
            if (stateInfo.IsName("BobAttack") && stateInfo.normalizedTime >= 1.0f)
            {
                // Animation is finished
                playerMovement.animator.SetBool("IsAttack", false);
                AttackAnimation = false;
            }

        }
        if (isShaking)
        {
            StartCoroutine(CameraShakeRoutine());
        }
        attackTimer -= Time.deltaTime;

        if (playerMovement.isFacingRight)
        {
            directionofAttack = transform.right;
            return;
        }
        if (playerMovement.isFacingLeft)
        {
            directionofAttack = -transform.right;
            return;
        }
    }

    private void OnDisable()
    {

        playerInput.Player.Attack.performed -= ctx => Attacking();
        playerInput.Player.Disable();
    }


    public void Attacking()
    {
        if (attackTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionofAttack, 0.5f, layerMask);
            //playerMovement.animator.SetTrigger("Attack");
            playerMovement.animator.SetBool("IsAttack", true);
            AttackAnimation = true;
            isShaking = true;
            impulseSource.GenerateImpulse();

            if (hit)
            {
                if (hit.collider.tag == "Enemy")
                {
                    Debug.Log("Hit Goblin");
                    HittingSound.Play();
                    EnemyHealth enemyHealth = hit.collider.transform.GetComponent<EnemyHealth>();
                    enemyHealth.TakeDamage(damage);
                }
            }
            Debug.DrawRay(transform.position, directionofAttack * 2f, Color.green, 100f);
            attackTimer = attackTimerMax;

        }
    }

    IEnumerator CameraShakeRoutine()
    {
        mainCamera.transform.position = originalCameraPosition + (Random.insideUnitSphere * shakeIntensity);
        yield return new WaitForSeconds(shakeDuration);
        mainCamera.transform.position = originalCameraPosition;
        isShaking = false;
    }

}
