using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButtonY : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [SerializeField] private UnityEvent whenButtonIsPressed;

    [Header("Variables")]
    [SerializeField, Range(-0.1f, 0.0f), Tooltip("Button Travel Distance is the distance the button travels inwards until it is stopped. This should always be negative.")] private float buttonTravelDistance;
    [SerializeField, Range(0, 10), Tooltip("Times the button can be cycled, 0 is infinite.")] private int numCycles;
    [SerializeField, Range(-1, 5), Tooltip("How long the button will be held after pressing it, -1 is infinite")] private float holdTime;
    
    [HideInInspector] private Vector3 StartPos;
    [HideInInspector] private bool isActive;
    [HideInInspector] private bool isInfinite = false;
    [HideInInspector] private bool holdForever = false;
    [HideInInspector] private float holdTimeAbsolute = 0;
    [HideInInspector] private int cyclesPassed = 0;

    void Start()
    {
        StartPos = transform.position;
        if (numCycles == 0) isInfinite = true;
        if (Mathf.Floor(holdTime) == -1 || numCycles == 1) holdForever = true;
    }

    void Update()
    {
        if (holdForever == false)
        {
            if (isActive)
            {
                if (holdTimeAbsolute <= Time.time && (isInfinite || cyclesPassed <= numCycles))
                {
                    SetAllConstraints(false);
                    isActive = false;
                }
            }
            else if (transform.position.y > StartPos.y)
            {
                CodeLibrary.SetY(transform, StartPos.y);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isActive == false && transform.position.y - StartPos.y < buttonTravelDistance)
        {
            CodeLibrary.SetY(transform, buttonTravelDistance + StartPos.y);
            SetAllConstraints(true);
            isActive = true;
            whenButtonIsPressed.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (holdForever == false) holdTimeAbsolute = Time.time + holdTime;
    }

    private void SetAllConstraints(bool freezeAll)
    {
        if (freezeAll)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
}
