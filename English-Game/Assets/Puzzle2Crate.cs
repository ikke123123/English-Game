using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle2Crate : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    [Header("Settings")]
    [SerializeField] private UnityEvent onCompletion;
    [SerializeField] private SoundcardPlayer player;
    [SerializeField] private CrateCompletion[] crateCompletions;

    [Header("Debug")]
    [SerializeField] private bool completed = false;
    [SerializeField] private int currentNumber = 0;

    public void ReceiveCard(ObjectCard objectCard, GameObject gameobject)
    {
        if (completed == false)
        {
            if (currentNumber == crateCompletions.Length)
            {
                Debug.LogWarning("crateCompletions is empty on " + gameObject.name);
                return;
            }
            if (objectCard == crateCompletions[currentNumber].card)
            {
                gameobject.layer = 0;
                if (gameobject.GetComponent<BreakScript>()) Destroy(gameobject.GetComponent<BreakScript>());
                player.StartPlaying(crateCompletions[currentNumber].toPlay);
                currentNumber++;
                if (currentNumber >= crateCompletions.Length)
                {
                    onCompletion.Invoke();
                    completed = true;
                }
            }
        }
    }
}

[System.Serializable]
public class CrateCompletion 
{
    [SerializeField] public ObjectCard card;
    [SerializeField] public Soundcard toPlay;
}
