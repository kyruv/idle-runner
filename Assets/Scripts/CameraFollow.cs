using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Constraints")]
    [SerializeField] private float smooth_speed = 5.0f;
    [SerializeField] private float start_follow_offset = .4f;
    [SerializeField] private float stop_follow_offset = .2f;

    private bool cam_following = false;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector2 cam_delta;
            if (cam_following)
            {
                cam_delta = GetCameraDelta(target.position, stop_follow_offset);
            }
            else
            {
                cam_delta = GetCameraDelta(target.position, start_follow_offset);
                cam_following = true;
            }

            // Debug.Log(cam_delta);

            if (cam_delta != Vector2.zero)
            {
                Vector3 desiredPosition = target.position + threeD(cam_delta);
                desiredPosition.z = transform.position.z;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smooth_speed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
            else
            {
                cam_following = false;
            }
        }
    }

    Vector3 threeD(Vector2 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }

    Vector2 twoD(Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    Vector2 GetCameraDelta(Vector2 playerPosition, float dist)
    {
        Vector2 free_walk_distance = new Vector2((1 - dist) * Camera.main.orthographicSize * Screen.width / Screen.height, (1 - dist) * Camera.main.orthographicSize);

        Vector2 world_camera_min = twoD(transform.position) - free_walk_distance;
        Vector2 world_camera_max = twoD(transform.position) + free_walk_distance;

        float dx = 0;
        float dy = 0;

        // Debug.Log("free_walk_distance " + free_walk_distance);
        // Debug.Log("world_camera_min " + world_camera_min);
        // Debug.Log("world_camera_max " + world_camera_max);
        // Debug.Log("player_postion " + playerPosition);
        // Debug.Log("");

        if (playerPosition.x < world_camera_min.x)
        {
            dx = playerPosition.x - world_camera_min.x;
        }
        else if (playerPosition.x > world_camera_max.x)
        {
            dx = playerPosition.x - world_camera_max.x;
        }

        if (playerPosition.y < world_camera_min.y)
        {
            dy = playerPosition.y - world_camera_min.y;
        }
        else if (playerPosition.y > world_camera_max.y)
        {
            dy = playerPosition.y - world_camera_max.y;
        }

        return new Vector2(dx, dy);
    }
}
