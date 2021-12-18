using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public float waitTimeCountdown = -1f;
    public List<AudioClip> sounds;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!source.isPlaying)
        {
            if (waitTimeCountdown < 0f)
            {
                source.clip = sounds[Random.Range(0, sounds.Count)];
                source.Play();
                waitTimeCountdown = Random.Range(1f, 45f);
            }
            else
                waitTimeCountdown -= Time.deltaTime;
        }
    }
}
