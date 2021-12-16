using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent ))]
public class HostageController : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject hostageInteractPanel;
    private GameObject panel;
    Animator animator;
    private float distanceToPlayer;

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

        panel = Instantiate(hostageInteractPanel);
        panel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        animator = agent.GetComponentInParent<Animator>();
    }

    void Update()
    {
        agent.destination = targetPlayer.position;
        distanceToPlayer = Vector3.Distance(agent.transform.position, targetPlayer.position);
        agent.speed = (triggered && isFollowAllowed && distanceToPlayer >= 3) ? 2f : 0f;

        if(isFollowAllowed)
            agent.transform.LookAt(targetPlayer.transform);

        if (Input.GetKey(KeyCode.F))
            isFollowAllowed = !isFollowAllowed;

        if (animator != null) //later delete condition
        {
            animator.SetFloat("Speed", agent.speed);
            animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
        }

    }

    public void StopHostage()
    {
        animator.SetFloat("Speed", 0);
        enabled = false;
    }
}
