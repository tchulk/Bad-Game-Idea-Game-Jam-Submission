using UnityEngine;

public class CoffeeMachine : ObjectInteractionManager
{
    private bool IsMinigameDone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (IsMinigameDone) return;
        Debug.Log("You interact with the Coffee Machine.");
        base.Interact(); 
        IsMinigameDone = true;
    }
}
