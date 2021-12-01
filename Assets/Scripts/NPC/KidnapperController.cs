using UnityEngine;

public class KidnapperController : MonoBehaviour
{
    public GameObject player;
    public GameObject kidnapper;
    private Rigidbody rb;

    public float allowedSpeed;
    public float gameOverDistance;

    private RaycastHit shot;
    private Vector3 playerPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerPosition = player.transform.position - transform.position;

        var qTo = Quaternion.LookRotation(player.transform.position - transform.position);
        qTo = Quaternion.Slerp(transform.rotation, qTo, 10 * Time.deltaTime);
        rb.MoveRotation(qTo); //code to look at the player //NOT WORKING YET

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot);

        if (shot.distance <= gameOverDistance)
        {
            Debug.Log("GAME OVER." + gameOverDistance);
            //Time.timeScale = 0; //freeze game
        }

        kidnapper.GetComponent<Animator>().Play("Z_Walk1_InPlace");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}
