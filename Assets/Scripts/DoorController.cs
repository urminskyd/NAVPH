using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorInteractPanel;
    private GameObject panel;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(true);
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            panel.SetActive(false);
            triggered = false;
        }
    }

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        panel = Instantiate(doorInteractPanel);
        panel.transform.SetParent(canvas.transform, false);
    }

    void Update()
    {
        if (triggered && Input.GetButton("InteractDoor"))
            transform.GetComponentInChildren<Animator>().SetTrigger("OpenClose");
    }
}
