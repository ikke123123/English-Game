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
    //Last Modification Time: 10:56 08/01/2020

    [SerializeField] private bool rainbowMode = false;
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
            time = Time.time + 0.5f;
            switch (currentNum)
            {
                case 0:
                    meshRenderer.material.color = ConvertToTransparent(Color.red);
                    break;
                case 1:
                    meshRenderer.material.color = ConvertToTransparent(Color.green);
                    break;
                case 2:
                    meshRenderer.material.color = ConvertToTransparent(Color.blue);
                    break;
                case 3:
                    meshRenderer.material.color = ConvertToTransparent(Color.yellow);
                    break;
                case 4:
                    meshRenderer.material.color = ConvertToTransparent(Color.cyan);
                    break;
                case 5:
                    meshRenderer.material.color = ConvertToTransparent(Color.magenta);
                    break;
                case 6:
                    meshRenderer.material.color = ConvertToTransparent(Color.black);
                    currentNum = -1;
                    break;
            }
            currentNum++;
        }
    }

    public void ChangeColor(Color input)
    {
        meshRenderer.material.color = ConvertToTransparent(input);
    }

    private Color ConvertToTransparent(Color input)
    {
        Color newColor = input;
        newColor.a = 0.6f;
        return newColor;
    }
}
