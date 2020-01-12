using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetManager : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Can reset all objects in its array.

    [SerializeField] private List<ObjectReset> objectResets = new List<ObjectReset>();
    [SerializeField] private bool reset = false;

    [HideInInspector] public bool canRespawnOnDestroy = true;

    private void FixedUpdate()
    {
        if (reset)
        {
            ResetObjects();
            reset = false;
        }
    }

    public void ObjectResetAdd(ObjectReset input)
    {
        objectResets.Add(input);
    }

    public void ObjectResetRemove(ObjectReset input)
    {
        objectResets.Remove(input);
    }

    private void OnDestroy()
    {
        canRespawnOnDestroy = false;
    }

    public void ResetObjects()
    {
        foreach (ObjectReset objectReset in objectResets)
        {
            if (objectReset != null)
            {
                objectReset.ResetPosition();
            }
        }
    }
}
