using UnityEngine;

public class CoffeeMachine : ObjectInteractionManager
{
    private bool minigameStarted = false;
    private bool minigameFinished = false;
    private bool minigameFailed = false;
    private float buttonTimer;
    [SerializeField] private float buttonTimerMax = 2;
    private GameObject player;
    private PlayerInteractionManager playerInteractionManager;
    private PlayerInputAction playerInput;

    private bool hasPLayerDoneButton1;
    private bool hasPlayerDoneButton2;
    private bool hasPlayerDoneButton3;


    public override void Awake()
    {
        playerInput = new PlayerInputAction();
        buttonTimer = buttonTimerMax;
        base.Awake();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.TimingButtion1.performed += ctx => PressingButton1();
        playerInput.Player.TimingButtion2.performed += ctx => PressingButton2();
        playerInput.Player.TimingButtion3.performed += ctx => PressingButton3();
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

    private void OnDisable()
    {
        playerInput.Player.TimingButtion1.performed -= ctx => PressingButton1();
        playerInput.Player.TimingButtion2.performed -= ctx => PressingButton2();
        playerInput.Player.TimingButtion3.performed -= ctx => PressingButton3();
        playerInput.Player.Disable();
    }

    public void PressingButton1()
    {
        hasPLayerDoneButton1 = true;
        buttonTimer = buttonTimerMax;
}
    public void PressingButton2()
    {
        if (minigameStarted != true) return;
        if (hasPLayerDoneButton1 != true) return;
        hasPlayerDoneButton2 = true;
        buttonTimer = buttonTimerMax;
    }
    public void PressingButton3()
    {
        if (minigameStarted != true) return;
        if (hasPLayerDoneButton1 != true) return;
        if (hasPlayerDoneButton2 != true) return;
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
