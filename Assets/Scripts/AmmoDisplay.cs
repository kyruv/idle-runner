using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{

    Transform[] bulletMarkers;

    void Awake()
    {
        bulletMarkers = GetComponentsInChildren<Transform>();
    }

    public void SetAmmo(int ammo)
    {
        for (int i = 1; i < bulletMarkers.Length; i++)
        {
            if (i - 1 < ammo)
            {
                bulletMarkers[i].gameObject.SetActive(true);
            }
            else
            {
                bulletMarkers[i].gameObject.SetActive(false);
            }
        }
    }
}
