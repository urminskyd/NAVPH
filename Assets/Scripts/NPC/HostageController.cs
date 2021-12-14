using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent ))]
public class HostageController : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject hostageInteractPanel;
    
    private NavMeshAgent agent;

    private bool triggered = false;
    private bool isFollowAllowed = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
            hostageInteractPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            hostageInteractPanel.SetActive(false);
    }

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        var qTo = Quaternion.LookRotation(targetPlayer.position - targetPlayer.position);
        qTo = Quaternion.Slerp(targetPlayer.rotation, qTo, 10 * Time.deltaTime);
        targetPlayer.GetComponent<Rigidbody>().MoveRotation(qTo); //code to look at the player

        agent.destination = targetPlayer.position;

        if (Input.GetKeyDown(KeyCode.F))
            isFollowAllowed = !isFollowAllowed;

        if (triggered && isFollowAllowed)
        {
            agent.speed = 1.5f;
            //hostage.GetComponent<Animator>().Play("Z_Walk1_InPlace");
        }
        else
        {
            agent.speed = 0;
            //hostage.GetComponent<Animator>().Play("Z_Idle");
        }
    }
}
