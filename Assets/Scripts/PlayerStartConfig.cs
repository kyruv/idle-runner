using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAwakeConfig : MonoBehaviour
{
    void Start()
    {
        System.Action<GameObject> player_damage = (obj) =>
            {
                Animator animation = obj.GetComponent<Animator>();
                animation.SetBool("damaged", true);
                Utility.instance.WithDelay(.5f, () =>
                {
                    animation.SetBool("damaged", false);
                });
            };
        System.Action<GameObject> player_death = (obj) =>
                {
                    Animator animation = obj.GetComponent<Animator>();
                    animation.SetBool("death", true);
                    Utility.instance.WithDelay(.5f, () =>
                    {
                        SceneManager.LoadScene("Game");
                    });
                };

        Health h = GetComponent<Health>();
        h.SetColor(Color.green);
        h.SetDamageCallback(player_damage);
        h.SetDeathCallback(player_death);
    }
}
