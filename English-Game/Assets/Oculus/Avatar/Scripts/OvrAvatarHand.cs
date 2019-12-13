using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Oculus.Avatar;

public class OvrAvatarHand : OvrAvatarComponent
{
    public bool isLeftHand = true;
    ovrAvatarHandComponent component = new ovrAvatarHandComponent();

    void Update()
    {
        if (owner == null)
        {
            return;
        }

        bool hasComponent = false;
        if (isLeftHand)
        {
            hasComponent = CAPI.ovrAvatarPose_GetLeftHandComponent(owner.sdkAvatar, ref component);
        }
        else
        {
            hasComponent = CAPI.ovrAvatarPose_GetRightHandComponent(owner.sdkAvatar, ref component);
        }

        if (hasComponent)
        {
            UpdateAvatar(component.renderComponent);
        }
        else
        {
            if (isLeftHand)
            {
                owner.HandLeft = null;

            }
            else
            {
                owner.HandRight = null;
            }

            Destroy(this);
        }

        //Draws a raycast from the right hand
        if (isLeftHand != true) {
            int layerMask = 1 << 8;
            Vector3 handPointing = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, handPointing, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                hit.collider.gameObject.GetComponent<ChangeColor>().isHit();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //hit.collider.gameObject.GetComponent<ChangeColor>().isNotHit();
            }
        }
    }
}
