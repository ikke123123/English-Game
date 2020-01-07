using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncorChange : MonoBehaviour
{
    Vector3 currentPos;
    private Transform playerChild;
    private Vector3 playerPos;
    public Vector3 difference;
    // Start is called before the first frame update
    void Start()
    {
        playerChild = this.gameObject.transform.GetChild(0);
        playerPos = playerChild.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        difference.x = currentPos.x - playerPos.x;
        difference.z = currentPos.z - playerPos.z;
    }

    //Ancor pos once it changes + 1 for the hight
    public void ChangeAnchor(Vector3 pos)
    {
        transform.position = pos;
        gameObject.transform.Translate(new Vector3(0, 1, 0));
    }


}
