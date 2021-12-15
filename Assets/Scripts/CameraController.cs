using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public float sensitivity;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll, 30, 100);
    }
}
