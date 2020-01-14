using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //This script maintains a system of milesto-
    //nes by which the game will know where the
    //player is, what he/she has to do, and this
    //script can save those things for later 
    //use.
    //------------------------------------------
    //Last Modification Time: 15:14 07/01/2020

    [Header("Milestone Data")]
    [SerializeField] private Milestones[] milestones;
    [SerializeField] private Finish end;

    [Header("Playerdata")]
    [SerializeField] private bool noSave;
    [SerializeField] private string playerName;

    [Header("Debug")]
    [SerializeField] private int currentMilestoneID;
    [SerializeField] public Milestones currentMilestone;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("CurrentPlayerName", "Mark-Peter");
        if (playerName == "Mark-Peter" && noSave) DeleteThisUser();
        SetCurrentMilestone(PlayerPrefs.GetInt(playerName + ":CurrentMilestoneID", 0));
    }

    /// <summary>
    /// Moves the game onto the next milestone. currentID is used only as verification
    /// </summary>
    /// <param name="currentID"></param>
    /// <returns></returns>
    public void CurrentMilestoneCompleted(int currentID)
    {
        if (currentID == currentMilestoneID)
        {
            if (currentID + 1 < milestones.Length)
            {
                currentMilestone.isCompleted = true;
                currentMilestone.toDoWhenCompleted.Invoke();
                currentMilestoneID++;
                currentMilestone = milestones[currentMilestoneID];
                UpdateUserProgress();
                currentMilestone.toDoWhenStarted.Invoke();
                return;
            }
            currentMilestone.isCompleted = true;
            currentMilestone.toDoWhenCompleted.Invoke();
            end.toDoWhenStarted.Invoke();
        }
    }

    private bool SetCurrentMilestone(int milestoneID = 0)
    {
        if (milestoneID < milestones.Length && milestoneID >= 0)
        {
            currentMilestoneID = milestoneID;
            currentMilestone = milestones[milestoneID];
            UpdateUserProgress();
            currentMilestone.toDoWhenStarted.Invoke();
            return true;
        }
        else if (milestones.Length == 0)
        {
            currentMilestoneID = milestoneID;
            UpdateUserProgress();
            return true;
        }
        SetCurrentMilestone();
        return false;
    }

    private bool UpdateUserProgress()
    {
        if (noSave == false)
        {
            PlayerPrefs.SetInt(playerName + ":CurrentMilestoneID", currentMilestoneID);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public void DeleteThisUser()
    {
        PlayerPrefs.DeleteKey(playerName + ":CurrentMilestoneID");
        PlayerPrefs.Save();
    }

    public void StopGame()
    {
        Debug.Log("Game was stopped");
        Application.Quit(1);
    }

    public void GoTo(int currentID, int goTo)
    {
        if (currentMilestoneID == currentID)
        {
            SetCurrentMilestone(goTo);
        }
    }
}

[System.Serializable]
public class Milestones
{
    [SerializeField] public string milestoneName;
    [TextArea]
    [SerializeField] public string goalDescription;
    [SerializeField] public UnityEvent toDoWhenStarted;
    [SerializeField] public UnityEvent toDoWhenCompleted;
    [HideInInspector] public bool isCompleted;
}

[System.Serializable]
public class Finish
{
    [SerializeField] public UnityEvent toDoWhenStarted;
}