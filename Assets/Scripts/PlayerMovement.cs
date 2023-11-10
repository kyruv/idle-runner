using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float dash_speed = 25f;
    public float dash_duration = .5f;


    private Rigidbody2D rb;
    private float dash_timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dash_timer <= 0)
        {
            dash_timer = dash_duration;
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        movement.Normalize();

        if (dash_timer > 0)
        {
            rb.MovePosition(transform.position + (movement * dash_speed * Time.fixedDeltaTime));
            dash_timer -= Time.fixedDeltaTime;
        }
        else
        {
            rb.MovePosition(transform.position + (movement * speed * Time.fixedDeltaTime));
        }

    }
}
