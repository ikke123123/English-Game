using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackPlayer : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1.0f)] private float volume = 0.5f;
    [SerializeField, Range(0.01f, 1f)] private float step = 0.01f;

    [Header("Prefab Settings")]
    [SerializeField] private AudioSource[] layers;
    
    private List<AudioSource> fadeIn = new List<AudioSource>();

    private void OnEnable()
    {
        foreach (AudioSource audioSource in layers) audioSource.Play();
        EnableLayer(0);
    }

    private void FixedUpdate()
    {
        foreach (AudioSource audioSource in fadeIn)
        {
            audioSource.volume += step;
            if (audioSource.volume > volume)
            {
                audioSource.volume = volume;
                fadeIn.Remove(audioSource);
            }
        }
    }

    public void EnableLayer(int input)
    {
        layers[input].volume = volume;
    }
}
