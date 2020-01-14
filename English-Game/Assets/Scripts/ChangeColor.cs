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
    public AncorChange ancor;

    public GameObject positionObject;

    public bool locked = false;

    Vector3 currentPos;
    Vector3 newPos;
    private void Awake()
    {
        ancor = FindObjectOfType<AncorChange>();
    }
    // Start is called before the first frame update
    void Start()
    {
        myPoint = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked == false)
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
                newPos.x = /*ancor.difference.x +*/ transform.position.x ;
                newPos.z = /*ancor.difference.z +*/ transform.position.z;
                //Debug.Log(newPos.z);

                
                //positionObject.SetActive(true);

                //positionObject.transform.position = newPos;
                rayOver = false;
            }
            else
            {
                //positionObject.SetActive(false);
                GetComponent<Renderer>().material.SetColor("_Color", startColor);
            }
        }
    }

    public void isHit()
    {
        rayOver = true;
    }

    public void unHit()
    {
        rayOver = false;
    }

    //Reference to this form another script to unlock the locked area. 
    public void unlock()
    {
        locked = false;
    }
}
