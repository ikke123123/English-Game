using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objects : ScriptableObject
{
    [SerializeField] GameObject prefab;
    [HideInInspector] List<GameObject> assignedGameObject;
}
