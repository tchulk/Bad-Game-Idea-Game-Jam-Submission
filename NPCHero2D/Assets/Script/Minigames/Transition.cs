using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public int MinigameDone = 0;
    [SerializeField] private int AllMinigametoDo = 1;
    [SerializeField] private GameObject sword;
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
            Debug.Log("All minigames completed! Spawning Sword...");
            sword.SetActive(true);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in the build index
        }
    }
}
