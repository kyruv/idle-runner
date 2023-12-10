using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI gunLevel;
    public TextMeshProUGUI dexterityLevel;
    public TextMeshProUGUI enduranceLevel;
    public GameObject xpDrop;
    public StatHover statHover;

    [Header("Gun")]
    public int gun_exp;
    public int gun_level;
    public float damage;
    public int shotgun_spread;

    [Header("Dexterity")]
    public int dexterity_exp;
    public int dexterity_level;
    public float reload_time;
    public float fire_rate;
    public int clip_size;

    [Header("Endurance")]
    public int endurance_exp;
    public int endurance_level;
    public float health;
    public float speed;
    public float dash_cooldown;

    public int darkest_survived = 0;

    private List<int> level_breakpoints = new List<int>
        {
            100, 107, 115, 123, 132, 141, 151, 162, 174, 186,
            200, 214, 229, 245, 263, 282, 302, 324, 347, 372,
            400, 429, 460, 493, 528, 566, 607, 651, 698, 748,
            800, 857, 919, 985, 1056, 1132, 1213, 1300, 1393, 1493,
            1600, 1715, 1838, 1970, 2111, 2263, 2425, 2599, 2786, 2986,
            3200, 3430, 3676, 3940, 4223, 4526, 4851, 5199, 5572, 5972,
            6400, 6859, 7351, 7879, 8445, 9051, 9701, 10397, 11143, 11943,
            12800, 13719, 14704, 15759, 16890, 18102, 19401, 20793, 22285, 23884,
            25600, 27437, 29406, 31517, 33779, 36203, 38801, 41586, 44571, 47770,
            52000, 55732, 59732, 64019, 68614, 73539, 78817, 84474, 90537, 97035,
            104000
        };

    void Start()
    {
        UpdateGun();
        UpdateDexterity();
        UpdateEdurance();
    }

    public void AddGunExp(int xp)
    {
        SpawnXp(xp);
        gun_exp += xp;
        UpdateGun();
        statHover.MaybeUpdateHover("bullet", gun_exp);
    }

    public void AddDexterityExp(int xp)
    {
        SpawnXp(xp);
        dexterity_exp += xp;
        UpdateDexterity();
        statHover.MaybeUpdateHover("dexterity", dexterity_exp);
    }

    public void AddEduranceExp(int xp)
    {
        SpawnXp(xp);
        endurance_exp += xp;
        UpdateEdurance();
        statHover.MaybeUpdateHover("endurance", endurance_exp);
    }

    public void SpawnXp(int xp)
    {
        GameObject o = Instantiate(xpDrop, transform.position, Quaternion.identity);
        o.GetComponentInChildren<TextMeshProUGUI>().text = "+" + xp;
        StartCoroutine(MoveAndDestroyCoroutine(o));
    }

    IEnumerator MoveAndDestroyCoroutine(GameObject o)
    {
        // Move the object upward for 1 second
        float duration = 1.0f;
        float elapsedTime = 0f;
        Vector3 initialPosition = o.transform.position;
        Vector3 targetPosition = initialPosition + 2 * Vector3.up + Vector3.right;

        while (elapsedTime < duration)
        {
            o.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            o.transform.localScale = Vector3.one * (.5f + ((duration - elapsedTime) / duration));
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Destroy the object after 1 second
        Destroy(o);
    }

    private void UpdateGun()
    {
        gun_level = GetLevel(gun_exp);
        damage = .75f * gun_level + .5f;
        shotgun_spread = gun_level / 10 + 1;
        GetComponent<Gun>().UpdateGunStats();
        UpdateCanvas();
    }

    private void UpdateDexterity()
    {
        dexterity_level = GetLevel(dexterity_exp);
        fire_rate = 1.025f - dexterity_level / 100f;
        reload_time = 3.05f - dexterity_level / 33f;
        clip_size = 2 + 3 * (dexterity_level / 10);
        GetComponent<Gun>().UpdateDexterityStats();
        UpdateCanvas();
    }

    private void UpdateEdurance()
    {
        endurance_level = GetLevel(endurance_exp);
        health = 1 + endurance_level * 5;
        speed = 2f + endurance_level / 20f;
        dash_cooldown = 5.05f - .5f * (endurance_level / 10);
        GetComponent<Health>().SetMaxHealth(health);
        GetComponent<PlayerMovement>().dash_cooldown = dash_cooldown;
        GetComponent<PlayerMovement>().speed = speed;
        UpdateCanvas();
    }

    private int GetLevel(int xp)
    {
        int lxp = 0;
        for (int l = 1; l < 101; l++)
        {
            lxp += level_breakpoints[l - 1];
            if (lxp > xp)
            {
                return l;
            }
        }
        return 101;
    }

    private void UpdateCanvas()
    {
        gunLevel.text = "" + gun_level;
        dexterityLevel.text = "" + dexterity_level;
        enduranceLevel.text = "" + endurance_level;
    }
}
