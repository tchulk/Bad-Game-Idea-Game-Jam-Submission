using UnityEngine;

public class Transition : MonoBehaviour
{
    public int MinigameDone = 0;
    [SerializeField] private int AllMinigametoDo = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameDone >= AllMinigametoDo)
        {
            // Load the next scene or perform the transition
            Debug.Log("All minigames completed! Transitioning to the next scene...");
            // Example: SceneManager.LoadScene("NextSceneName");
        }
    }
}
