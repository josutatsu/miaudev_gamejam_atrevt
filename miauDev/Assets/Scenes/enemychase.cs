using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
