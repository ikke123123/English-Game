using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public Vector3 TeleportLocation;
    public GameObject Ancor;

    public void PlayLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MoveSomewhere()
    {
        Ancor.transform.position = TeleportLocation;
    }
}
