using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script can change the color of the
    //liquid within the bottle.
    //------------------------------------------
    //Last Modification Time: 15:15 09/01/2020

    [Header("Properties")]
    [SerializeField, Range(0.1f, 0.9f)] private float alpha = 0.6f;

    [Header("Properties [Rainbow mode]")]
    [SerializeField] private bool rainbowMode = false;
    [SerializeField, Range(0.1f, 0.9f)] private float refreshRate = 0.1f;
    [SerializeField] private Color[] cycle;

    [Header("Technical")]
    [SerializeField] private MeshRenderer meshRenderer = null;
    [HideInInspector] private int currentNum = 0;
    [HideInInspector] private float time = 0;

    private void Awake()
    {
        if (meshRenderer == null) Debug.LogError("Meshrender was assigned to " + gameObject);
    }

    private void FixedUpdate()
    {
        if (rainbowMode && Time.time >= time)
        {
            time = Time.time + refreshRate;
            ChangeColor(cycle[currentNum]);
            CodeLibrary.IncrementalIncrease(ref currentNum, 1, cycle.Length - 1);
        }
    }

    public void ChangeColor(Color input)
    {
        meshRenderer.material.color = ConvertToTransparent(input);
    }

    private Color ConvertToTransparent(Color input)
    {
        Color newColor = input;
        newColor.a = alpha;
        return newColor;
    }
}
