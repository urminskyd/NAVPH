using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostageSpawn : MonoBehaviour
{
    public GameObject hostageInteractPanel;
    public GameObject hostagePrefab;

    void Start()
    {
        StartCoroutine(LateStart()); //late start lebo inak to hodi do sceny Menu a nie Game
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2);

        int alive = 2;
        //int alive = GameManager.Instance.alive; //odkomentovat len ked je Menu naloadovane inak gameManager neni vytvoreny a NPE
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HostageSpawnPoint");
        for (int x = 0; x < alive; x++)
        {
            GameObject go = Instantiate(hostagePrefab, gameObjects[x].transform.position, Quaternion.identity);
            Rigidbody rigidbody = go.AddComponent<Rigidbody>();
            rigidbody.mass = 100;
            go.tag = "Hostage";

            HostageController hostageController = go.GetComponentInChildren<HostageController>();
            hostageController.hostageInteractPanel = hostageInteractPanel;
            hostageController.targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

            //SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByBuildIndex(1));
        }

    }
}
