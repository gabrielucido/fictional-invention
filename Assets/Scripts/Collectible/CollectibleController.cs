using System;
using UnityEngine;

public class CollectibleController : MonoBehaviour, ICollectible
{
    public event Action Collected;

    public void Collect(GameObject by)
    {
        Collected?.Invoke();
        Destroy(gameObject);
    }
}