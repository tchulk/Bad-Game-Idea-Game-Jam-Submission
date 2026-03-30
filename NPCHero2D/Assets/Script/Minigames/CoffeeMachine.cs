using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI buttonToClick;

    private bool DoOnce = false;

    [SerializeField] private TextMeshProUGUI tutorialText;
    private float tutorialTimer = 5;
    private bool tutorialActive = false;


    public override void Awake()
    {
        playerInput = new PlayerInputAction();
        buttonTimer = buttonTimerMax;
        timerText.gameObject.SetActive(false);
        buttonToClick.gameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);
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
            if (tutorialActive)
            {
            tutorialText.gameObject.SetActive(true);
            tutorialTimer -= Time.deltaTime;
                if (tutorialTimer <= 0)
                {
                    tutorialText.gameObject.SetActive(false);
                tutorialActive = false;
                minigameStarted = true;
            }
        }
        if (buttonTimer <= 0 && minigameStarted)
        {
            minigameFailed = true;
        }
        if (minigameStarted)
        {
            buttonTimer -= Time.deltaTime;
            timerText.gameObject.SetActive(true);
            timerText.text = buttonTimer.ToString("F2");
            buttonToClick.gameObject.SetActive(true);
                if (hasPLayerDoneButton1 != true)
                {
                    buttonToClick.text = "Press " + playerInput.Player.TimingButtion1.GetBindingDisplayString(0);
                }
                else if (hasPlayerDoneButton2 != true)
                {
                    buttonToClick.text = "Press " + playerInput.Player.TimingButtion2.GetBindingDisplayString(0);
            }
                else if (hasPlayerDoneButton3 != true)
                {
                    buttonToClick.text = "Press " + playerInput.Player.TimingButtion3.GetBindingDisplayString(0);
            }
        }
        if (minigameFailed)
        {
            Debug.Log("Minigame Failed");
            timerText.gameObject.SetActive(false);
            buttonToClick.gameObject.SetActive(false);
            minigameStarted = false;
             hasPLayerDoneButton1 = false;
             hasPlayerDoneButton2 = false;
             hasPlayerDoneButton3 = false;
            minigameFailed = false;
            buttonTimer = buttonTimerMax;
        }
        if (minigameFinished == true && DoOnce == false)
        {
            Debug.Log("Minigame finished");
            timerText.gameObject.SetActive(false);
            buttonToClick.gameObject.SetActive(false);
            DoOnce = true;
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
        tutorialActive = true;
    }
}
