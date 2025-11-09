using UnityEngine;

public class Item2 : MonoBehaviour
{
    public ItemCollector collector;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collector != null)
            {
                collector.CollectItem(gameObject);
            }
        }
    }
}
