using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    //Soundtack, played through the camera
    public AudioSource soundtrack;
    //new audio source, selected by player interaction
    //private GameObject player = GameObject.Find("PlayerBall");
    private AudioSource newSound;
    private GameObject selectedObj;
    
    private bool playSong;

    public float volLevel = 1;
    public bool playToggle;
    // Start is called before the first frame update
    void Start()
    {
        soundtrack = GetComponent<AudioSource>();
        playSong = true;


    }

    // Update is called once per frame
    void Update()
    {

        selectedObj = BallControl.selected;
        newSound = selectedObj.GetComponent<AudioSource>();
        

        if (playSong == true && playToggle == true)
        {
            volLevel = 0;

            
            playSong = false;
            playToggle = false;
        }

        if (playSong == false && playToggle == true)
        {
            volLevel = 1;
            
            
            playSong = true;
            playToggle = false;
        }

        soundtrack.volume = volLevel;

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Click");
            AudioInteract();
        }




    }

    public void AudioInteract()
    {
        if (newSound)
        {
            newSound.Play();
        }

    }
}
