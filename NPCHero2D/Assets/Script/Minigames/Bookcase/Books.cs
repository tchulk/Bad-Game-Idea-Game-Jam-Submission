using UnityEngine;

public class Folder : MonoBehaviour
{
    [SerializeField] private Transform WhereBookShouldBe;
    [SerializeField] private GameObject Bookcase;

    private bool DoOnce = false;
    private bool isInPosition = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {

        if (isInPosition == true)
        {
            transform.position = WhereBookShouldBe.position;
        }
        if(DoOnce == false)
          {
            if (transform.position.x >= WhereBookShouldBe.position.x - 10 && transform.position.x <= WhereBookShouldBe.position.x + 10)
            {
                if (Bookcase.GetComponent<Bookcase>() != null)
                {
                    Bookcase.GetComponent<Bookcase>().NumberOfBooks++;
                    DoOnce = true;
                    isInPosition = true;
                    Debug.Log("Book added");
                }
            }
        }
    }
}
