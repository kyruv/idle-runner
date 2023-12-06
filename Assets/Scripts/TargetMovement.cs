using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject target;

    public float speed = 1.75f;
    public float speed_multiplier = 1f;

    void Start()
    {
        target = GameObject.Find("Player");
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }

    void FixedUpdate()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        transform.Translate(diff * speed * speed_multiplier * Time.fixedDeltaTime, Space.World);
    }
}
