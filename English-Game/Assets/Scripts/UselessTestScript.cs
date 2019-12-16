using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessTestScript : MonoBehaviour
{
    public enum Numbers { Bruh1, Bruh2, Bruh3, Bruh4, Bruh5, Bruh6};

    public void Bruh(int numberIdentifier)
    {
        Debug.Log((Numbers)numberIdentifier);
    }
}
