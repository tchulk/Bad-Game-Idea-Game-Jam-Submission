using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Transition : MonoBehaviour
{
    public int MinigameDone = 0;
    [SerializeField] private int AllMinigameToDo = 2;
    [SerializeField] private GameObject sword;
    [SerializeField] private TextMeshProUGUI SwordText;

    private void Awake()
    {
        SwordText.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameDone >= AllMinigameToDo)
        {
            // Load the next scene or perform the transition
            Debug.Log("All minigames completed! Spawning Sword...");
            sword.SetActive(true);
            SwordText.gameObject.SetActive(true);
        }
    }
}
