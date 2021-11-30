using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorInteractPanel;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Door")
        {
            doorInteractPanel.SetActive(true);
            Animator anim = other.GetComponentInChildren<Animator>();
            if (Input.GetKeyDown(KeyCode.E))
                anim.SetTrigger("OpenClose");
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
            doorInteractPanel.SetActive(false);
    }
}
