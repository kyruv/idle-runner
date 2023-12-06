using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bullet_prefab;

    [Header("Gun Details")]
    [SerializeField] private int clipSize;
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private float damage;
    [SerializeField] private int shotgun_num;
    [SerializeField] private float shotgun_angle;

    private AmmoDisplay ammoDisplay;
    private int numBullets;
    private float fireTimeout = 0f;

    void Start()
    {
        ammoDisplay = GameObject.Find("BulletDisplay").GetComponent<AmmoDisplay>();
        ammoDisplay.SetAmmo(clipSize);
        numBullets = clipSize;
    }

    public void UpdateGunStats()
    {
        shotgun_num = GetComponent<PlayerStats>().shotgun_spread;
        damage = GetComponent<PlayerStats>().damage;
        Debug.Log("Shotgun: " + shotgun_num);
    }

    public void UpdateDexterityStats()
    {
        fireRate = GetComponent<PlayerStats>().fire_rate;
        damage = GetComponent<PlayerStats>().damage;
        reloadTime = GetComponent<PlayerStats>().reload_time;
        Debug.Log("Shotgun: " + shotgun_num);
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimeout > 0f)
        {
            fireTimeout -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            float zCoordinate = 10f;
            Vector3 worldClickPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zCoordinate));
            Vector3 diff = worldClickPosition - transform.position;
            Vector2 diff2d = new Vector2(diff.x, diff.y);
            diff2d.Normalize();

            int num_to_shoot = shotgun_num;
            float da = shotgun_angle / num_to_shoot;
            for (int i = 0; i < num_to_shoot; i++)
            {
                Vector2 v = RotateVector(diff2d, (i - num_to_shoot / 2) * da);
                GameObject o = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
                Bullet b = o.GetComponent<Bullet>();
                b.SetDir(v);
                b.damage = damage;
            }
            numBullets -= 1;

            ammoDisplay.SetAmmo(numBullets);
            if (numBullets == 0)
            {
                numBullets = clipSize;
                fireTimeout = reloadTime;
                Utility.instance.WithDelay(reloadTime, () =>
                {
                    ammoDisplay.SetAmmo(clipSize);
                });
            }
            else
            {
                fireTimeout = fireRate;
            }
        }
    }

    Vector2 RotateVector(Vector2 vector, float degrees)
    {
        // Convert degrees to radians
        float radians = Mathf.Deg2Rad * degrees;

        // Calculate the rotated vector using trigonometric functions
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;

        Vector2 v = new Vector2(x, y);
        v.Normalize();

        return v;
    }
}
