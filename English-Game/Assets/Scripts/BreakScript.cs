using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float breakLimit = 1;
    [SerializeField, Tooltip("Will only work if the liquid is attached")] private GameObject effect;
    [SerializeField] private MeshRenderer liquid;
    [SerializeField] public SoundcardPlayer specialBreak;

    [Header("Don't Touch")]
    [SerializeField] private GameObject replacementObject;
    [SerializeField] private Soundcard emptyGlassBreak;
    [SerializeField] private Soundcard fullGlassBreak;
    [SerializeField] private Soundcard specialBreakSoundcard;
    [SerializeField] private GameObject simpleSoundCardPlayer;
    [HideInInspector] private bool hasBroken = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > breakLimit && hasBroken == false)
        {
            hasBroken = true;
            GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
            if (liquid != null)
            {
                GameObject tempEffect = Instantiate(effect, transform.position, transform.rotation);
                tempEffect.GetComponent<ApplyParticleColor>().SetColor(liquid.material);
                audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(fullGlassBreak);
            }
            else audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(emptyGlassBreak);
            if (specialBreak != null)
            {
                specialBreak.StartPlaying(specialBreakSoundcard, true);
            }
            CodeLibrary.ReplaceObject(gameObject, replacementObject);
        }
    }
}
