using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float dash_speed = 25f;
    public float dash_duration = .5f;


    private Rigidbody2D rb;
    private Animator animator;
    private float dash_timer = 0f;
    private bool facing_right = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if (horizontalInput > 0 && !facing_right)
        {
            facing_right = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0, 0));
        }
        else if (horizontalInput < 0 && facing_right)
        {
            facing_right = false;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180, 0));
        }

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        if (movement == Vector3.zero)
        {
            Debug.Log("HERE");
            animator.SetBool("walking", false);
            return;
        }

        movement.Normalize();
        animator.SetBool("walking", true);

        if (dash_timer > 0)
        {
            transform.Translate(movement * dash_speed * Time.fixedDeltaTime, Space.World);
            dash_timer -= Time.fixedDeltaTime;
        }
        else
        {
            transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);
        }

    }
}
