using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemy_prefab;

    [Header("Runner Config")]
    [SerializeReference] private int level = 1;
    [SerializeReference] private float enemy_spawn_rate = 2f;
    [SerializeReference] private float enemy_spawn_rate_increase = .9f;
    [SerializeReference] private float time_till_increase = 2.5f;
    [SerializeReference] private float base_enemy_health = 2.5f;
    [SerializeReference] private float enemy_damage = 2.5f;

    private float spawn_count_down = 0;
    private float difficulty_count_down = 0;

    void Start()
    {
        difficulty_count_down = time_till_increase;
        SetLevel(level);
    }

    void SetLevel(int level)
    {
        this.level = level;

        enemy_spawn_rate = .05f + 1f * ((999 - level) / 999);
        enemy_spawn_rate_increase = .9f;
        time_till_increase = 2.5f - 2 * ((999 - level) / 999);
        base_enemy_health = 1 + level / 5f;
        enemy_damage = .25f + level / 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_count_down <= 0)
        {
            SpawnEnemy();
        }
        spawn_count_down -= Time.deltaTime;

        if (difficulty_count_down <= 0)
        {
            enemy_spawn_rate *= enemy_spawn_rate_increase;
            base_enemy_health *= (1 / enemy_spawn_rate_increase);
            difficulty_count_down = time_till_increase;
        }
    }

    void SpawnEnemy()
    {
        float randomRadius = Random.Range(50f, 60f);
        float randomAngle = Random.Range(0, 360);
        float theta = randomAngle * Mathf.Deg2Rad;

        float x = randomRadius * Mathf.Cos(theta) + 40f;
        float y = randomRadius * Mathf.Sin(theta);

        GameObject o = Instantiate(enemy_prefab);
        o.transform.position = new Vector3(x, y, 0.0f);
        o.GetComponent<Health>().SetMaxHealth(base_enemy_health);
        o.GetComponent<Enemy>().damage = enemy_damage;

        spawn_count_down = enemy_spawn_rate;
    }
}
