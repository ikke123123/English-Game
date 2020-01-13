using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePicture : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private MeshRenderer toSetTo;

    private void OnEnable()
    {
        toSetTo.material = material;
    }
}
