using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkScript : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.9f)] private float minScale = 0.1f;
    [SerializeField, Range(0.01f, 0.9f)] private float step = 0.1f;

    [HideInInspector] private List<OVRGrabber> grabbers = new List<OVRGrabber>();
    [HideInInspector] private Vector3 standardSize;
    [HideInInspector] private Vector3 minSize;

    private void OnEnable()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Hand"))
        {
            if (gameObject.GetComponent<OVRGrabber>()) grabbers.Add(gameObject.GetComponent<OVRGrabber>());
        }
        if (grabbers.Count != 2) Debug.LogWarning("Number of grabbers is not correct.");
        standardSize = transform.localScale;
        minSize = CodeLibrary.ScaleVector3Modifier(standardSize, minScale);
    }

    private void FixedUpdate()
    {
        if (GetComponent<OVRGrabbable>())
        {
            bool isGrabbed = false;
            foreach (OVRGrabber grabber in grabbers) if (GetComponent<OVRGrabbable>().grabbedBy == grabber) isGrabbed = true;
            if (isGrabbed)
            {
                Destroy(GetComponent<OVRGrabbable>());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Shrinker")
        {
            if (transform.localScale != minSize)
            {
                transform.localScale = CodeLibrary.ClampVector3(CodeLibrary.ScaleVector3Sum(transform.localScale, -step), minSize, standardSize);
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Shrinker")
        {
            Destroy(GetComponent<ShrinkScript>());
        }
    }
}
