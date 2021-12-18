using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    // direction of character movement
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 moveDirection = Vector3.zero;

    // walk properties
    public float walkSpeed;
    public float runSpeed;
    private bool isRunning = false;
    
    // character sounds and animations
    public List<AudioClip> walkSounds;
    public List<AudioClip> runSounds;
    private AudioSource source;
    private Animator anim;

    public Light playerLight;

    // intensity of light surrounding character  
    public float originalRange;
    public float hideRange;
        
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        isRunning = Input.GetButton("Run") && Input.GetButton("Forward");

        playerLight.range = GameManager.Instance.playerIsHide ? hideRange : originalRange;

        moveDirection = transform.TransformDirection(new Vector3(0, 0, moveVertical));
        moveDirection *= isRunning ? runSpeed : walkSpeed;

        anim.SetFloat("Walk", moveVertical);
        anim.SetBool("Backwards", Input.GetButton("Backward"));
        anim.SetBool("Run", isRunning);

        transform.Rotate(0, moveHorizontal * 4, 0);
        controller.Move(moveDirection * Time.deltaTime);

        if (moveVertical > 0)
            PlaySound(walkSounds);
        else if (isRunning)
            PlaySound(runSounds);
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
