using System.Collections.Generic;
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

    [Header("Feedback")]
    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private SoundcardPlayer soundcardPlayer;

    [Header("Event")]
    [SerializeField] private UnityEvent toDoAfterComplete;

    [Header("Puzzle Settings")]
    [SerializeField, Tooltip("Measured from the last card, which card should be the right one"), Range(0, 5)] private int cardAnswer = 0;
    [SerializeField] private bool disablePuzzle = false;

    //Hidden from inspector
    [HideInInspector] private int currentCardNumber = 0;
    [HideInInspector] private List<PictureTag> passedTags = new List<PictureTag>();
    [HideInInspector] private int activityCount = 2;
    [HideInInspector] private int locationCount = 2;
    [HideInInspector] private int attributeCount = 2;

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
            if (Check(input, SemiRandomPictureAspect())) currentCardNumber++;
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

    private void Finished()
    {
        toDoAfterComplete.Invoke();
        disablePuzzle = true;
        ApplyText("Great job, you did it! Have some Radiohead", false);
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
        Soundcard soundcard = PictureTagToSoundcard(tag);
        if (soundcard != null) soundcardPlayer.StartPlaying(soundcard);
        ApplyText(tag.ToString());
        return output;
    }

    private Soundcard PictureTagToSoundcard(PictureTag tag)
    {
        switch (tag)
        {
            case PictureTag.blue:
                return pictureSound.blue;
            case PictureTag.gray:
                return pictureSound.gray;
            case PictureTag.green:
                return pictureSound.green;
            case PictureTag.standard:
                return pictureSound.standard;
            case PictureTag.lighter:
                return pictureSound.lighter;
            case PictureTag.darker:
                return pictureSound.darker;
            case PictureTag.normal:
                return pictureSound.normal;
            case PictureTag.metallic:
                return pictureSound.metallic;
            case PictureTag.transparent:
                return pictureSound.transparent;
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
public class PictureSound
{
    [SerializeField] public Soundcard blue;
    [SerializeField] public Soundcard gray;
    [SerializeField] public Soundcard green;
    [SerializeField] public Soundcard standard;
    [SerializeField] public Soundcard lighter;
    [SerializeField] public Soundcard darker;
    [SerializeField] public Soundcard normal;
    [SerializeField] public Soundcard metallic;
    [SerializeField] public Soundcard transparent;
}