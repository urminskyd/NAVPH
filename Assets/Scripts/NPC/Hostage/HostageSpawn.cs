using System.Collections.Generic;
using UnityEngine;

public class HostageSpawn : MonoBehaviour
{
    public GameObject hostageInteractPanel;
    public GameObject hostagePrefab;
    public List<GameObject> hostagePrefabs;

    void Start()
    {
        int alive = GameManager.Instance.alive;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HostageSpawnPoint");
        if (gameObjects.Length != alive)
            Debug.LogWarning("Number of hostage spawn points doesn't match with number of hostages!");

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
