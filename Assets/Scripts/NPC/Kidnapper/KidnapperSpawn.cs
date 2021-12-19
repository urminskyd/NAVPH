using System.Collections.Generic;
using UnityEngine;

public class KidnapperSpawn : MonoBehaviour
{
    public GameObject kidnapperPrefab;
    private GameObject kidnapper;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerIsHide = false;

            //bool v = Random.value > 0.5;
            if (!GameObject.FindGameObjectWithTag("Kidnapper"))
            { 
                GameObject spawnPoint = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"))
                    .Find(g => g.transform.IsChildOf(transform));
                kidnapperPrefab.transform.position = spawnPoint.transform.position;

                kidnapper = Instantiate(kidnapperPrefab, spawnPoint.transform.position, Quaternion.identity);
                kidnapper.GetComponent<KidnapperController>().targetPlayer = other.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            GameManager.Instance.playerIsHide = true;
    }
}
