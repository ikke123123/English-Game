using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinations : ScriptableObject
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script contains the possible combina-
    //tions that you can do with other objects.

    [SerializeField] public ObjectCard[] combinesWith;
    [SerializeField] public ObjectCard result;
}
