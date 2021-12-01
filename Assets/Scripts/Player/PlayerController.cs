using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float walkSpeed = 2.2F;
    private float runSpeed = 3.5F;

    private bool isRunning = false;
    private Vector3 moveDirection = Vector3.zero;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        isRunning = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= isRunning ? runSpeed : walkSpeed;

        anim.SetFloat("Walk", Input.GetAxis("Vertical"));
        anim.SetBool("Backwards", Input.GetKey(KeyCode.S));
        anim.SetBool("Run", isRunning);

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
