using System.Collections.Generic;
using UnityEngine;

public class HostageSpawn : MonoBehaviour
{
    public GameObject hostageInteractPanel;
    public GameObject hostagePrefab;
    public List<GameObject> hostagePrefabs;

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HostageSpawnPoint");
        GameManager.Instance.alive = gameObjects.Length;

        for (int x = 0; x < gameObjects.Length; x++)
        {
            int index = Random.Range(0, hostagePrefabs.Count);
            GameObject go = Instantiate(hostagePrefabs[index], gameObjects[x].transform.position, Quaternion.identity);
            HostageController hostageController = go.GetComponentInChildren<HostageController>();
            hostageController.hostageInteractPanel = hostageInteractPanel;
            hostageController.targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
