using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TentDialog : MonoBehaviour
{

    public GameObject dialog;
    private RunManager runManager;
    private PlayerStats playerStats;

    void Start()
    {
        runManager = GameObject.Find("RunManager").GetComponent<RunManager>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        dialog.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (runManager.in_run)
        {
            return;
        }

        if (other.CompareTag("player"))
        {
            dialog.SetActive(true);
            dialog.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Darkest Night Survived: " + playerStats.darkest_survived;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            dialog.SetActive(false);
        }
    }
}
