using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class EnemyStartConfig : MonoBehaviour
{
    void Start()
    {
        System.Action<GameObject> enemy_damage = (obj) =>
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Color tmp = sr.color;
                sr.color = Color.white;
                StartCoroutine(Utility.instance.WithDelay(.1f, () =>
                {
                    sr.color = tmp;
                }));
            };
        System.Action<GameObject> enemy_death = (obj) =>
            {
                Destroy(obj);
            };

        Health h = GetComponent<Health>();
        h.SetColor(Color.yellow);
        h.SetDamageCallback(enemy_damage);
        h.SetDeathCallback(enemy_death);
    }
}
