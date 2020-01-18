using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestockVendingMachine : MonoBehaviour
{
    //------------------------------------------
    //             Made By Thomas
    //------------------------------------------

    [Header("Settings")]
    [SerializeField] private bool disabled = false;

    [Header("Don't Touch")]
    [SerializeField] private ObjectCard card = null;
    [SerializeField] private Soundcard restockSound;
    [SerializeField] private GameObject simpleSoundCardPlayer;

    private GameObject prefab = null;
    private GameObject son = null;
    private bool firstRun = true;
    private float timer = 0;

    private void Awake()
    {
        prefab = card.prefab;
    }

    public void RestockerOff(bool off)
    {
        disabled = off;
    }

    private void Update()
    {
        if (disabled == false)
        {
            if (prefab != null && son == null)
            {
                if (timer == 0 && firstRun == false)
                {
                    timer = Time.time + 5.6f;
                    GameObject audioObject = Instantiate(simpleSoundCardPlayer, transform.position, transform.rotation);
                    audioObject.GetComponent<SimpleSoundCardPlayer>().StartPlaying(restockSound);
                }
                else if (timer <= Time.time)
                {
                    timer = 0;
                    firstRun = false;
                    son = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + prefab.GetComponent<Collider>().bounds.extents.y, transform.position.z), transform.rotation);
                }
            }
        }

    }
}
