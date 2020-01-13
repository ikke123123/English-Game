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
    //trigger. And sends the Object Cards around
    //when a card enters its bounds.
    //------------------------------------------
    //Last Modification Time: 22:41 07/01/2020

    [HideInInspector] private enum CombinationComponent { unselected, read, write };

    [Header("Data")]
    [SerializeField, Tooltip("Action that will be taken upon a certain card entering/leaving. Can be left empty")] private ObjectCardAction[] cardAction;
    [Header("Puzzle One")]
    [SerializeField, Tooltip("Pushes an Object card to the puzzle one object. Can be left empty.")] private PuzzleOneScript puzzleOneObject = null;
    [Header("Puzzle Two")]
    [SerializeField, Tooltip("Select purpose of object data reader. Can be left at unselected if not used.")] private CombinationComponent combinationComponent;
    [SerializeField, Tooltip("Script that decides what needs to happen with the object cards that are pushed, made for puzzle 2. Can be left empty.")] private Combination combinationObject = null;
    [SerializeField, Tooltip("All possible combinations (leave out the alternative possible). Put only on the read object/Collider")] private Combinations[] possibleCombinations;
    //Remove later
    [SerializeField] Combinations combination;

    [HideInInspector] private List<ObjectCard> objectCards = new List<ObjectCard>();
    [HideInInspector] private List<GameObject> gameObjects = new List<GameObject>();

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
        if (collider.gameObject.GetComponent<ObjectCardHolder>() && collider.gameObject.GetComponent<ObjectCardHolder>().objectCard != null)
        {
            gameObjects.Add(collider.gameObject);
            foreach (ObjectCardHolder objectCard1 in collider.gameObject.GetComponents<ObjectCardHolder>())
            {
                ObjectCard objectCard = objectCard1.objectCard;
                foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onEnter.Invoke();
                objectCards.Add(objectCard);
                PushCardToPuzzleOneObject(objectCard);
                if (combinationComponent == CombinationComponent.read) PushCombination();
                if (combinationComponent == CombinationComponent.write) PushCardAndObject(objectCard, collider.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<ObjectCardHolder>() && collider.gameObject.GetComponent<ObjectCardHolder>().objectCard != null)
        {
            gameObjects.Remove(collider.gameObject);
            foreach (ObjectCardHolder objectCard1 in collider.gameObject.GetComponents<ObjectCardHolder>())
            {
                ObjectCard objectCard = collider.gameObject.GetComponent<ObjectCardHolder>().objectCard;
                foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onLeave.Invoke();
                objectCards.Remove(objectCard);
                if (combinationComponent == CombinationComponent.read) PushCombination();
                if (combinationComponent == CombinationComponent.write) PushCardAndObject(objectCard, collider.gameObject);
            }
        }
    }

    public void PushCombination()
    {
        if (combinationObject == null) return;

        if (CombinationCheck()) combinationObject.CatchPossibleCombinations(gameObjects.ToArray(), combination);
    }

    public void PushCardAndObject(ObjectCard card, GameObject gameObject)
    {
        if (combinationObject == null) return;
        combinationObject.CatchEmptyComponent(gameObject, card);
    }

    public void PushCardToPuzzleOneObject(ObjectCard card)
    {
        if (puzzleOneObject == null) return;
        puzzleOneObject.PictureGiven(card);
    }

    private bool CombinationCheck()
    {
        combination = null;
        List<ObjectCard> remainingCards = new List<ObjectCard>();
        foreach (Combinations combinations in possibleCombinations)
        {
            if (combinations.combinesWith.Length == objectCards.Count)
            {
                remainingCards.Clear();
                remainingCards.AddRange(combinations.combinesWith);
                foreach (ObjectCard objectCard in objectCards)
                {
                    if (remainingCards.Remove(objectCard) == false) break;
                }
                if (remainingCards.Count == 0)
                {
                    combination = combinations;
                    return true;
                }
            }
        }
        return false;
    }
}

[System.Serializable]
public class ObjectCardAction
{
    [SerializeField] public ObjectCard card;
    [SerializeField] public UnityEvent onEnter;
    [SerializeField] public UnityEvent onLeave;
}