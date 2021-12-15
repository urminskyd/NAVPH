using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Camera cam;
    public float sensitivity;

    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;

    void Start()
    {
        cam = Camera.main;
        cameraOffset = transform.position - target.transform.position;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll, 30, 110);
    }

    void LateUpdate()
    {
        Vector3 newPos = target.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(target);
    }
}
