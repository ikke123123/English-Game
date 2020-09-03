using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour
{
    //------------------------------------------
    //Actually performs the combination of the
    //objects.
    //------------------------------------------

    [Header("Settings")]
    [SerializeField, Tooltip("The empty component that's required for the combination, for instance the empty bottle.")] private ObjectCard emptyComponent;
    [SerializeField, Tooltip("The resulting component, for instance the full bottle.")] private ObjectCard fullComponent;
    [SerializeField, Tooltip("If the game doesn't receive a correct combination object, it will create this instead.")] private Combinations alternativeComponent;
    [SerializeField, Tooltip("A potion that can be unlocked through the specialCombinationEnabled bool.")] private Combinations specialCombination;
    //ReadObjectCard scripts maintain a list of Objects with the ObjectCard script attached within their trigger collider.
    [SerializeField, Tooltip("The ReadObjectCards whose trigger collider will be emptied upon combination.")] private ReadObjectCard[] clearOnCombine;

    [Header("Constant")]
    //One time disposable sound player.
    [SerializeField] private GameObject simpleSoundCardPlayer;
    [SerializeField] private Soundcard playOnCombine;

    [Header("Debug")]
    public bool specialCombinationEnabled = false;
    [SerializeField] private bool canCombine = false;
    [SerializeField] private Combinations possibleCombination;
    [SerializeField] private GameObject[] gameObjectsInCombination;
    //The bottle that is to be replaced.
    [SerializeField] private GameObject emptyComponentGameObject;
    [SerializeField] private ObjectCard receivedComponent;

    private void Awake()
    {
        //Error checks.
        if (emptyComponent == null) Debug.LogError("No empty component was assigned.");
        if (fullComponent == null) Debug.LogError("No full component was assigned.");
        if (alternativeComponent == null) Debug.LogError("Alternative component wasn't assigned.");
    }

    //Receives the gameObjects present in the ingredient side of combination device, in combination with the possible combinations that are stored in the ReadObjectCard script.
    public bool CatchPossibleCombinations(GameObject[] gameObjects, Combinations combination)
    {
        gameObjectsInCombination = gameObjects;
        possibleCombination = combination;
        canCombine = CheckIfCanCombine();
        return canCombine;
    }

    //Receives the gameObject present and the objectCard of that object bottle side of combination device
    public bool CatchEmptyComponent(GameObject gameObject, ObjectCard objectCard)
    {
        emptyComponentGameObject = gameObject;
        receivedComponent = objectCard;
        canCombine = CheckIfCanCombine();
        return canCombine;
    }

    //Combine is activated through a button using Unity's Event System.
    public void Combine()
    {
        if (canCombine) ApplyCombine();
    }

    public void EnableSpecialComponent(bool enabled)
    {
        specialCombinationEnabled = enabled;
    }

    //Executes the necessary checks and starts process of combining.
    private void ApplyCombine()
    {
        //Creates Sound Object and starts playing the sound.
        GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
        audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(playOnCombine);
        if (possibleCombination == null) possibleCombination = alternativeComponent;
        foreach (GameObject gameObject in gameObjectsInCombination) Destroy(gameObject);
        //Makes bottle ungrabbable
        emptyComponentGameObject.layer = 16;
        Invoke("SpawnNewBottle", 3);
        gameObjectsInCombination = null;
        foreach (ReadObjectCard readObjectCard in clearOnCombine) readObjectCard.ClearOnCombine();
        canCombine = false;
    }

    private void SpawnNewBottle()
    {
        GameObject newObject = Instantiate(fullComponent.prefab, emptyComponentGameObject.transform.position, emptyComponentGameObject.transform.rotation);
        if (newObject.GetComponent<ObjectCardHolder>() == false) newObject.AddComponent<ObjectCardHolder>();
        newObject.GetComponent<ObjectCardHolder>().objectCard = possibleCombination.result;
        //Sets color of liquid to the color of the prefab cube.
        newObject.GetComponent<ColorChanger>().ChangeColor(possibleCombination.result.prefab.GetComponent<MeshRenderer>().sharedMaterial.GetColor("_Color"));
        //Destroy the empty bottle.
        Destroy(emptyComponentGameObject);
        possibleCombination = null;
    }

    private bool CheckIfCanCombine()
    {
        if (gameObjectsInCombination.Length > 0 && receivedComponent == emptyComponent && receivedComponent.assignedGameObject.Contains(emptyComponentGameObject) && emptyComponentGameObject != null)
        {
            return true;
        }
        return false;
    }
}
