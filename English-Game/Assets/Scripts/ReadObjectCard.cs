using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReadObjectCard : MonoBehaviour
{
    //------------------------------------------
    //Script maintains a list of objectcards that
    //are currently within the bounds of the
    //trigger. And sends the Object Cards around
    //when a card enters its bounds.
    //------------------------------------------

    private enum CombinationComponent { unselected, read, write };

    [Header("Data")]
    [SerializeField, Tooltip("Action that will be taken upon a certain card entering/leaving. Can be left empty")] private ObjectCardAction[] cardAction = null;
    [Header("Puzzle One")]
    [SerializeField, Tooltip("Pushes an Object card to the puzzle one object. Can be left empty.")] private PuzzleOneScript puzzleOneObject = null;
    [Header("Puzzle Two")]
    [SerializeField, Tooltip("Select purpose of object data reader. Can be left at unselected if not used.")] private CombinationComponent combinationComponent;
    [SerializeField, Tooltip("Script that decides what needs to happen with the object cards that are pushed, made for puzzle 2. Can be left empty.")] private Puzzle2Script combinationObject = null;
    [SerializeField, Tooltip("Script that decides what needs to happen with the object cards that are pushed, made for puzzle 2. Can be left empty.")] private Puzzle2Crate crateObject = null;
    [SerializeField, Tooltip("All possible combinations (leave out the alternative and special combination). Put only on the read object/collider")] private Combinations[] possibleCombinations = null;

    [Header("Debug")]
    [SerializeField] private Combinations combination = null;
    [SerializeField] private List<ObjectCard> objectCards = new List<ObjectCard>();
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        //Check to see if the gameObject contains a trigger.
        foreach (Collider collider in gameObject.GetComponents<Collider>())
        {
            if (collider.isTrigger) return;
        }
        Debug.LogWarning(gameObject.name + " doesn't contain a trigger.");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<ObjectCardHolder>() && collider.gameObject.GetComponent<ObjectCardHolder>().objectCard != null)
        {
            gameObjects.Add(collider.gameObject);
            //Checks for different cards being held. And adds them to stored list of cards in trigger.
            foreach (ObjectCardHolder objectCardHolder in collider.gameObject.GetComponents<ObjectCardHolder>())
            {
                ObjectCard objectCard = objectCardHolder.objectCard;
                //Activates special actions for certain ObjectCards
                foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onEnter.Invoke();
                objectCards.Add(objectCard);
                PushCardToPuzzleOneObject(objectCard);
                PushToCrate(objectCard, collider.gameObject);
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
            //Checks for different cards being held. And removes them from the stored list of cards in trigger.
            foreach (ObjectCardHolder objectCardHolder in collider.gameObject.GetComponents<ObjectCardHolder>())
            {
                ObjectCard objectCard = objectCardHolder.objectCard;
                foreach (ObjectCardAction objectCardAction in cardAction) if (objectCardAction.card == objectCard) objectCardAction.onLeave.Invoke();
                objectCards.Remove(objectCard);
                if (combinationComponent == CombinationComponent.read) PushCombination();
                if (combinationComponent == CombinationComponent.write) PushCardAndObject(objectCards.Count == 1 ? objectCards[0] : null, gameObjects.Count == 1 ? gameObjects[0] : null);
            }
        }
    }

    public bool PushCombination()
    {
        if (combinationObject == null) return false;
        CombinationCheck();
        combinationObject.CatchPossibleCombinations(gameObjects.ToArray(), combination);
        return true;
    }

    public bool PushToCrate(ObjectCard card, GameObject gameobject)
    {
        if (crateObject == null) return false;
        crateObject.ReceiveCard(card , gameobject);
        return true;
    }

    public bool PushCardAndObject(ObjectCard card, GameObject gameObject)
    {
        if (combinationObject == null) return false;
        combinationObject.CatchEmptyComponent(gameObject, card);
        return true;
    }

    public bool PushCardToPuzzleOneObject(ObjectCard card)
    {
        if (puzzleOneObject == null) return false;
        puzzleOneObject.PictureGiven(card);
        return true;
    }

    private bool CombinationCheck()
    {
        combination = null;
        foreach (Combinations combinations in possibleCombinations)
        {
            if (combinations.combinesWith.Length == objectCards.Count)
            {
                List<ObjectCard> remainingCards = new List<ObjectCard>();
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

    public void ClearOnCombine()
    {
        combination = null;
        objectCards.Clear();
        gameObjects.Clear();
    }
}