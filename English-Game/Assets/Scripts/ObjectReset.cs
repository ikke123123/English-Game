using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Script makes sure that the object respawns
    //when it is below the bounds of the playing
    //area.
    //Use this script on objects that are missi-
    //on critical, and need to be respawned when
    //they get lost.

    [Header("Settings")]
    [SerializeField, Tooltip("Object should respawn whenever it gets below the Minimal Height.")] private bool respawnUpdate = false;
    [SerializeField, Tooltip("Object should respawn whenever it gets destroyed.")] private bool respawnOnDestroy = false;
    [SerializeField] private float minHeight = -5;

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
        if (startPos.y < minHeight && respawnUpdate)
        {
            Debug.LogWarning("Will forever respawn: " + gameObject.name); 
            disable = true;
        }
  
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Disabled respawning: " + gameObject.name); 
            disable = true;
        }
    }

    private void Update()
    {
        if (respawnUpdate && disable == false && transform.position.y < minHeight)
        {
            transform.position = startPos;
            transform.rotation = startRotation;
            CodeLibrary.SetVelocity(rb);
        }
    }

    private void OnDestroy()
    {
        if (disable == false && respawnOnDestroy)
        {
            GameObject copy = Instantiate(gameObject, startPos, startRotation);
        }
    }

    public void ResetPosition()
    {
        if (disable == false)
        {
            transform.position = startPos;
            transform.rotation = startRotation;
            CodeLibrary.SetVelocity(rb);
        }
    }
}
