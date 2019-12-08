using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private readonly UnityEvent buttonPress;

    [Header("Button Settings")]
    [SerializeField] public readonly float game;

    [HideInInspector] private GameObject button;

    private void Start()
    {
        
    }
}
