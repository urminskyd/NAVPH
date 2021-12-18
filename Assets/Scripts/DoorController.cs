using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorInteractPanel;
    private GameObject panel;

    private bool triggered = false;
    private Collider other;

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        this.other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        if (other.tag == "Player")
            panel.SetActive(false);
    }

    private void Start()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        panel = Instantiate(doorInteractPanel);
        panel.transform.SetParent(canvas.transform, false);
    }

    void Update()
    {
        if (triggered)
        {
            if (other.tag == "Player")
            {
                panel.SetActive(true);
                Animator anim = transform.GetComponentInChildren<Animator>();
                if (Input.GetButton("InteractDoor"))
                    anim.SetTrigger("OpenClose");
            }
        }
    }
}
