using UnityEngine;

public class CoffeeMachine : ObjectInteractionManager
{
    private bool isMinigameDone = false;
    private bool minigameStarted = false;
    private bool minigameFinished = false;
    private bool minigameFailed = false;
    private float buttonTimer;
    [SerializeField] private float buttonTimerMax = 2;
    private PlayerInputAction playerInput;

    private bool hasPLayerDoneButton1;
    private bool hasPlayerDoneButton2;
    private bool hasPlayerDoneButton3;


    private void Awake()
    {
        playerInput = new PlayerInputAction();
        buttonTimer = buttonTimerMax;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Interact.performed += ctx => Interact();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonTimer <= 0)
        {
            minigameFailed = true;
        }
        if (minigameStarted)
        {
            buttonTimer -= Time.deltaTime;

        }
        if (minigameFailed)
        {
            Debug.Log("Minigame Failed");
        }
        if (minigameFinished)
        {
            Debug.Log("Minigame finished");
            base.Interact();
        }
    }

    public void PressingButton1()
    {
        hasPLayerDoneButton1 = true;
        buttonTimer = buttonTimerMax;
}
    public void PressingButton2()
    {
        if (hasPLayerDoneButton1) return;
        hasPlayerDoneButton2 = true;
        buttonTimer = buttonTimerMax;
    }
    public void PressingButton3()
    {
        if (hasPLayerDoneButton1) return;
        if (hasPlayerDoneButton2) return;
        hasPlayerDoneButton3 = true;
        buttonTimer = buttonTimerMax;
        minigameStarted = false;
        minigameFinished = true;
    }
    public override void Interact()
    {
        if (minigameFinished) return;
        Debug.Log("You interact with the Coffee Machine.");
        minigameStarted = true;
    }
}
