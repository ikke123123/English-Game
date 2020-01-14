using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoundCardPlayer : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }

    public void StartPlaying(Soundcard input)
    {
        audioSource.clip = input.clip;
        audioSource.loop = false;
        audioSource.volume = input.volume;
        audioSource.spatialBlend = 1;
        audioSource.Play();
    }
}
