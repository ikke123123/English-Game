using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWithinDistance : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Makes sure this particle system only plays
    //if within a certain distance.

    [SerializeField] private float maxDistance;

    private Transform player;
    private ParticleSystem particleSystem;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= maxDistance)
        {
            particleSystem.Stop();
            return;
        }
        if (particleSystem.isPlaying == false)
        {
            particleSystem.Play();
        }
    }
}
