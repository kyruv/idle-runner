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
            GetComponent<Health>().TakeDamage(.5f);
        }
        else if (collision.collider.CompareTag("player"))
        {
            collision.collider.gameObject.GetComponent<Health>().TakeDamage(1);
            Debug.Log("Hit Player");
            Destroy(gameObject);
        }
    }
}
