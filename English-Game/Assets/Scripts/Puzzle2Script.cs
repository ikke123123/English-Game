using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Actually performs the combination of the
    //objects.
    //------------------------------------------
    //Last Modification Time: 12:44 08/01/2020

    [Header("Settings")]
    [SerializeField, Tooltip("The empty component that's required for the combination, for instance the empty bottle.")] private ObjectCard emptyComponent;
    [SerializeField, Tooltip("The resulting component, for instance the full bottle.")] private ObjectCard fullComponent;
    [SerializeField, Tooltip("If the game doesn't receive a correct combination object, it will create this instead.")] private Combinations alternativeComponent;
    [SerializeField] private ReadObjectCard[] clearOnCombine;

    [Header("Don't Touch")]
    [SerializeField] private GameObject simpleSoundCardPlayer;
    [SerializeField] private Soundcard playOnCombine;

    [Header("Debug")]
    [SerializeField] private bool canCombine = false;
    [SerializeField] private Combinations possibleCombination;
    [SerializeField] private GameObject[] gameObjectsInCombination;
    [SerializeField] private GameObject emptyComponentGameObject;
    [SerializeField] private ObjectCard receivedComponent;

    private void Awake()
    {
        if (emptyComponent == null) Debug.LogError("No empty component was assigned.");
        if (fullComponent == null) Debug.LogError("No full component was assigned.");
        if (alternativeComponent == null) Debug.LogError("Alternative component wasn't assigned.");
    }

    //Receives From Read
    public bool CatchPossibleCombinations(GameObject[] gameObjects, Combinations combination)
    {
        Debug.Log("Read");
        Debug.Log(combination);
        //if (gameObjectInCombination == gameObjects) gameObjectInCombination = null;
        //else
        gameObjectsInCombination = gameObjects;
        //if (possibleCombination == combination) possibleCombination = null;
        //else
        possibleCombination = combination;
        canCombine = CheckIfCanCombine();
        Debug.Log(canCombine);
        return canCombine;
    }

    //Receives from Write
    public bool CatchEmptyComponent(GameObject gameObject, ObjectCard objectCard)
    {
        Debug.Log("Write");
        //if (emptyComponentGameObject == gameObject) emptyComponentGameObject = null;
        //else
        emptyComponentGameObject = gameObject;
        //if (receivedComponent == objectCard) receivedComponent = null;
        //else
        receivedComponent = objectCard;
        canCombine = CheckIfCanCombine();
        Debug.Log(canCombine);
        return canCombine;
    }

    public void Combine()
    {
        Debug.Log("Combine void ran");
        if (canCombine)
        {
            ApplyCombine(/*possibleCombination*/);
            return;
        }
        if (gameObjectsInCombination != null && gameObjectsInCombination.Length > 0 && emptyComponentGameObject != null && receivedComponent != null && receivedComponent == emptyComponent)
        {
            ApplyCombine(/*alternativeComponent*/);
        }
    }

    private void ApplyCombine()
    {
        GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
        audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(playOnCombine);
        if (possibleCombination == null) possibleCombination = alternativeComponent;
        Debug.Log("Combine apply ran");
        foreach (GameObject gameObject in gameObjectsInCombination) Destroy(gameObject);
        emptyComponentGameObject.layer = 0;
        Invoke("SpawnNewBottle", 3);
        gameObjectsInCombination = null;
        foreach (ReadObjectCard readObjectCard in clearOnCombine) readObjectCard.ClearOnCombine();
        CatchPossibleCombinations(gameObjectsInCombination, possibleCombination);
        CatchEmptyComponent(emptyComponentGameObject, receivedComponent);
    }

    private void SpawnNewBottle()
    {
        GameObject newObject = Instantiate(fullComponent.prefab, emptyComponentGameObject.transform.position, emptyComponentGameObject.transform.rotation);
        if (newObject.GetComponent<ObjectCardHolder>() == false) newObject.AddComponent<ObjectCardHolder>();
        newObject.GetComponent<ObjectCardHolder>().objectCard = possibleCombination.result;
        newObject.GetComponent<ColorChanger>().ChangeColor(possibleCombination.result.prefab.GetComponent<MeshRenderer>().sharedMaterial.GetColor("_Color"));
        Destroy(emptyComponentGameObject);
        possibleCombination = null;
    }

    private bool CheckIfCanCombine()
    {
        Debug.Log("Can Combine?");
        Debug.Log(possibleCombination);
        if (possibleCombination != null && gameObjectsInCombination != null)
        {
            Debug.Log("0");
            if (gameObjectsInCombination.Length > 0)
            {
                Debug.Log("1");
                if (receivedComponent == emptyComponent)
                {
                    Debug.Log("2");
                    if (receivedComponent.assignedGameObject.Contains(emptyComponentGameObject))
                    {
                        Debug.Log("3");
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
