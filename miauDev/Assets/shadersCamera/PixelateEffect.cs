using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PixelateEffect : MonoBehaviour
{
    public Shader pixelateShader;
    [Range(64, 512)]
    public float pixelDensity = 128f;
    private Material pixelateMaterial;

    void Start()
    {
        if (pixelateShader == null)
            pixelateShader = Shader.Find("Custom/Pixelate");

        if (pixelateShader != null)
            pixelateMaterial = new Material(pixelateShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pixelateMaterial != null)
        {
            pixelateMaterial.SetFloat("_PixelSize", pixelDensity);
            Graphics.Blit(source, destination, pixelateMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
