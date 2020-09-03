using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCardHolder : MonoBehaviour
{
    //-------------------------------------------
    //Script holds an objectcard for this object
    //and assigns this object to the card.
    //-------------------------------------------

    [SerializeField] public ObjectCard objectCard;

    private void OnEnable()
    {
        if (objectCard != null)
        {
            objectCard.assignedGameObject.Add(gameObject);
            return;
        }
        Debug.LogWarning("A card wasn't assigned: " + gameObject.name);
    }

    private void OnDestroy()
    {
        if (objectCard != null)
        {
            objectCard.assignedGameObject.Remove(gameObject);
            return;
        }
        Debug.LogWarning("An object wasn't assigned: " + gameObject.name);
    }
}
