using UnityEngine;
using System;

public class MainPlayer : MonoBehaviour
{
    private float walkSpeed = 0.2F;
    private float runSpeed = 0.4F;

    float speed = 6.0F;
    float gravity = 20.0F;
    Vector3 moveDirection = Vector3.zero;
    bool isRunning = false;
    public Vector3 jump;
    public bool isGrounded;
    public float jumpForce = 2.0f;

    Animator anim;
    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical") * (isRunning ? runSpeed : walkSpeed));

        anim.SetFloat("Walk", Input.GetAxis("Vertical"));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //NOT WORKING
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        anim.SetBool("Backwards", Input.GetKey(KeyCode.S));

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Run", true);
            isRunning = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Run", false);
            isRunning = false;
        }
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
