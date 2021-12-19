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
            GameManager.Instance.light = false;
            
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.playerIsHide = false;
            GameManager.Instance.light = true;
            //triggered = true;
            //hostageInteractPanel.SetActive(true);
        }
    }
}
