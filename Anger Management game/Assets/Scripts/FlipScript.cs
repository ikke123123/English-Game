using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipScript : MonoBehaviour
{
    public Vector3 rotationXYZ;
    private Vector3 center;

    private bool fliped = false;
    float height;
    Vector3 extents;

    // Start is called before the first frame update
    void Start()
    {
        rotationXYZ = transform.eulerAngles;
        height = GetComponent<Renderer>().bounds.extents.y;
        extents = GetComponent<Renderer>().bounds.extents;
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.eulerAngles.x >= 60 && transform.eulerAngles.x <= 300 || transform.eulerAngles.z >= 60 && transform.eulerAngles.z <= 300)
        {
            transform.eulerAngles = rotationXYZ;
        }
    }

    private void OnMouseUp()
    {
        if (fliped)
        {
            center += new Vector3(0, height, 0);
            transform.rotation = Quaternion.Euler(rotationXYZ);
            transform.position = center;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
