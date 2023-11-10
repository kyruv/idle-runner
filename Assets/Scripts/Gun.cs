using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bullet_prefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            float zCoordinate = 10f;
            Vector3 worldClickPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zCoordinate));
            Vector3 diff = worldClickPosition - transform.position;
            Vector2 diff2d = new Vector2(diff.x, diff.y);
            diff2d.Normalize();

            GameObject o = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
            o.GetComponent<Bullet>().SetDir(diff2d);
        }
    }
}
