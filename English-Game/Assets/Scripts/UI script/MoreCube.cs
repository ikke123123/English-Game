using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCube : MonoBehaviour
{
    public GameObject cubePrefab;


    public void Cube()
    {
        Instantiate(cubePrefab);
    }
}
