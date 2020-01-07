using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetManager : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Can reset all objects in its array.

    [SerializeField] private ObjectReset[] objectResets;
    [SerializeField] private bool reset = false;

    private void FixedUpdate()
    {
        if (reset)
        {
            ResetObjects();
            reset = false;
        }
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
