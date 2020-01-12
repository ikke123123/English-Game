using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    [SerializeField] private float breakLimit = 1;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject replacementObject;

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > breakLimit)
        {
            Instantiate(effect, transform.position, transform.rotation);
            CodeLibrary.ReplaceObject(gameObject, replacementObject);
        }
    }
}
