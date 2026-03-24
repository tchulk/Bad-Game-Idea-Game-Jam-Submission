using UnityEngine;

public class Bookcase : ObjectInteractionManager
{
    private bool minigameStarted = false;
    private bool minigameFinished = false;

    [SerializeField] private GameObject UI;

    public override void Awake()
    {
       UI.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (minigameStarted)
        {
            UI.SetActive(true);
        }

        if (minigameFinished)
        {
            base.Interact();
        }
    }

    public override void Interact()
    {
        minigameStarted = true;
    }
}
