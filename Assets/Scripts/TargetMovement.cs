using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject target;

    public float speed = 1.75f;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }

    void FixedUpdate()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        rb.MovePosition(transform.position + (diff * speed * Time.fixedDeltaTime));
    }
}
