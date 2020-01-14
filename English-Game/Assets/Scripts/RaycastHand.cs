using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHand : MonoBehaviour
{
    //public LayerMask layerMaskFloor;
    //public LayerMask layerMaskGrab;
    ChangeColor lastHitFloor;
    public float teleportIndicatorRangeCutoff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            int layerMaskGrab = 1 << 9;
            Vector3 handPointing = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

        if (Physics.Raycast(transform.position, handPointing, out hit, 3, layerMaskGrab))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            if (hit.collider.gameObject.GetComponent<OVRGrabbable>()) hit.collider.gameObject.GetComponent<OVRGrabbable>().isHit();
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //hit.collider.gameObject.GetComponent<ChangeColor>().isNotHit();
        }
    }
}
