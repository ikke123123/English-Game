using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [SerializeField] private float minHeight;
    [HideInInspector] private Vector3 startPos;
    [HideInInspector] private Quaternion startRotation;
    [HideInInspector] private Rigidbody rb;
    [HideInInspector] private bool disable;

    private void Start()
    {
        startPos = transform.position;
        if (startPos.y < minHeight) Debug.LogError("Will forever respawn: " + gameObject.name); disable = true;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        if (rb == null) Debug.LogError("Disabled respawning: " + gameObject.name); disable = true;
    }

    private void FixedUpdate()
    {
        if (disable == false && transform.position.y < minHeight)
        {
            transform.position = startPos;
            transform.rotation = startRotation;
            CodeLibrary.SetVelocity(rb);
        }
    }
}
