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
    //Last Modification Time: 22:21 07/01/2020

    [SerializeField, Tooltip("The empty component that's required for the combination, for instance the empty bottle.")] private ObjectCard emptyComponent;

    [HideInInspector] private bool canCombine;
    [HideInInspector] private Combinations possibleCombination;
    [HideInInspector] private GameObject[] gameObjectInCombination;
    [HideInInspector] private GameObject emptyComponentGameObject;
    [HideInInspector] private ObjectCard receivedComponent;

    public void CatchPossibleCombinations(GameObject[] gameObjects, Combinations combination)
    {
        if (gameObjectInCombination == gameObjects) gameObjectInCombination = null;
        if (possibleCombination == combination) possibleCombination = null;
        canCombine = CheckIfCanCombine();
    }

    public void CatchEmptyComponent(GameObject gameObject, ObjectCard objectCard)
    {
        if (emptyComponentGameObject == gameObject) emptyComponentGameObject = null;
        if (receivedComponent == objectCard) receivedComponent = null;
        canCombine = CheckIfCanCombine();
    }

    public void Combine()
    {
        if (canCombine)
        {

        }
    }

    private bool CheckIfCanCombine()
    {
        if (possibleCombination != null && gameObjectInCombination.Length != 0 && receivedComponent == emptyComponent && receivedComponent.assignedGameObject.Contains(emptyComponentGameObject))
        {
            return true;
        }
        return false;
    }
}
