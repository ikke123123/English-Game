using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    [SerializeField] private float breakLimit = 1;
    [SerializeField, Tooltip("Will only work if the liquid is attached")] private GameObject effect;
    [SerializeField] private MeshRenderer liquid;
    [SerializeField] private GameObject replacementObject;
    [HideInInspector] private bool hasBroken = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > breakLimit && hasBroken == false)
        {
            hasBroken = true;
            if (liquid != null)
            {
                GameObject tempEffect = Instantiate(effect, transform.position, transform.rotation);
                tempEffect.GetComponent<ApplyParticleColor>().SetColor(liquid.material);
            }
            CodeLibrary.ReplaceObject(gameObject, replacementObject);
        }
    }
}
