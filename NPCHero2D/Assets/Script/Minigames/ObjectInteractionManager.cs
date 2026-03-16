using UnityEngine;

public class ObjectInteractionManager : MonoBehaviour
{
     private GameObject Transition;

    public virtual void Awake()
    {
        Transition = GameObject.FindGameObjectWithTag("TransitionObject");
        if (Transition == null)
        {
            Debug.LogError("Transition GameObject is not assigned in the inspector.");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        if (Transition.GetComponent<Transition>() != null)
        {
            Transition.GetComponent<Transition>().MinigameDone++;
        }
    }
}
