using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float sensitivity;
    public float smoothFactor = 0.5f;

    private Camera cam;
    private float scroll;

    void Start()
    {
        cam = Camera.main;
        cameraOffset = transform.position - target.transform.position;
    }

    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll, 30, 150);
    }

    void LateUpdate()
    {
        cameraOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * cameraOffset;   //camera rotation by x axis of mouse (left, right)
        cameraOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 4f, Vector3.left) * cameraOffset; //camera rotation by y axis of mouse (up, down)

        Vector3 newPos = target.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(target);
    }
}
