using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class KidnapperController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform targetPlayer;
    public float waitTimeCountdown = -1f;
    public List<AudioClip> audioClips;
    private AudioSource source;
    private bool targetIsPlayer = false;
    private Animator animator;

    void Awake()
    {
        GameManager.Instance.scoreChanged += OnAliveChange;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance.playerIsHide)
        {
            agent.destination = targetPlayer.position;
            targetIsPlayer = true;
        }
        else
        {
            targetIsPlayer = false;
            GameObject nearestSpawnPoint = null;

            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (GameObject point in spawnPoints)
            {
                float dist = Vector3.Distance(point.transform.position, transform.position);
                float min = 0;
                if (dist < min || min == 0)
                    nearestSpawnPoint = point;
            }
            agent.destination = nearestSpawnPoint.transform.position;
        }

        if (agent.remainingDistance < 2)
        {
            Debug.LogError("CHYTENY");
            //if (targetIsPlayer)
            //{
            //    Time.timeScale = 0; //freeze game
            //    GameManager.Instance.FinishGame();
            //}
            //agent.gameObject.SetActive(false);
        }

        animator.SetBool("Walk", agent.speed < 3);
        animator.SetBool("Run", agent.speed >= 3);
        animator.SetBool("Attack", agent.remainingDistance < 2);

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
    }

    void OnAliveChange()
    {
        agent.speed *= 2f;
    }
}
