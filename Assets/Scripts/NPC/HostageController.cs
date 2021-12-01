using UnityEngine;

public class HostageController : MonoBehaviour
{
    public GameObject player;
    public GameObject hostage;

    public float allowedDistance;
    public float allowedSpeed;
    public bool isFollowingAllowed = false;

    private RaycastHit shot;
    private Vector3 playerPosition;
    private Vector3 cubeTriggerPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var qTo = Quaternion.LookRotation(player.transform.position - transform.position);
        qTo = Quaternion.Slerp(transform.rotation, qTo, 10 * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(qTo); //code to look at the player

        cubeTriggerPosition = player.transform.position - transform.parent.position;
        playerPosition = player.transform.position - transform.position;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot);

        if (shot.distance >= allowedDistance && isFollowingAllowed)
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
        Rigidbody parentRigidbody = transform.parent.GetComponent<Rigidbody>();
        parentRigidbody.MovePosition(transform.parent.position + (cubeTriggerPosition * allowedSpeed * Time.deltaTime));

        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}
