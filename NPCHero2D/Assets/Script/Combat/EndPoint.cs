using UnityEditor;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= transform.position.x - 1 && player.transform.position.x <= transform.position.x + 1)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            // This quits the game application when built
#else
            Application.Quit();
#endif
        }
    }

   }


