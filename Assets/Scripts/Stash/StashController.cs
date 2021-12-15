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
            
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerIsHide = false;
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }
}
