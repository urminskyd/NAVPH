using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidnapperController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targetPlayer;
    public float waitTimeCountdown = -1f;
    public List<AudioClip> audioClips;
    private AudioSource source;
    private bool targetIsPlayer = false;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance.playerIsHide)
        {
            agent.destination = targetPlayer.position;
            transform.LookAt(targetPlayer);
            targetIsPlayer = true;
        }
        else
        {
            targetIsPlayer = false;
            GameObject nearestSpawnPoint = null;
            float min = 0;

            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (GameObject point in spawnPoints)
            {
                float dist = Vector3.Distance(point.transform.position, transform.position);
                if (dist < min || min == 0)
                    nearestSpawnPoint = point;
            }
            agent.destination = nearestSpawnPoint.transform.position;
            transform.LookAt(nearestSpawnPoint.transform);
        }

        if (agent.remainingDistance < 1)
        {
            if (targetIsPlayer)
            {
                Time.timeScale = 0; //freeze game
                GameManager.Instance.FinishGame();
            }
            agent.gameObject.SetActive(false);
        }
        else
        {
            transform.GetComponent<Animator>().Play("Z_Run_InPlace");
        }

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
