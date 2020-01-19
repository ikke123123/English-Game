using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect : MonoBehaviour
{
    [SerializeField] private Shader effect;

    private Material material;

    private void Start()
    {
        if (TryCreateMaterial() == false)
        {
            Debug.LogError("Shader incorrect.");
        }
    }

    private void OnEnable()
    {
        TryCreateMaterial();
    }

    private bool TryCreateMaterial()
    {
        if (effect == null) return false;
        if (material == null) material = new Material(effect);
        return (material != null);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
