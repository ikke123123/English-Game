using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyParticleColor : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void SetColor(Material input)
    {
        ParticleSystem.MainModule main = particles.main;
        main.startColor = input.color;
    }
}
