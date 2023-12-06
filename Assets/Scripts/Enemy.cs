using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int num_souls = 1;
    public float damage;

    public void Consume(Enemy them)
    {
        num_souls += them.num_souls;
        float scale = Mathf.Min(.4f + (num_souls / 15.0f), 5f);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        Health me = GetComponent<Health>();
        Health themH = them.gameObject.GetComponent<Health>();
        me.SetMaxHealth(me.max_health + 1.25f * themH.health);
        GetComponent<TargetMovement>().speed_multiplier = Mathf.Min(1f + num_souls / 50f, 4f);
        Destroy(them.gameObject);
    }

    public void DamagedPlayer()
    {
        if (num_souls == 1)
        {
            Destroy(gameObject);
            return;
        }
        num_souls /= 2;
        float scale = Mathf.Min(.4f + (num_souls / 15.0f), 5f);
        GetComponent<TargetMovement>().speed_multiplier = Mathf.Min(1f + num_souls / 50f, 4f);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
        Health me = GetComponent<Health>();
        me.SetMaxHealth(me.max_health / 2);

        GameObject duplicate = Instantiate(gameObject, transform.position, Quaternion.identity);
        Vector2 a = GetRandomUnitVector2();
        Vector2 b = GetRandomUnitVector2();

        duplicate.GetComponent<Rigidbody2D>().AddForce(250 * a, ForceMode2D.Impulse);
        duplicate.GetComponent<TargetMovement>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(250 * b, ForceMode2D.Impulse);
        GetComponent<TargetMovement>().enabled = false;

        duplicate.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Utility.instance.WithDelay(1f, () =>
        {
            if (duplicate != null)
            {
                duplicate.GetComponent<Collider2D>().enabled = true;
                duplicate.GetComponent<Rigidbody2D>().AddForce(-200 * a, ForceMode2D.Impulse);
                duplicate.GetComponent<TargetMovement>().enabled = true;
            }
            if (this != null)
            {
                GetComponent<Collider2D>().enabled = true;
                GetComponent<Rigidbody2D>().AddForce(-200 * b, ForceMode2D.Impulse);
                GetComponent<TargetMovement>().enabled = true;
            }
        });

    }

    Vector2 GetRandomUnitVector2()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Use trigonometric functions to convert the angle to a unit vector
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Sin(randomAngle);

        return new Vector2(x, y);
    }

    void Start()
    {
        Color original = GetComponent<SpriteRenderer>().color;
        System.Action<GameObject> enemy_damage = (obj) =>
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                sr.color = Color.white;
                Utility.instance.WithDelay(.1f, () =>
                {
                    if (obj != null)
                    {
                        sr.color = original;
                    }
                });
            };
        System.Action<GameObject> enemy_death = (obj) =>
            {
                Destroy(obj);
            };

        Health h = GetComponent<Health>();
        h.SetColor(Color.yellow);
        h.SetDamageCallback(enemy_damage);
        h.SetDeathCallback(enemy_death);
    }
}
