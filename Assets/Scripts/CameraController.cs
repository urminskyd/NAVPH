using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Update()
    {
        Vector3 temp = target.transform.position;
        temp.x = temp.x - offset.x;
        temp.y = offset.y;
        temp.z = temp.z - offset.z;

        transform.position = temp;

    }
}
