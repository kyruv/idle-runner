using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatHover : MonoBehaviour
{
    public GameObject popup;
    private string hovering = "";

    void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }

    public void MaybeUpdateHover(string skill, int xp)
    {
        if (skill == hovering)
        {
            popup.GetComponentInChildren<TextMeshProUGUI>().text = xp.ToString("N0") + " xp";
        }
    }

    public void StartHover(string skill)
    {
        hovering = skill;
        if (popup != null)
        {
            popup.SetActive(true);
            if (skill == "bullet")
            {
                popup.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<PlayerStats>().gun_exp.ToString("N0") + " xp";
            }
            else if (skill == "dexterity")
            {
                popup.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<PlayerStats>().dexterity_exp.ToString("N0") + " xp";
            }
            else if (skill == "endurance")
            {
                popup.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<PlayerStats>().endurance_exp.ToString("N0") + " xp";
            }
        }
    }

    public void EndHover()
    {
        hovering = "";
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}