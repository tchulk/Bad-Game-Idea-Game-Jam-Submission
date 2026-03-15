using UnityEngine;

public class ObjectInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject Transition;
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
