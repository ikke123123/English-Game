using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For resetting the position.
public class OVRResetGrabbable : OVRGrabbable
{
    public Transform handel;

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(Vector3.zero, Vector3.zero);

        transform.position = handel.transform.position;
        transform.rotation = handel.transform.rotation;

        
        Rigidbody rbhandler = handel.GetComponent<Rigidbody>();
        rbhandler.velocity = Vector3.zero;
        rbhandler.angularVelocity = Vector3.zero;
        
    }
    
    private void Update()
    {
        if(Vector3.Distance(handel.position, transform.position) > 0.4f)
        {
            grabbedBy.ForceRelease(this);
        }
    }
    
}
