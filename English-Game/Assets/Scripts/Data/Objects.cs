using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Object Card", menuName = "Custom/Object Card")]
public class Objects : ScriptableObject
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [SerializeField] public GameObject prefab;
    [SerializeField] public ObjectCombination[] combinations;
    [HideInInspector] public List<GameObject> assignedGameObject;

    private void OnEnable()
    {
        assignedGameObject = new List<GameObject>();
    }
}

[System.Serializable]
public class ObjectCombination
{
    [SerializeField] public Objects[] combinesWith;
    [SerializeField] public Objects result;
}
