using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class combineChecker : MonoBehaviour
{
    private CollisionChecker ChildL;
    private CollisionChecker ChildR;

    private CollisionChecker[] children;

    BoxType boxColorL;
    BoxType boxColorR;

    public GameObject greenBlock;

    // Start is called before the first frame update
    void Start()
    {
        //Find child L
        ChildL = this.transform.GetChild(0).GetComponent<CollisionChecker>();

        //find child R
        ChildR = this.transform.GetChild(1).GetComponent<CollisionChecker>();

        children = GetComponentsInChildren<CollisionChecker>();
    }

    // Update is called once per frame
    void Update()
    {      
        if (children != null && children.Length > 1)
        {
            //creates an array from the children boxtypes
            var boxTypes = children.Select(child => child.boxType).ToArray();
            //Filters out nulls, and checks if every type is not null
            if (boxTypes.Where(type => type != null).ToArray().Length == boxTypes.Length)
            {
                //Creates an array of colors
                var boxColors = boxTypes.Select(type => type.boxColor).ToArray();

                //====Here is the list for all the combinations====
                //Checks if there are 2 different colored boxes 
                if (boxColors.Contains(BoxType.BlueBox) && boxColors.Contains(BoxType.OrangeBox))
                {
                    Debug.Log(boxColors);
                    if (ChildR.boxType.gameObject.GetComponent<OVRGrabbable>().isGrabbed == false && ChildL.boxType.gameObject.GetComponent<OVRGrabbable>().isGrabbed == false && OVRInput.Get(OVRInput.Button.Two)) { 
                    Destroy(ChildR.boxType.gameObject);
                    Destroy(ChildL.boxType.gameObject);
                    Instantiate(greenBlock, transform.position, transform.rotation);
                    }
                }
                //Checks if there are 2 or more of the same color
                else if (boxColors.Where(color => color == BoxType.BlueBox).ToArray().Length >= 2)
                {
                    Debug.Log("Super Blue");
                }

                if (boxColors.Contains(BoxType.BlueBox) && boxColors.Contains(BoxType.GreenBox))
                {
                    Debug.Log(boxColors);
                    if (ChildR.boxType.gameObject.GetComponent<OVRGrabbable>().isGrabbed == false && ChildL.boxType.gameObject.GetComponent<OVRGrabbable>().isGrabbed == false && OVRInput.Get(OVRInput.Button.Two))
                    {
                        Destroy(ChildR.boxType.gameObject);
                        Destroy(ChildL.boxType.gameObject);
                        Instantiate(greenBlock, transform.position, transform.rotation);
                    }
                }
            }
        }
    }
}
