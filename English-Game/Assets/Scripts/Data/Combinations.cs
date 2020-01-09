using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combination Card", menuName = "Custom/Combination Card")]
public class Combinations : ScriptableObject
{
    [SerializeField] public ObjectCard[] combinesWith;
    [SerializeField] public ObjectCard result;
}
