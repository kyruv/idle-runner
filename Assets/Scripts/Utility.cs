using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour
{
    public static Utility instance;
    void Awake()
    {
        instance = this;
    }

    public IEnumerator WithDelay(float delay, System.Action thing)
    {
        yield return new WaitForSeconds(delay);
        thing?.Invoke();
    }
}