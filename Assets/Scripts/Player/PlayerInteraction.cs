using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject doorInteractPanel;
    public GameObject hostageInteractPanel;
    private HostageController playerFollow;

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
        if (other.tag == "Door")
            doorInteractPanel.SetActive(false);
        else if (other.tag == "Hostage")
            hostageInteractPanel.SetActive(false);
    }

    private void Update()
    {
        if(triggered)
        {
            if (other.tag == "Door")
            {
                doorInteractPanel.SetActive(true);
                Animator anim = other.GetComponentInChildren<Animator>();
                if (Input.GetKeyDown(KeyCode.E))
                    anim.SetTrigger("OpenClose");
            }
            else if (other.tag == "Hostage")
            {
                hostageInteractPanel.SetActive(true);
                playerFollow = other.gameObject.GetComponentInChildren<HostageController>();
                if (Input.GetKeyDown(KeyCode.F))
                    playerFollow.isFollowingAllowed = !playerFollow.isFollowingAllowed;
            }
        }
    }
}
