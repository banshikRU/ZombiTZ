using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerBehaviour : MonoBehaviour
{
    public event Action OnPlayerDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ZombieBehaviour>(out ZombieBehaviour zombie))
        {
            OnPlayerDeath.Invoke();
        }
    }
}
