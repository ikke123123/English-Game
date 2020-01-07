using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessTestScript : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Just for testing...
    //------------------------------------------
    //Last Modification Time: 15:15 07/01/2020

    public void Bruh(ObjectCard[] input)
    {
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = input[0].prefab.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
