using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ObjectCardAction
{
    //------------------------------------------
    //Stores actions that need to happen when
    //the ObjectCard leaves or enters a reader.
    //------------------------------------------

    public ObjectCard card;
    public UnityEvent onEnter;
    public UnityEvent onLeave;
}
