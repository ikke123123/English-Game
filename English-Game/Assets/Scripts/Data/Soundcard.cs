﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soundcard", menuName = "Custom/Soundcard")]
public class Soundcard : ScriptableObject
{
    [HideInInspector] public enum State { off, playing, rampOn, rampOff };
    [SerializeField] public AudioClip clip;
    [SerializeField, Range(0.1f, 1)] public float volume;
    [SerializeField, Range(-1, -0.01f), Tooltip("For every Fixed Update how much should the volume decrease? -1 is instantly turned off, -0.01 is a slow ramp-off. This will only be used if the audio is triggered to turn off")] public float rampOffStep;
    [SerializeField, Range(0.01f, 1), Tooltip("For every Fixed Update how much should the volume increase? 1 is instantly turned on, 0.01 is a slow turn-on. This will always be activated when the audio clip is triggered to play")] public float rampOnStep;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public State state = State.off;
    [HideInInspector] public bool paused = false;
}