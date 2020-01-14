using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restock : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [SerializeField] private ObjectCard card = null;

    private GameObject prefab = null;
    private GameObject son = null;

    private void Awake()
    {
        prefab = card.prefab;
    }

    private void Update()
    {
        if (prefab != null && son == null)
        {
            son = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + prefab.GetComponent<Collider>().bounds.extents.y, transform.position.z), transform.rotation);
        }
    }
}
