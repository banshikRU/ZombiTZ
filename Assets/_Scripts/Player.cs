using System;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
          //  OnPlayerDeath.Invoke();
        }
    }
}
