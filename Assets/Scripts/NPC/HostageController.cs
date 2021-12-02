using UnityEngine;

public class HostageController : MonoBehaviour
{
    public GameObject player;
    public GameObject hostage;
    public GameObject hostageInteractPanel;

    public float allowedDistance;
    public float allowedSpeed;
    private bool triggered = false;
    private bool isFollowAllowed = false;

    private RaycastHit shot;
    private Vector3 playerPosition;
    private Vector3 playerPositionRelativeToHostage;
    private Rigidbody rb;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var qTo = Quaternion.LookRotation(player.transform.position - hostage.transform.position);
        qTo = Quaternion.Slerp(hostage.transform.rotation, qTo, 10 * Time.deltaTime);
        hostage.GetComponent<Rigidbody>().MoveRotation(qTo); //code to look at the player

        playerPositionRelativeToHostage = player.transform.position - hostage.transform.position;
        playerPosition = player.transform.position - transform.position;
        Physics.Raycast(hostage.transform.position, hostage.transform.TransformDirection(Vector3.forward), out shot);

        if (Input.GetKeyDown(KeyCode.F))
            isFollowAllowed = !isFollowAllowed;

        if (shot.distance >= allowedDistance && triggered && isFollowAllowed)
        {
            allowedSpeed = 0.5f;
            //hostage.GetComponent<Animator>().Play("Z_Walk1_InPlace");
        }
        else
        {
            allowedSpeed = 0;
            //hostage.GetComponent<Animator>().Play("Z_Idle");
        }
    }

    private void FixedUpdate()
    {
        Rigidbody hostageRigidbody = hostage.transform.GetComponent<Rigidbody>();
        hostageRigidbody.MovePosition(hostage.transform.position + (playerPositionRelativeToHostage * allowedSpeed * Time.deltaTime));

        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}
