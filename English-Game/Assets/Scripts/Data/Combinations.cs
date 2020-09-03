using UnityEngine;

[CreateAssetMenu(fileName = "New Combination Card", menuName = "Custom/Combination Card")]
public class Combinations : ScriptableObject
{
    //------------------------------------------
    //Stores combinations of ObjectCards.
    //------------------------------------------

    [SerializeField] public ObjectCard[] combinesWith;
    [SerializeField] public ObjectCard result;
}
