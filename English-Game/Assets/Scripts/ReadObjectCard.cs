using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReadObjectCard : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Script maintains a list of objectcards that
    //are currently within the bounds of the
    //trigger.
    //------------------------------------------
    //Last Modification Time: 15:14 07/01/2020

    [Header("Data")]
    [SerializeField, Tooltip("Can be left empty")] private ObjectCardAction[] cardAction;

    [Header("Puzzle Objects")]
    [SerializeField, Tooltip("Can be left empty")] private GameObject combinationObject = null;
    [SerializeField, Tooltip("Can be left empty")] private PuzzleOneScript puzzleOneObject = null;

    [HideInInspector] private List<ObjectCard> objectCards = new List<ObjectCard>();
    [HideInInspector] private List<Combinations> possibleCombinations = new List<Combinations>();

    private void Awake()
    {
        foreach (Collider collider in gameObject.GetComponents<Collider>())
        {
            if (collider.isTrigger) return;
        }
        Debug.LogWarning(gameObject.name + " doesn't contain a collider");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<ObjectCardHolder>().objectCard != null)
        {
            ObjectCard objectCard = collider.gameObject.GetComponent<ObjectCardHolder>().objectCard;
            foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onEnter.Invoke();
            objectCards.Add(objectCard);
            PushCard(objectCard);
            PushCombination();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<ObjectCardHolder>().objectCard != null)
        {
            ObjectCard objectCard = collider.gameObject.GetComponent<ObjectCardHolder>().objectCard;
            foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onEnter.Invoke();
            objectCards.Remove(objectCard);
        }
    }

    public void PushCombination()
    {
        if (combinationObject == null) return;
        if (combinationObject.GetComponent<UselessTestScript>() == null) return;
        if (CombinationCheck()) combinationObject.GetComponent<UselessTestScript>().Bruh(objectCards.ToArray()); //NEEDS TO BE UPDATED
    }

    public void PushCard(ObjectCard card)
    {
        if (puzzleOneObject == null) return;
        puzzleOneObject.GetComponent<PuzzleOneScript>().PictureGiven(card);
    }

    private bool CombinationCheck()
    {
        if (objectCards.Count == 0) return false;
        possibleCombinations.Clear();
        foreach (ObjectCard objectCard in objectCards)
        {
            if (objectCard.combinations == null) return false;
            foreach (Combinations combination in objectCard.combinations)
            {
                if (CheckAllInCombination(combination) == false) return false;
                possibleCombinations.Add(combination);
            }
        }
        return true;
    }

    private bool CheckAllInCombination(Combinations input)
    {
        List<ObjectCard> tempCards = new List<ObjectCard>();
        tempCards.AddRange(input.combinesWith);
        foreach (ObjectCard objectCardInCombination in objectCards)
        {
            if (!(tempCards.Contains(objectCardInCombination)))
            {
                return false;
            }
        }
        return true;
    }
}

[System.Serializable]
public class ObjectCardAction
{
    [SerializeField] public ObjectCard card;
    [SerializeField] public UnityEvent onEnter;
    [SerializeField] public UnityEvent onLeave;
}