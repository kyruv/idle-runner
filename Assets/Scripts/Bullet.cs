using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100;
    public float damage = .5f;

    private Vector3 dir = Vector3.zero;
    private Rigidbody2D rb;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        audioSource.time = .1f;
        audioSource.Play();
    }

    public void SetDir(Vector2 d)
    {
        dir = new Vector3(d.x, d.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "map")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            // transform.Translate(dir * speed * Time.fixedDeltaTime, Space.World);
            rb.MovePosition(transform.position + (dir * speed * Time.fixedDeltaTime));
        }
    }
}
