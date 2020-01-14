using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudThunk : MonoBehaviour
{
    private enum ObjectType { blockSoft, blockHard, bottle };

    [Header("Set type")]
    [SerializeField] private ObjectType objectType;
    [SerializeField, Range(0, 5)] private float minimumVelocity = 1;
    [Header("Don't touch")]
    [SerializeField] private Soundcard blockSoft;
    [SerializeField] private Soundcard blockHard;
    [SerializeField] private Soundcard bottle;
    [SerializeField] private GameObject simpleSoundCardPlayer;

    private Soundcard toPlay = null;

    private void OnEnable()
    {
        switch (objectType)
        {
            case ObjectType.blockSoft:
                toPlay = blockSoft;
                break;
            case ObjectType.blockHard:
                toPlay = blockHard;
                break;
            case ObjectType.bottle:
                toPlay = bottle;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= minimumVelocity)
        {
            GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
            audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(toPlay);
        }
    }
}
