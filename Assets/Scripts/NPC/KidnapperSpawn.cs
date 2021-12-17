using System.Collections.Generic;
using UnityEngine;

public class KidnapperSpawn : MonoBehaviour
{
    public GameObject kidnapper;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            triggered = true;
    }

    private void OnTriggerExit()
    {
        triggered = false;
        kidnapper.SetActive(false); //if player gets out of trigger space, the kidnapper will dissapear
    }

    void Update()
    {
        if (triggered && !kidnapper.activeSelf && !GameManager.Instance.playerIsHide)
        {
            GameObject spawnPoint = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"))
                .Find(g => g.transform.IsChildOf(transform));
            kidnapper.transform.position = spawnPoint.transform.position;
            kidnapper.SetActive(true);
            Debug.Log("SPAWN kiddnaper");
        }
    }

}
