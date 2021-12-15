using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KidnapperController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform targetPlayer;
    public float waitTimeCountdown = -1f;
    public List<AudioClip> audioClips;
    private AudioSource source;
    private bool targetIsPlayer = false;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance.playerIsHide)
        {
            agent.destination = targetPlayer.position;
            //agent.SetDestination(targetPlayer.transform.position);

            //Debug.Log(agent.nextPosition);
            //Debug.Log(targetPlayer.transform.position);
            //transform.LookAt(targetPlayer);
            targetIsPlayer = true;
        }
        else
        {
            //float dist = Vector3.Distance(agent.destination, transform.position);
            float min = 0;

            //if (dist < min || min == 0)
            //{

            //}

            targetIsPlayer = false;
            GameObject nearestSpawnPoint = null;

            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            foreach (GameObject point in spawnPoints)
            {
                float dist = Vector3.Distance(point.transform.position, transform.position);
                if (dist < min || min == 0)
                    nearestSpawnPoint = point;
            }
            agent.destination = nearestSpawnPoint.transform.position;
            //transform.LookAt(nearestSpawnPoint.transform);
        }

        if (agent.remainingDistance < 1)
        {
            //Debug.LogError("CHYTENY");
            //if (targetIsPlayer)
            //{
            //    Time.timeScale = 0; //freeze game
            //    GameManager.Instance.FinishGame();
            //}
            //agent.gameObject.SetActive(false);
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
