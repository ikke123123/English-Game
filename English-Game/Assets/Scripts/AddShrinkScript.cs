using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AddShrinkScript : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [HideInInspector] private List<ObjectThing> objects = new List<ObjectThing>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Shrinkable")
        {
            objects.Add(new ObjectThing { collisionObject = collider.gameObject, minSize = CodeLibrary.ScaleVector3Modifier(collider.transform.localScale, 0.1f) });
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (ObjectThing objectthing in objects) if (objectthing.collisionObject == other.gameObject)
            {
                other.gameObject.transform.localScale = CodeLibrary.ClampVector3(CodeLibrary.ScaleVector3Sum(other.gameObject.transform.localScale, -0.1f) , objectthing.minSize, CodeLibrary.MaxVector3Size());
            }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectThing toRemove = null;
        foreach (ObjectThing objectthing in objects) if (objectthing.collisionObject == other.gameObject) toRemove = objectthing;
        if (toRemove != null) objects.Remove(toRemove);
    }
}

public class ObjectThing 
{
    [HideInInspector] public GameObject collisionObject;
    [HideInInspector] public Vector3 minSize;
}

