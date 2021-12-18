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

    public int hostageSpeed;
    private bool isFollowAllowed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enabled)
            panel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            panel.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButton("InteractHostage"))
        {
            Debug.Log("NAHANAM");
            isFollowAllowed = !isFollowAllowed;
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
        distanceToPlayer = Vector3.Distance(agent.transform.position, targetPlayer.position);

        agent.destination = targetPlayer.position;
        agent.speed = (isFollowAllowed && distanceToPlayer >= agent.stoppingDistance) ? hostageSpeed : 0f;

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
