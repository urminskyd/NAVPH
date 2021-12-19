using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform levelView;

    public float sensitivity;
    public float smoothFactor = 0.5f;
    public bool isViewEnabled = false;

    public float distance = 10.0f;
    public float height = 5.0f; // the height we want the camera to be above the target

    private Camera cam;
    private float scroll;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            isViewEnabled = !isViewEnabled;

        if (!isViewEnabled)
        {
            scroll = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + scroll, 30, 100);
        }
    }

    void LateUpdate()
    {
        if (isViewEnabled)
        {
            transform.position = levelView.transform.position + new Vector3(0, 70, 0);
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.x = 90;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            // Calculate the current rotation angles
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3.0f * Time.deltaTime); // Damp the rotation around the y-axis
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2.0f * Time.deltaTime); // Damp the height

            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0); // Convert the angle into a rotation

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
            transform.LookAt(target);
        }
    }
}
