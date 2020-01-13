using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncorChange : MonoBehaviour
{
    Vector3 currentPos;
    public Transform playerChild;
    private Vector3 playerPos;
    public Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {
        //playerChild = this.gameObject.transform.GetChild(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        currentPos = transform.position;
        playerPos = playerChild.transform.position;
        difference.x = playerPos.x - currentPos.x;
        difference.z = playerPos.z - currentPos.z;
        //Debug.Log("ANCHOR POS: " + transform.position + " | PLAYER POS: " + playerPos);
    }

    //Ancor pos once it changes + 1 for the hight
    public void ChangeAnchor(Vector3 pos)
    {
        transform.position = pos;
        gameObject.transform.Translate(new Vector3(0, 0, 0));
    }
}
