using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private bool yes;
    [SerializeField] private UnityEvent toDoAfterPlay;
    [HideInInspector] private AudioSource sound;

    void Update()
    {
        
    }
}
