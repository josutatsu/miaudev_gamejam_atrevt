using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class DreamNostalgiaEffect : MonoBehaviour
{
    public Shader shader;
    private Material _mat;

    [Header("Efecto base")]
    [Range(0, 5)] public float blurStrength = 1.0f;
    [Range(0, 0.05f)] public float distortion = 0.01f;
    [Range(0, 5)] public float timeSpeed = 1.0f;
    public Color tintColor = new Color(1f, 0.9f, 0.8f);

    [Header("Bordes nublados")]
    [Range(0, 3)] public float edgeFog = 1.0f;          // Densidad de la niebla
    [Range(0.1f, 1.0f)] public float edgeStart = 0.3f;  // Punto donde empieza a desvanecer (radio interno)
    [Range(0.3f, 1.5f)] public float edgeEnd = 0.8f;    // Punto donde la niebla llega a su máximo (radio externo)

    void Start()
    {
        if (shader == null)
            shader = Shader.Find("Hidden/DreamNostalgiaEffect");
        if (shader == null || !shader.isSupported)
            enabled = false;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_mat == null)
            _mat = new Material(shader);

        // Propiedades visuales
        _mat.SetFloat("_BlurStrength", blurStrength);
        _mat.SetFloat("_Distortion", distortion);
        _mat.SetFloat("_TimeSpeed", timeSpeed);
        _mat.SetColor("_TintColor", tintColor);

        // Parámetros del efecto de bordes
        _mat.SetFloat("_EdgeFog", edgeFog);
        _mat.SetFloat("_EdgeStart", edgeStart);
        _mat.SetFloat("_EdgeEnd", edgeEnd);

        Graphics.Blit(src, dest, _mat);
    }
}
