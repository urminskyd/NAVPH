using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject hostage;

    public float allowedDistance;
    public float allowedSpeed;

    private RaycastHit shot;
    private Vector3 playerPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(player.transform);
        playerPosition = player.transform.position - transform.position;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot);

        if (shot.distance >= allowedDistance)
        {
            allowedSpeed = 1.5f;
            //hostage.GetComponent<Animation>().Play("Run");
        }
        else
        {
            allowedSpeed = 0;
            //hostage.GetComponent<Animation>().Play("Idle");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}
