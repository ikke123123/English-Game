using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyParticleColor : MonoBehaviour
{
    [SerializeField] private ParticleSystem.MainModule particles;

    public void SetColor(Material input)
    {
        particles.startColor = input.color;
    }
}
