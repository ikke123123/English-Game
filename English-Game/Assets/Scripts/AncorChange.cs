using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncorChange : MonoBehaviour
{
    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Ancor pos once it changes + 1 for the hight
    public void ChangeAnchor(Vector3 pos)
    {
        transform.position = pos;
        gameObject.transform.Translate(new Vector3(0, 1, 0));
    }


}
