using UnityEngine;
using UnityEngine.AI;

public class HostageController : MonoBehaviour
{
    public Transform targetPlayer;
    public GameObject hostageInteractPanel;
    private GameObject panel;
    private Animator animator;
    private float distanceToPlayer;

    private NavMeshAgent agent;

    public float hostageSpeed;
    private bool isFollowAllowed = false;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enabled)
        {
            triggered = true;
            panel.SetActive(true);
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

    void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = agent.GetComponentInParent<Animator>();
        panel = Instantiate(hostageInteractPanel);
        panel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    void Update()
    {
        if (Input.GetButtonDown("InteractHostage"))
        {
            if (triggered)
            {
                isFollowAllowed = !isFollowAllowed;
            }
            else
            {
                isFollowAllowed = false;
            }
        }

        distanceToPlayer = Vector3.Distance(agent.transform.position, targetPlayer.position);

        agent.destination = targetPlayer.position;
        agent.speed = (enabled && isFollowAllowed && distanceToPlayer >= agent.stoppingDistance) ? hostageSpeed : 0f;

        animator.SetFloat("Speed", agent.speed);
        animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
    }

    public void StopHostage()
    {
        animator.SetFloat("Speed", 0);
        enabled = false;
    }

    public void HostageDeath()
    {
        animator.SetBool("Die", true);
        enabled = false;
    }
}
