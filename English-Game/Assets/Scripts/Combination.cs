using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------
    //Actually performs the combination of the
    //objects.
    //------------------------------------------
    //Last Modification Time: 12:44 08/01/2020

    [SerializeField, Tooltip("The empty component that's required for the combination, for instance the empty bottle.")] private ObjectCard emptyComponent;
    [SerializeField, Tooltip("The resulting component, for instance the full bottle.")] private ObjectCard fullComponent;
    [SerializeField, Tooltip("If the game doesn't receive a correct combination object, it will create this instead.")] private Combinations alternativeComponent;

    [HideInInspector] private bool canCombine;
    [HideInInspector] private Combinations possibleCombination;
    [HideInInspector] private GameObject[] gameObjectInCombination;
    [HideInInspector] private GameObject emptyComponentGameObject;
    [HideInInspector] private ObjectCard receivedComponent;

    private void Awake()
    {
        if (emptyComponent == null) Debug.LogError("No empty component was assigned.");
        if (fullComponent == null) Debug.LogError("No full component was assigned.");
        if (alternativeComponent == null) Debug.LogError("Alternative component wasn't assigned.");
    }

    public bool CatchPossibleCombinations(GameObject[] gameObjects, Combinations combination)
    {
        if (gameObjectInCombination == gameObjects) gameObjectInCombination = null;
        if (possibleCombination == combination) possibleCombination = null;
        canCombine = CheckIfCanCombine();
        return canCombine;
    }

    public bool CatchEmptyComponent(GameObject gameObject, ObjectCard objectCard)
    {
        if (emptyComponentGameObject == gameObject) emptyComponentGameObject = null;
        if (receivedComponent == objectCard) receivedComponent = null;
        canCombine = CheckIfCanCombine();
        return canCombine;
    }

    public void Combine()
    {
        if (canCombine)
        {
            ApplyCombine(possibleCombination);
        } else if (gameObjectInCombination.Length > 0 && emptyComponentGameObject != null && receivedComponent == emptyComponent)
        {
            ApplyCombine(alternativeComponent);
        }
    }

    private void ApplyCombine(Combinations combinations)
    {
        foreach (GameObject gameObject in gameObjectInCombination) Destroy(gameObject);
        GameObject newObject = Instantiate(fullComponent.prefab, emptyComponentGameObject.transform);
        if (newObject.GetComponent<ObjectCardHolder>() == false) newObject.AddComponent<ObjectCardHolder>();
        newObject.GetComponent<ObjectCardHolder>().objectCard = combinations.result;
        newObject.GetComponent<ColorChanger>().ChangeColor(combinations.result.prefab.GetComponent<MeshRenderer>().material.color);
        Destroy(emptyComponent);
        CatchPossibleCombinations(gameObjectInCombination, possibleCombination);
        CatchEmptyComponent(emptyComponentGameObject, receivedComponent);
    }

    private bool CheckIfCanCombine()
    {
        if (possibleCombination != null && gameObjectInCombination.Length > 0 && receivedComponent == emptyComponent && receivedComponent.assignedGameObject.Contains(emptyComponentGameObject))
        {
            return true;
        }
        return false;
    }
}
