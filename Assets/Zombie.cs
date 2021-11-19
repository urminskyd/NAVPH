using UnityEngine;

public class Zombie : MonoBehaviour
{
    private const float V = 0.2F;
    float speed = 6.0F;
    float gravity = 20.0F;
    Vector3 moveDirection = Vector3.zero;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical") * V);

        anim.SetFloat("Speed", Input.GetAxis("Vertical"));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
            anim.SetBool("Run", true);
        else if (Input.GetKey(KeyCode.Q))
            anim.SetBool("Run", false);

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
