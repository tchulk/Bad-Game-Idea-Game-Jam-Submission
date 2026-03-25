using UnityEngine;
using UnityEngine.UI;

public class Bookcase : ObjectInteractionManager
{
    private bool minigameStarted = false;
    private bool minigameFinished = false;

    [SerializeField] private GameObject UI;

    public int NumberOfBooks = 0;
    private bool DoOnce = false;
    public override void Awake()
    {
       UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (minigameStarted)
        {
            UI.SetActive(true);
        }

        if ( NumberOfBooks == 3)
        {
            minigameFinished = true;
        }

        if (minigameFinished == true && DoOnce == false)
        {
            minigameStarted = false;
            DoOnce = true;
            UI.SetActive(false);
            base.Interact();
        }
    }

    public override void Interact()
    {
        if (minigameFinished) return;
        minigameStarted = true;
    }
}
