using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemy_prefab;

    [Header("Runner Config")]
    [SerializeReference] private float enemy_spawn_rate = 2f;
    [SerializeReference] private float enemy_spawn_rate_increase = .9f;
    [SerializeReference] private float time_till_increase = 2.5f;


    private float spawn_count_down = 0;

    void Start()
    {
        RepeatedEvery(time_till_increase);
    }

    private IEnumerator RepeatedEvery(float delayInSeconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        enemy_spawn_rate *= enemy_spawn_rate_increase;
        RepeatedEvery(delayInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_count_down <= 0)
        {
            SpawnEnemy();
        }
        spawn_count_down -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        float randomRadius = Random.Range(12.5f, 15);
        float randomAngle = Random.Range(0, 360);
        float theta = randomAngle * Mathf.Deg2Rad;

        float x = randomRadius * Mathf.Cos(theta) + 12.5f;
        float y = randomRadius * Mathf.Sin(theta) + 12.5f;

        GameObject o = Instantiate(enemy_prefab);
        o.transform.position = new Vector3(x, y, 0.0f);

        spawn_count_down = enemy_spawn_rate;
    }
}
