using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent ))]
public class HostageController : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject hostageInteractPanel;
    private GameObject panel;

    private NavMeshAgent agent;

    private bool triggered = false;
    private bool isFollowAllowed = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
            panel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            panel.SetActive(false);
    }

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        panel = Instantiate(hostageInteractPanel);
        panel.transform.SetParent(canvas.transform, false);
    }

    void Update()
    {
        agent.destination = targetPlayer.position;

        if (Input.GetKey(KeyCode.F))
            isFollowAllowed = !isFollowAllowed;

        if (triggered && isFollowAllowed)
        {
            agent.speed = 2f;
            //hostage.GetComponent<Animator>().Play("Z_Walk1_InPlace");
        }
        else
        {
            agent.speed = 0;
            //hostage.GetComponent<Animator>().Play("Z_Idle");
        }
    }
}
