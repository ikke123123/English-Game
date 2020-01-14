using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScript: MonoBehaviour
{
    public Vector3 rotationXYZ;
    private Vector3 center;

    private bool inBox = false;
    float height;
    Vector3 extents;

    private float rightPrimaryTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
    private float leftPrimaryTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

    [HideInInspector] private enum LeftOrRight { left, right };
    [HideInInspector] private LeftOrRight leftOrRight = 0;

    public bool multipleSnap = false;
    private bool wasSnapped = false;

    // Start is called before the first frame update
    void Start()
    {
        height = GetComponent<Renderer>().bounds.extents.y;
        extents = GetComponent<Renderer>().bounds.extents;
    }

    // Update is called once per frame
    void Update()
    {
        rightPrimaryTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        leftPrimaryTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        if (inBox == true && rightPrimaryTrigger < 0.5 && GetComponent<OVRGrabbable>().isGrabbed && wasSnapped == false && leftOrRight == LeftOrRight.right)
        {
            //====Does all the things it needs to do to make it snap====
            center += new Vector3(0, height, 0);
            transform.rotation = Quaternion.Euler(rotationXYZ);
            transform.position = center;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (multipleSnap == false)
            {
                rb.isKinematic = true;
                wasSnapped = true;
                GetComponent<OVRGrabbable>().enabled = false;
            }
        }

        if (inBox == true && leftPrimaryTrigger < 0.5 && GetComponent<OVRGrabbable>().isGrabbed && wasSnapped == false && leftOrRight == LeftOrRight.left)
        {
            //====Does all the things it needs to do to make it snap====
            center += new Vector3(0, height, 0);
            transform.rotation = Quaternion.Euler(rotationXYZ);
            transform.position = center;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (multipleSnap == false)
            {
                rb.isKinematic = true;
                wasSnapped = true;
                GetComponent<OVRGrabbable>().enabled = false;
            }
        }

        //Debug.Log("Trigger value = " + OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            //Checks in wich hand the object is in.
            if (other.gameObject.name.ToString().ToLower().IndexOf("left") != -1)
            {
                leftOrRight = LeftOrRight.left;
            }
            if (other.gameObject.name.ToString().ToLower().IndexOf("right") != -1)
            {
                leftOrRight = LeftOrRight.right;
            }

        }

        if (other.gameObject.tag == "TriggerBox")
        {
            center = other.transform.position;
            //Debug.Log(rotationXYZ);
            inBox = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TriggerBox")
        {
            inBox = false;
        }
    }

    private void OnMouseUp()
    {
        if (inBox == true && wasSnapped == false)
        {
            //====Does all the things it needs to do to make it snap====
            center += new Vector3(0, height, 0);
            transform.rotation = Quaternion.Euler(rotationXYZ);
            transform.position = center;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (multipleSnap == false)
            {
                rb.isKinematic = true;
                wasSnapped = true;
                GetComponent<OVRGrabbable>().enabled = false;
            }
        }
    }
}
