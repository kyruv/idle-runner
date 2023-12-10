using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private ScreenShake screen_shake;

    void Start()
    {
        screen_shake = Camera.main.gameObject.GetComponent<ScreenShake>();
    }

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

        if (collision.collider.CompareTag("pushback"))
        {
            Vector2 a = new Vector2(collision.collider.gameObject.transform.position.x, collision.collider.gameObject.transform.position.y) -
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            float dist = a.magnitude;
            a.Normalize();
            a *= 1 / dist;

            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(-100 * a, ForceMode2D.Impulse);
            gameObject.GetComponent<TargetMovement>().enabled = false;

            Utility.instance.WithDelay(1f, () =>
            {
                if (this != null)
                {
                    gameObject.GetComponent<Collider2D>().enabled = true;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(100 * a, ForceMode2D.Impulse);
                    gameObject.GetComponent<TargetMovement>().enabled = true;
                }
            });
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
            screen_shake.ShakeScreen();
        }
    }
}
