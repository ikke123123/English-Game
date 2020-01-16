using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRightHand : MonoBehaviour
{
    //public LayerMask layerMaskFloor;
    //public LayerMask layerMaskGrab;
    ChangeColor lastHitFloor;
    public GameObject hitObject;
    public float teleportIndicatorRangeCutoff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int layerMaskFloor = 1 << 12;
        int layerMaskGrab = 1<< 9;
        //int layerMaskUI = 1 << 5;

        Vector3 handPointing = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, handPointing, out hit, 3, layerMaskGrab))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            hit.collider.gameObject.GetComponent<OVRGrabbable>().isHit();
        }

        if (Physics.Raycast(transform.position, handPointing, out hit, Mathf.Infinity, layerMaskFloor))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (Vector3.Distance(hit.point, transform.position) > teleportIndicatorRangeCutoff)
            {
                hitObject.SetActive(true);
                hitObject.transform.position = hit.point;
            }

            if (hit.collider.gameObject.GetComponent<ChangeColor>()) hit.collider.gameObject.GetComponent<ChangeColor>().isHit();
            lastHitFloor = hit.collider.gameObject.GetComponent<ChangeColor>();
            //Debug.Log("hey");
        }
        else
        {
            hitObject.SetActive(false);
            if (lastHitFloor != null)
            {
                lastHitFloor.unHit();
                lastHitFloor = null;
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //hit.collider.gameObject.GetComponent<ChangeColor>().isNotHit();
        }
    }
}
