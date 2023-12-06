using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject heath_bar;

    [Header("Details")]
    [SerializeField] public float max_health;
    [SerializeField] private float y_diff;
    [SerializeField] private float scale = 1;

    private SpriteRenderer health_image;
    public float health;
    private System.Action<GameObject> on_death_callback;
    private System.Action<GameObject> on_damage_callback;

    void Awake()
    {
        health = max_health;
        GameObject o = Instantiate(heath_bar, transform);
        o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y + y_diff, o.transform.position.z);
        o.transform.localScale = new Vector3(scale, scale, scale);
        health_image = o.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color c)
    {
        health_image.color = c;
    }

    public void SetMaxHealth(float max_health)
    {
        float old_max_health = this.max_health;
        this.max_health = max_health;
        this.health += (this.max_health - old_max_health);
        float fill = health / max_health;
        health_image.size = new Vector2(fill, health_image.size.y);
    }

    public void SetDeathCallback(System.Action<GameObject> callback)
    {
        on_death_callback = callback;
    }

    public void SetDamageCallback(System.Action<GameObject> callback)
    {
        on_damage_callback = callback;
    }

    public void TakeDamage(float damage)
    {
        float new_health = Mathf.Clamp(health - damage, 0f, max_health);
        SetHealth(new_health);
    }

    public void HealDamage(float damage)
    {
        float new_health = Mathf.Clamp(health + damage, 0f, max_health);
        SetHealth(new_health);
    }

    private void SetHealth(float new_health)
    {
        float diff = new_health - health;

        health = new_health;
        float fill = health / max_health;
        health_image.size = new Vector2(fill, health_image.size.y);
        Debug.Log("HEATH: " + health);

        if (health <= 0f)
        {
            on_death_callback?.Invoke(gameObject);
        }
        else if (diff < 0)
        {
            on_damage_callback?.Invoke(gameObject);
        }
    }
}
