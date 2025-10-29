using UnityEngine;

public class FogAnimation : MonoBehaviour
{
    public float speedX = 0.01f;
    public float speedY = 0.0f;
    private Renderer rend;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset.x += speedX * Time.deltaTime;
        offset.y += speedY * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}
