using UnityEngine;
using System.Collections.Generic;

public class KidnapperController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    private AudioSource source;

    public float allowedSpeed;
    public float gameOverDistance;

    public float waitTimeCountdown = -1f;
    public List<AudioClip> audioClips;

    private RaycastHit shot;
    private Vector3 playerPosition;
    private Quaternion qTo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        source.Play();
    }

    void Update()
    {
        if (!source.isPlaying)
        {
            if (waitTimeCountdown < 0f)
            {
                source.clip = audioClips[Random.Range(0, audioClips.Count)];
                source.Play();
                waitTimeCountdown = Random.Range(1f, 5f);
            }
            else
                waitTimeCountdown -= Time.deltaTime;
        }

        if (!GameManager.Instance.playerIsHide)
        {
            transform.GetComponent<Animator>().Play("Z_Run_InPlace");

            qTo = Quaternion.LookRotation(player.transform.position - transform.position);
            qTo = Quaternion.Slerp(transform.rotation, qTo, 10 * Time.deltaTime);

            playerPosition = player.transform.position - transform.position;
            playerPosition = playerPosition.normalized * allowedSpeed; //normalize vector, so hostage speed is not based on distance

            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot);

            if (shot.distance > 0 && shot.distance <= gameOverDistance)
            {
                transform.GetComponent<Animator>().Play("Z_Idle");
                Time.timeScale = 0; //freeze game
                GameManager.Instance.FinishGame();

            }
        }
       
        PlayerPrefs.Save();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(qTo.normalized); //look at the player
        rb.MovePosition(transform.position + (playerPosition * allowedSpeed * Time.deltaTime));
    }
}
