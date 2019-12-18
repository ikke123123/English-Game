using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessTestScript : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    public void Bruh(ObjectCard[] input)
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = input[0].prefab.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
