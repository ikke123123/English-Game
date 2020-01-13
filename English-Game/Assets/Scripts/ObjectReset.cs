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
    //Last Modification Time: 15:13 07/01/2020

    [Header("Settings")]
    [SerializeField, Tooltip("Object should respawn whenever it gets below the Minimal Height.")] private bool respawnUpdate = false;
    [SerializeField, Tooltip("Object should respawn whenever it gets destroyed.")] private bool respawnOnDestroy = false;
    [SerializeField] private float minHeight = -5;

    [HideInInspector] private GameObject clone;
    [HideInInspector] private ObjectResetManager resetManager;
    [HideInInspector] private Vector3 startPos;
    [HideInInspector] private Quaternion startRotation;
    [HideInInspector] private bool disable = false;

    private void OnEnable()
    {
        if (respawnOnDestroy == true)
        {
            SpawnClone();
        }

        resetManager = GameObject.FindGameObjectWithTag("Data Object").GetComponent<ObjectResetManager>();
        resetManager.ObjectResetAdd(this);

        startPos = transform.position;
        if (startPos.y < minHeight && respawnUpdate)
        {
            Debug.LogError("Will forever respawn: " + gameObject.name); 
            disable = true;
        }
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (respawnUpdate && transform.position.y < minHeight)
        {
            ResetPosition();
        }
    }

    private void OnDestroy()
    {
        if (disable == false && respawnOnDestroy && resetManager.canRespawnOnDestroy)
        {
            clone.GetComponent<ObjectReset>().respawnOnDestroy = true;       
            clone.SetActive(true);
            resetManager.ObjectResetRemove(this);
        }
    }

    public void SpawnClone()
    {
        respawnOnDestroy = false;
        clone = Instantiate(gameObject, transform.position, transform.rotation);
        clone.SetActive(false);
        respawnOnDestroy = true;
    }

    public void ResetPosition()
    {
        if (disable == false)
        {
            transform.position = startPos;
            transform.rotation = startRotation;
            CodeLibrary.SetVelocity(gameObject.GetComponent<Rigidbody>());
        }
    }
}
