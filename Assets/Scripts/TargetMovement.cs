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

    public bool facing_right = false;

    void Start()
    {
        target = GameObject.Find("Player");
    }

    public void SetTarget(GameObject o)
    {
        target = o;
        Vector3 diff = target.transform.position - transform.position;
        if (diff.x > 0)
        {
            facing_right = true;
        }
        else
        {
            facing_right = false;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180, 0));
        }
    }

    void FixedUpdate()
    {
        Vector3 diff = target.transform.position - transform.position;
        if (diff.x > 0 && facing_right)
        {
            facing_right = true;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0, 0));
        }
        else if (diff.x < 0 && !facing_right)
        {
            facing_right = false;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180, 0));
        }

        diff.Normalize();
        transform.Translate(diff * speed * speed_multiplier * Time.fixedDeltaTime, Space.World);
    }
}
