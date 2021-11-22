using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private float walkSpeed = 2.2F;
    private float runSpeed = 3F;

    bool isRunning = false;

    Vector3 moveDirection = Vector3.zero;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= isRunning ? runSpeed : walkSpeed;

        anim.SetFloat("Walk", Input.GetAxis("Vertical"));
        anim.SetBool("Backwards", Input.GetKey(KeyCode.S));

        isRunning = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);
        anim.SetBool("Run", isRunning);

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
