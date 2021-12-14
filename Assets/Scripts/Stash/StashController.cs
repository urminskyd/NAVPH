using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StashController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerIsHide = true;
            Debug.LogError("Collider skryvam sa");
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerIsHide = false;
            Debug.LogError("Collider uz sa neskryvam");
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
