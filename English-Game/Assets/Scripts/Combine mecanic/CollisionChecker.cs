using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public ObjectTypeScript boxType;

    //Finds what boxtype (object/ingerdient) is in the collision checker. 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "GrabbableObject") { 
            boxType = other.GetComponent<ObjectTypeScript>();
            Debug.Log(boxType.boxColor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GrabbableObject")
        {
            boxType = null;
        }
    }
}
