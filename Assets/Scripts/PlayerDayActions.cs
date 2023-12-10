using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDayActions : MonoBehaviour
{
    [Header("References")]
    public GameObject level_starter;

    private bool touching_tent = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tent"))
        {
            touching_tent = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("tent"))
        {
            touching_tent = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touching_tent)
        {
            if (Input.GetKey("e"))
            {
                level_starter.SetActive(true);
            }
        }
    }
}
