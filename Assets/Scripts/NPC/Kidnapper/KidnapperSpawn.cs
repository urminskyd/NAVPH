using System.Collections.Generic;
using UnityEngine;

public class KidnapperSpawn : MonoBehaviour
{
    private GameObject kidnapper;
    private bool triggered = false;

    private void Awake()
    {
        kidnapper = GameObject.Find("Kidnapper");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggered = true;
        }
    }

    private void OnTriggerExit()
    {
        triggered = false;
        kidnapper.SetActive(false); //if player gets out of trigger space, the kidnapper will dissapear
    }

    void Update()
    {
        if (triggered && !kidnapper.activeInHierarchy && !GameManager.Instance.playerIsHide)
        {
            GameObject spawnPoint = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"))
                .Find(g => g.transform.IsChildOf(transform));
            kidnapper.transform.position = spawnPoint.transform.position;
            kidnapper.SetActive(true);
        }
    }

}
