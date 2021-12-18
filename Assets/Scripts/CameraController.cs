using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float sensitivity;
    public float smoothFactor = 0.5f;
    public bool isVerticalRotationEnabled = false;

    private Camera cam;
    private float scroll;
    private Vector3 cameraOffset;
    private Quaternion camRotation;

    void Start()
    {
        camRotation = transform.rotation;
        camRotation.x = 20f;
        cam = Camera.main;
        cameraOffset = transform.position - target.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            isVerticalRotationEnabled = !isVerticalRotationEnabled;

        scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll, 30, 150);
    }

    void LateUpdate()
    {
        camRotation.y += Input.GetAxis("Mouse X") * sensitivity;
        camRotation.y = Mathf.Clamp(camRotation.y, -90, 90);

        if (isVerticalRotationEnabled)
        {
            camRotation.x += Input.GetAxis("Mouse Y") * (sensitivity * 1.5f) * (-1);
            camRotation.x = Mathf.Clamp(camRotation.x, -45, 75);
        }

        Vector3 newPos = target.transform.position + cameraOffset;

        transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        //transform.LookAt(target);
    } 
}
