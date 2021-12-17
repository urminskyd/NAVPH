using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageSpawn : MonoBehaviour
{
    public GameObject hostageInteractPanel;
    public GameObject hostagePrefab;
    public List<GameObject> hostagePrefabs;

    void Start()
    {
        StartCoroutine(LateStart()); //delayed start lebo inak to hodi do sceny Menu a nie Game
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2);

        int alive = GameManager.Instance.alive;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HostageSpawnPoint");

        for (int x = 0; x < alive; x++)
        {
            int index = Random.Range(0, hostagePrefabs.Count);
            GameObject go = Instantiate(hostagePrefabs[index], gameObjects[x].transform.position, Quaternion.identity);
            HostageController hostageController = go.GetComponentInChildren<HostageController>();
            hostageController.hostageInteractPanel = hostageInteractPanel;
            hostageController.targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
