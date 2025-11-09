using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EyeOpenFromCenterEffect : MonoBehaviour
{
    public Material eyeMaterial;
    public float openSpeed = 0.8f; // velocidad de apertura
    public float delayBeforeDisable = 0.2f; // opcional: quitar efecto al terminar

    private float openAmount = 0f;
    private bool opening = true;

    void Start()
    {
        openAmount = 0f;
        if (eyeMaterial != null)
            eyeMaterial.SetFloat("_OpenAmount", 0f);

        opening = true;
    }

    void Update()
    {
        if (opening)
        {
            openAmount += Time.deltaTime * openSpeed;

            if (eyeMaterial != null)
                eyeMaterial.SetFloat("_OpenAmount", openAmount);

            if (openAmount >= 1f)
            {
                opening = false;
                Invoke(nameof(DisableEffect), delayBeforeDisable);
            }
        }
    }

    void DisableEffect()
    {
        Destroy(this); // elimina el script (efecto terminado)
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (eyeMaterial != null)
            Graphics.Blit(src, dest, eyeMaterial);
        else
            Graphics.Blit(src, dest);
    }
}
