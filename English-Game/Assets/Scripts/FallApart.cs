using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallApart : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DetachChildren();
    }
}
