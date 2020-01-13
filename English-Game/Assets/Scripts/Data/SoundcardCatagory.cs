using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soundcard Catagory", menuName = "Custom/Soundcard Catagory")]
public class SoundCategory : ScriptableObject
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script contains a soundcard catagory
    //so that two sounds of the same catagory
    //play over each other.

    [HideInInspector] public List<Soundcard> soundcards = new List<Soundcard>();
    [HideInInspector] public State catagoryState;

    private void OnDestroy()
    {
        soundcards.Clear();
    }

    public static bool CategoryStateOff(Soundcard soundcard)
    {
        if (soundcard.category == null || soundcard.category.catagoryState == State.off)
        { 
            return true;
        }
        return false;
    }

    public static void SetCategoryStateOff(Soundcard soundcard)
    {
        if (soundcard.category != null)
        {
            soundcard.category.catagoryState = State.off;
        }
    }

    public static void SetCategoryStateOn(Soundcard soundcard)
    {
        if (soundcard.category != null)
        {
            soundcard.category.catagoryState = State.playing;
        }
    }

    public static void AddSoundcardToCategory(Soundcard soundcard)
    {
        if (soundcard.category != null)
        {
            soundcard.category.soundcards.Add(soundcard);
        }
    }
}
