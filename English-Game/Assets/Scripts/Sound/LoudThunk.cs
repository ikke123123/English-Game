using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectThunkType { blockSoft, blockHard, bottle };

public class LoudThunk : MonoBehaviour
{
    [Header("Set Type")]
    [SerializeField] private LoudThunkSetting[] loudThunkSettings;
    [Header("Don't Touch")]
    [SerializeField] private Soundcard blockSoft;
    [SerializeField] private Soundcard blockHard;
    [SerializeField] private Soundcard bottle;
    [SerializeField] private GameObject simpleSoundCardPlayer;

    private void OnEnable()
    {
        foreach (LoudThunkSetting loudThunkSetting in loudThunkSettings)
        {
            switch (loudThunkSetting.objectType)
            {
                case ObjectThunkType.blockSoft:
                    loudThunkSetting.toPlay = blockSoft;
                    break;
                case ObjectThunkType.blockHard:
                    loudThunkSetting.toPlay = blockHard;
                    break;
                case ObjectThunkType.bottle:
                    loudThunkSetting.toPlay = bottle;
                    break;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (LoudThunkSetting loudThunkSetting in loudThunkSettings)
        {
            if (collision.relativeVelocity.magnitude >= loudThunkSetting.minimumVelocity && (loudThunkSetting.maximumVelocity == -1 || collision.relativeVelocity.magnitude <= loudThunkSetting.maximumVelocity))
            {
                /* && (loudThunkSetting.minimumVelocity == -1 || collision.relativeVelocity.magnitude <= loudThunkSetting.minimumVelocity)*/
                GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
                audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(loudThunkSetting.toPlay);
            }
        }
    }
}

[System.Serializable]
public class LoudThunkSetting
{
    [SerializeField] public ObjectThunkType objectType;
    [SerializeField, Range(0, 5)] public float minimumVelocity = 1;
    [SerializeField, Range(-1, 10), Tooltip("Set to -1 for infinite.")] public float maximumVelocity = -1;
    [HideInInspector] public Soundcard toPlay = null;
}
