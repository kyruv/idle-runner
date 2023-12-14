using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemy_prefab;
    [SerializeField] private GameObject player;
    [SerializeField] private DayManager day;
    [SerializeField] private TextMeshProUGUI roundNum;
    [SerializeField] private GameObject selection;
    [SerializeField] private TextMeshProUGUI roundResult;

    [Header("Runner Config")]
    [SerializeReference] private int level = 1;
    [SerializeReference] private float level_time = 60;

    [SerializeReference] private float enemy_spawn_rate = 2f;
    [SerializeReference] private float enemy_spawn_rate_increase = .9f;
    [SerializeReference] private float time_till_increase = 2.5f;
    [SerializeReference] private float base_enemy_health = 2.5f;
    [SerializeReference] private float enemy_damage = 2.5f;
    [SerializeReference] private float enemy_speed = 2.5f;

    private float spawn_count_down = 0;
    private float difficulty_count_down = 0;
    private float level_timer = 0;

    private AudioSource audioClip;
    public bool in_run;

    void Start()
    {
        audioClip = GetComponent<AudioSource>();
        difficulty_count_down = time_till_increase;
        player.GetComponent<Gun>().can_shoot = false;
        SetLevel(level);
    }

    public void IncreaseLevel()
    {
        level += 1;
        if (level > 999)
        {
            level = 999;
        }
        SetLevel(level);
    }

    public void DecreaseLevel()
    {
        level -= 1;
        if (level <= 0)
        {
            level = 1;
        }
        SetLevel(level);
    }

    public int GetLevel()
    {
        return level;
    }

    public void PlayerDied()
    {
        in_run = false;
        spawn_count_down = 0;
        difficulty_count_down = 0;
        level_timer = 0;
        enemy_spawn_rate = 1000000000;
        audioClip.Stop();
        day.FullDay();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject o in enemies)
        {
            Destroy(o);
        }
        player.GetComponent<Health>().HealDamage(100000);
        roundResult.enabled = true;
        roundResult.text = "You Died";
        roundResult.color = Color.red;
        player.transform.position = new Vector3(16, 8, 0);

        Utility.instance.WithDelay(2.5f, () =>
        {
            roundResult.enabled = false;
        });
    }

    public void SetLevel(int level)
    {
        this.level = level;
        roundNum.text = "" + level;

        enemy_spawn_rate = .1f + 2.5f * ((100f - Mathf.Min(level, 100)) / 100f) + .5f * ((999f - Mathf.Min(level, 999f)) / 999f);
        enemy_spawn_rate_increase = .95f;
        time_till_increase = 5f - 4.5f * (999f - level) / 999f;
        base_enemy_health = 1 + level / 2f;
        enemy_damage = 1f + level / 5f;
        enemy_speed = 3f + 7.5f * level / 999f;
        level_timer = 0;
    }

    public void StartRun()
    {
        SetLevel(level);
        in_run = true;
        player.GetComponent<Gun>().can_shoot = true;
        audioClip.Play();
        selection.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!in_run)
        {
            return;
        }

        if (spawn_count_down <= 0)
        {
            SpawnEnemy();
        }
        spawn_count_down -= Time.deltaTime;
        level_timer += Time.deltaTime;
        day.NightProgress(level_timer / level_time);
        if (level_timer > level_time)
        {
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {
                Destroy(e);
            }
            enemy_spawn_rate = 1000000000;
            audioClip.Stop();
            day.FullDay();
            in_run = false;
            level_timer = 0;
            player.GetComponent<PlayerStats>().darkest_survived = Mathf.Max(level, player.GetComponent<PlayerStats>().darkest_survived);
            roundResult.enabled = true;
            roundResult.text = "You Lived";
            roundResult.color = new Color(50f / 255, 150f / 255, 25f / 255);
            player.transform.position = new Vector3(16, 8, 0);

            Utility.instance.WithDelay(2.5f, () =>
            {
                roundResult.enabled = false;
            });
        }

        if (difficulty_count_down <= 0)
        {
            enemy_spawn_rate *= enemy_spawn_rate_increase;
            base_enemy_health *= (1 / enemy_spawn_rate_increase);
            difficulty_count_down = time_till_increase;
        }
    }

    void SpawnEnemy()
    {
        float randomRadius = Random.Range(15f, 20f);
        float randomAngle = Random.Range(0, 360);
        float theta = randomAngle * Mathf.Deg2Rad;

        float x = randomRadius * Mathf.Cos(theta) + player.transform.position.x;
        float y = randomRadius * Mathf.Sin(theta) + player.transform.position.y;

        GameObject o = Instantiate(enemy_prefab);
        o.transform.position = new Vector3(x, y, 0.0f);
        o.GetComponent<Health>().SetMaxHealth(base_enemy_health);
        o.GetComponent<Enemy>().damage = enemy_damage;
        o.GetComponent<TargetMovement>().speed = enemy_speed;

        spawn_count_down = enemy_spawn_rate;
    }
}
