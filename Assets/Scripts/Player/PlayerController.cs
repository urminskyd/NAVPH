using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float walkSpeed = 2.2F;
    private float runSpeed = 3.5F;

    public List<AudioClip> walkSounds;
    public List<AudioClip> runSounds;
    private AudioSource source;

    private bool isRunning = false;
    private Vector3 moveDirection = Vector3.zero;
    Animator anim;

    public Light playerLight;

    public float originalRange;
    public float hideRange;

    void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        isRunning = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W);

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= isRunning ? runSpeed : walkSpeed;

        if (GameManager.Instance.playerIsHide)
        {
            playerLight.range = hideRange;
        } else {
            playerLight.range = originalRange;
        }


        if (Input.GetAxis("Vertical") > 0)
            PlaySound(walkSounds);
        else if (isRunning)
            PlaySound(runSounds);

        anim.SetFloat("Walk", Input.GetAxis("Vertical"));
        anim.SetBool("Backwards", Input.GetKey(KeyCode.S));
        anim.SetBool("Run", isRunning);

        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        controller.Move(moveDirection * Time.deltaTime);
    }

    void PlaySound(List<AudioClip> sounds)
    {
        if (!source.isPlaying)
        {
            source.clip = sounds[Random.Range(0, sounds.Count)];
            source.Play();
        }
    }
}
