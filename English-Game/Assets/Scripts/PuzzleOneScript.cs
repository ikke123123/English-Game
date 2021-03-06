﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PuzzleOneScript : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script makes a list of objectcards
    //to check it hasn't already encountered one
    //of them before and counts them.

    private enum PictureAspect { activities, attributes, locations };

    [Header("Data")]
    [SerializeField] private PictureSound pictureSound;
    [SerializeField] private NopeSound nopeSounds;
    [SerializeField] private Soundcard beginDialogue;

    [Header("Feedback")]
    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private SoundcardPlayer soundcardPlayer;

    [Header("Event")]
    [SerializeField] private UnityEvent toDoAfterComplete;

    [Header("Puzzle Settings")]
    [SerializeField, Tooltip("Measured from the last card, which card should be the right one."), Range(0, 5)] private int cardAnswer = 0;
    [SerializeField, Tooltip("This bool is activated when the puzzle is finished, this bool is only here for debugging.")] private bool disablePuzzle = true;
    [SerializeField] private string textAfterCompletion = "";

    //Hidden from inspector
    [HideInInspector] private int currentCardNumber = 0;
    [HideInInspector] private List<PictureTag> passedTags = new List<PictureTag>();
    [HideInInspector] private int activityCount = 2;
    [HideInInspector] private int locationCount = 2;
    [HideInInspector] private int attributeCount = 2;

    public void StartPuzzle()
    {
        disablePuzzle = false;
        if (beginDialogue != null) soundcardPlayer.StartPlaying(beginDialogue);
    }

    public void PictureGiven(ObjectCard input)
    {
        if (disablePuzzle == false)
        {
            //Checks if the object holds a tag
            if (input.location == PictureTag.untagged) return;
            //Check if object has a tag that has already been shown
            if (TagHistoryCheck(input)) return;
            //Checks if the game is finished
            if (currentCardNumber > 5 - cardAnswer)
            {
                Finished();
                return;
            }
            //Act out order and check if there are duplicates
            ApplyOrder(input);
        }
    }

    private bool TagHistoryCheck(ObjectCard input)
    {
        List<PictureTag> tempTag = new List<PictureTag>();
        if (passedTags.Contains(input.location)) tempTag.Add(input.location);
        if (passedTags.Contains(input.activity)) tempTag.Add(input.activity);
        if (passedTags.Contains(input.attribute)) tempTag.Add(input.attribute);
        if (tempTag.Count != 0)
        {
            ExecuteOnObject(tempTag[Random.Range(0, tempTag.Count)]);
            return true;
        }
        return false;
    }

    private void ApplyOrder(ObjectCard input)
    {
        if (Check(input, SemiRandomPictureAspect())) currentCardNumber++;
    }

    private void Finished()
    {
        toDoAfterComplete.Invoke();
        disablePuzzle = true;
        ApplyText(textAfterCompletion, false);
    }

    private bool Check(ObjectCard input, PictureAspect tag)
    {
        PictureTag tempPicTag;
        switch (tag)
        {
            case PictureAspect.activities:
                tempPicTag = input.activity;
                break;
            case PictureAspect.attributes:
                tempPicTag = input.attribute;
                break;
            case PictureAspect.locations:
                tempPicTag = input.location;
                break;
            default:
                return false;
        }
        return ExecuteOnObject(tempPicTag);
    }

    private bool ExecuteOnObject(PictureTag tag)
    {
        bool output = true;
        if (passedTags.Contains(tag)) output = false;
        else passedTags.Add(tag);
        Soundcard soundcard = ChooseANope(TagToTag(tag));
        if (soundcard != null)
        {
            soundcard.playAfterThis = PictureTagToSoundcard(tag);
            soundcard.timePlayAfterThis = 3;
            soundcardPlayer.StartPlaying(soundcard);
        }
        ApplyText(tag.ToString());
        return output;
    }

    private PictureAspect TagToTag(PictureTag input)
    {
        if (input > PictureTag.untagged && input <= PictureTag.forrest) return PictureAspect.locations;
        if (input >= PictureTag.bird && input <= PictureTag.pug) return PictureAspect.attributes;
        return PictureAspect.activities;
    }

    private Soundcard ChooseANope(PictureAspect tag)
    {
        switch (tag)
        {
            case PictureAspect.locations:
                return nopeSounds.locations[Random.Range(0, nopeSounds.locations.Length)];
            case PictureAspect.activities:
                return nopeSounds.activities[Random.Range(0, nopeSounds.activities.Length)];
            case PictureAspect.attributes:
                return nopeSounds.attributes[Random.Range(0, nopeSounds.attributes.Length)];
            default:
                return null;
        }
    }

    private Soundcard PictureTagToSoundcard(PictureTag tag)
    {
        switch (tag)
        {
            case PictureTag.beach: 
                return pictureSound.beach;
            case PictureTag.field: 
                return pictureSound.field;
            case PictureTag.forrest: 
                return pictureSound.forest;
            case PictureTag.bird: 
                return pictureSound.bird;
            case PictureTag.cat: 
                return pictureSound.cat;
            case PictureTag.pug: 
                return pictureSound.pug;
            case PictureTag.lying: 
                return pictureSound.lying;
            case PictureTag.sitting: 
                return pictureSound.sitting;
            case PictureTag.standing: 
                return pictureSound.standing;
            default:
                return null;
        }
    }

    private void ApplyText(string input, bool useTemplate = true)
    {
        if (textDisplay != null)
        {
            if (useTemplate) textDisplay.text = "I would not like " + input + " ever.";
            else textDisplay.text = input;
            Debug.Log(textDisplay.text);
        }
    }

    private PictureAspect SemiRandomPictureAspect()
    {
        List<PictureAspect> randomPictureAspect = new List<PictureAspect>();
        if (activityCount > 0) randomPictureAspect.Add(PictureAspect.activities);
        if (locationCount > 0) randomPictureAspect.Add(PictureAspect.locations);
        if (attributeCount > 0) randomPictureAspect.Add(PictureAspect.attributes);
        if (randomPictureAspect.Count == 0)
        {
            Debug.LogError("Random Picture aspect set wrong");
        }
        PictureAspect tag = randomPictureAspect[Random.Range(0, randomPictureAspect.Count)];
        switch (tag)
        {
            case PictureAspect.activities:
                activityCount--;
                break;
            case PictureAspect.attributes:
                attributeCount--;
                break;
            case PictureAspect.locations:
                locationCount--;
                break;
        }
        return tag;
    }
}

[System.Serializable]
public class NopeSound
{
    [SerializeField] public Soundcard[] locations;
    [SerializeField] public Soundcard[] attributes;
    [SerializeField] public Soundcard[] activities;
}

[System.Serializable]
public class PictureSound
{
    [SerializeField] public Soundcard beach;
    [SerializeField] public Soundcard field;
    [SerializeField] public Soundcard forest;
    [SerializeField] public Soundcard bird;
    [SerializeField] public Soundcard cat;
    [SerializeField] public Soundcard pug;
    [SerializeField] public Soundcard lying;
    [SerializeField] public Soundcard sitting;
    [SerializeField] public Soundcard standing;
}