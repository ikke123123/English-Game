using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignToObjectID : MonoBehaviour
{
    [SerializeField] private Objects objectToAssignTo;

    private void Awake()
    {
        objectToAssignTo.assignedGameObject.Add(gameObject);
    }

    private void OnDestroy()
    {
        objectToAssignTo.assignedGameObject.Remove(gameObject);
    }
}
