using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combination Card", menuName = "Custom/Combination Card")]
public class Combinations : ScriptableObject
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [SerializeField] public ObjectCard[] combinesWith;
    [SerializeField] public ObjectCard result;
    [SerializeField] public Material material;
}
