using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    bool canBreak;
    private Rigidbody rb;
    public float breakLimit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (!canBreak)
        {
            if (rb.velocity.magnitude > breakLimit)
            {
                canBreak = true;
            }
        }
        //Debug.Log("Bottle " + rb.velocity.magnitude);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (canBreak)
        {

            foreach (Transform transyBit in transform)
            {
                transyBit.gameObject.GetComponent<Collider>().enabled = true;
                transyBit.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                transyBit.gameObject.transform.parent = null;
            }
        }
    }
}
