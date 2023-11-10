using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100;

    private Vector3 dir = Vector3.zero;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDir(Vector2 d)
    {
        dir = new Vector3(d.x, d.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            rb.MovePosition(transform.position + (dir * speed * Time.fixedDeltaTime));
        }
    }
}
