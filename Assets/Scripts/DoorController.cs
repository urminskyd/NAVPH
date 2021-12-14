using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorInteractPanel;

    private bool triggered = false;
    private Collider other;

    private void OnTriggerStay(Collider other)
    {
        triggered = true;
        this.other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        if (other.tag == "Player")
            doorInteractPanel.SetActive(false);
    }

    void Update()
    {
        if (triggered)
        {
            if (other.tag == "Player")
            {
                doorInteractPanel.SetActive(true);
                Animator anim = transform.GetComponentInChildren<Animator>();
                if (Input.GetKeyDown(KeyCode.E))
                    anim.SetTrigger("OpenClose");
            }
        }
    }
}
