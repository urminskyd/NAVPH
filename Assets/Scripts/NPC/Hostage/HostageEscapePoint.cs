using UnityEngine;

public class HostageEscapePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hostage")
        {
            Debug.Log("Hostage entered escape zone.");
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hostage")
        {
            GameManager.Instance.RescueHostage();
            Debug.Log("Hostage is rescued!");
            other.GetComponentInChildren<HostageController>().StopHostage();
        }
    }
}
