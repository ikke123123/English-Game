using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItoggle : MonoBehaviour
{
    private bool UIActive = false;
    Canvas canv;

    void Start()
    {
        canv = GetComponent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two)) 
        {
            if (UIActive == false)
            {
                UIActive = true;
                Debug.Log("true");
                canv.enabled = true;
            }
            else
            {
                UIActive = false;
                Debug.Log("false");
                canv.enabled = false;
            }
        }
    }
}
