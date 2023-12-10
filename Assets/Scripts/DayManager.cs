using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject player;
    private PlayerStats playerStats;

    private Light2D[] lights;

    private AudioSource audioClip;

    public bool full_day = true;

    // Start is called before the first frame update
    void Start()
    {
        audioClip = GetComponent<AudioSource>();
        NightProgress(1f);
        FullDay();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void NightProgress(float d)
    {
        if (d != 1 && audioClip.isPlaying)
        {
            audioClip.Stop();
            full_day = false;
        }
        volume.weight = 1f - .45f * d;
        lights = player.GetComponentsInChildren<Light2D>();
        foreach (Light2D l in lights)
        {
            l.intensity = 30 - 20 * d;
        }
    }

    public void FullDay()
    {
        audioClip.Play();
        volume.weight = .4f;
        lights = player.GetComponentsInChildren<Light2D>();
        player.GetComponent<Gun>().can_shoot = false;
        foreach (Light2D l in lights)
        {
            l.intensity = 3;
        }

        full_day = true;
    }

    void Update()
    {
        if (!full_day)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerStats.AddGunExp(Random.Range(10, 30));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerStats.AddDexterityExp(Random.Range(10, 30));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerStats.AddEduranceExp(Random.Range(10, 30));
        }
    }
}
