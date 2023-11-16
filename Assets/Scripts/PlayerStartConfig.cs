using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwakeConfig : MonoBehaviour
{
    void Start()
    {
        System.Action<GameObject> player_damage = (obj) =>
            {
                Animator animation = obj.GetComponent<Animator>();
                animation.SetBool("damaged", true);
                StartCoroutine(Utility.instance.WithDelay(.5f, () =>
                {
                    animation.SetBool("damaged", false);
                }));
            };
        System.Action<GameObject> player_death = (obj) =>
                {
                    Animator animation = obj.GetComponent<Animator>();
                    animation.SetBool("death", true);
                    StartCoroutine(Utility.instance.WithDelay(.25f, () =>
                    {
                        Debug.Log("HERE DESTROYING PLAYER");
                        Destroy(obj);
                    }));
                };

        Health h = GetComponent<Health>();
        h.SetColor(Color.green);
        h.SetDamageCallback(player_damage);
        h.SetDeathCallback(player_death);
    }
}
