using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class KidnapperController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform targetPlayer;

    public List<AudioClip> audioClips;
    private AudioSource source;

    private bool targetIsPlayer = false;
    public float waitTimeCountdown = -1f;
    private float remainingDistanceToTarget;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();

        GameManager.Instance.scoreChanged += OnAliveChange;
    }

    //void Start()
    //{
       
    //}

    private void checkFinish()
    {
        if (!agent.pathPending && agent.remainingDistance < 1 && targetIsPlayer)
        {
            Time.timeScale = 0;
            GameManager.Instance.FinishGame();
        }
    }

    private void goToSpawn()
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

        float distToDestroy = Vector3.Distance(targetPlayer.position, transform.position);

        if (distToDestroy > 20)
        {
            Destroy(GameObject.FindGameObjectWithTag("Kidnapper"));
        }
    }

    void Update()
    {
        checkFinish();
        PlaySound();

        remainingDistanceToTarget = agent.remainingDistance;

        animator.SetBool("Walk", agent.speed < 3 && remainingDistanceToTarget > 2);
        animator.SetBool("Run", agent.speed >= 3 && remainingDistanceToTarget > 2);
        animator.SetBool("Attack", targetIsPlayer && remainingDistanceToTarget <= 2);

        if (!GameManager.Instance.playerIsHide)
        {
            agent.destination = targetPlayer.position;
            targetIsPlayer = true;
        }
        else
        {
            goToSpawn();
        }
      
    }

    void OnAliveChange()
    {
        if (agent)
        {
            agent.speed *= 2f;
        }
    }

    void PlaySound()
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
    }
}
