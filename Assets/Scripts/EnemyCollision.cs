using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
