using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PictureTag { untagged, blue, gray, green, standard, lighter, darker, normal, metallic, transparent };

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
    [Header("Second Puzzle")]
    [SerializeField, Tooltip("Can be left empty, is only used for second puzzle.")] public Combinations[] combinations;
    [HideInInspector] public List<GameObject> assignedGameObject;

    private void OnEnable()
    {
        assignedGameObject = new List<GameObject>();
        //Checks if the PictureTags are viable
        if (location != PictureTag.untagged)
        {
            if (!(location > PictureTag.untagged && location <= PictureTag.green)) Debug.LogWarning("Location is not correct.");
            if (!(attribute >= PictureTag.standard && attribute <= PictureTag.darker)) Debug.LogWarning("Attribute is not correct.");
            if (!(activity >= PictureTag.normal && activity <= PictureTag.transparent)) Debug.LogWarning("Activity is not correct.");
        }
    }
}