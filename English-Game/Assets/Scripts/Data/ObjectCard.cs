using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PictureTag { untagged, beach, field, forrest, bird, cat, pug, lying, sitting, standing };

[CreateAssetMenu(fileName = "New Object Card", menuName = "Custom/Object Card")]
public class ObjectCard : ScriptableObject
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script contains data about objects t-
    //hat are assigned to it.

    [SerializeField] public GameObject prefab;
    [Header("First Puzzle")]
    [SerializeField, Tooltip("Can be left empty, is only used for first puzzle. Pick: Blue, Gray, or Green.")] public PictureTag location;
    [SerializeField, Tooltip("Can be left empty, is only used for first puzzle. Pick: Standard, Lighter, or Darker.")] public PictureTag attribute;
    [SerializeField, Tooltip("Can be left empty, is only used for first puzzle. Pick: Normal, Metallic, or Transparent.")] public PictureTag activity;
    [HideInInspector] public List<GameObject> assignedGameObject;

    private void OnEnable()
    {
        assignedGameObject = new List<GameObject>();
        //Checks if the PictureTags are viable
        if (location != PictureTag.untagged)
        {
            if (!(location > PictureTag.untagged && location <= PictureTag.forrest)) LogWarning("Location");
            if (!(attribute >= PictureTag.bird && attribute <= PictureTag.pug)) LogWarning("Attribute");
            if (!(activity >= PictureTag.lying && activity <= PictureTag.standing)) LogWarning("Activity");
        }
    }

    private void LogWarning(string what)
    {
        Debug.LogWarning(what + " is not correct. " + location + " " + attribute + " " + activity);
    }
}