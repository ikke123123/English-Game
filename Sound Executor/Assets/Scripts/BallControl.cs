using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    public Vector3 sightline;
    private Vector3 turning;

    public static GameObject selected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit look = new RaycastHit();
        sightline = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, sightline, Color.red);
        if(Input.anyKeyDown)
        {
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                turning = new Vector3(0, -3, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                turning = new Vector3(0, 3, 0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                turning = new Vector3(3, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                turning = new Vector3(-3, 0, 0);
            }
        }
        else
        {
            turning = new Vector3(0,0,0);
        }

        gameObject.transform.Rotate(turning, Space.Self);
        
        

        if (Physics.Raycast(transform.position, sightline, 100))
        {
            selected = look.collider.gameObject;
            
        }
        
    }
}
