using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            Enemy me = GetComponent<Enemy>();
            Enemy them = collision.collider.gameObject.GetComponent<Enemy>();
            if (me.num_souls >= them.num_souls)
            {
                me.Consume(them);
            }
            else
            {
                them.Consume(me);
            }
        }

        if (collision.collider.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
            GetComponent<Health>().TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
        }
        else if (collision.collider.CompareTag("player"))
        {
            Enemy us = GetComponent<Enemy>();
            collision.collider.gameObject.GetComponent<Health>().TakeDamage(us.damage);
            us.DamagedPlayer();
        }
    }
}
