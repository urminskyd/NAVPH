using UnityEngine;

public class KidnapperController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;

    public float allowedSpeed;
    public float gameOverDistance;

    private RaycastHit shot;
    private Vector3 playerPosition;
    private Quaternion qTo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerPrefs.SetInt("gameOver", 0);
        transform.GetComponent<Animator>().Play("Z_Run_InPlace");

        qTo = Quaternion.LookRotation(player.transform.position - transform.position);
        qTo = Quaternion.Slerp(transform.rotation, qTo, 10 * Time.deltaTime);

        playerPosition = player.transform.position - transform.position;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot);

        //Debug.Log(shot.distance);
        if (shot.distance > 0 && shot.distance <= gameOverDistance)
        {
            Debug.Log("GAME OVER." + gameOverDistance);
            transform.GetComponent<Animator>().Play("Z_Idle");
            Time.timeScale = 0; //freeze game

            Debug.Log("Nastavujem gameOver na 1");
            PlayerPrefs.SetInt("gameOver", 1);
            
        }
        PlayerPrefs.Save();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(qTo.normalized); //look at the player
        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}