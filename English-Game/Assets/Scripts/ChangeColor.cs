using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //Stuff for the color change
    public Color startColor;
    public Color hoverColor;
    bool rayOver = false;

    //Getting the object and location for changing the ancor.
    public GameObject anchor;
    private Transform myPoint;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        myPoint = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //If the ray is ovar and a certain button is pressed it will change the ancor.
        if (rayOver == true && OVRInput.Get(OVRInput.Button.One))
        {
            //Sending the child anchor position to the platform.
            anchor.GetComponent<AncorChange>().ChangeAnchor(myPoint.position);
        }


        //Just makes sure that the color changes if it is hit with the raycast. 
        if (rayOver == true)
        {
            GetComponent<Renderer>().material.SetColor("_Color", hoverColor);
            rayOver = false;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", startColor);
        }
    }

    public void isHit()
    {
        rayOver = true;
    }
}
