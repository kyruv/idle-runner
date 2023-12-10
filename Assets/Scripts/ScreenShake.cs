using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Intensity of the screen shake
    public float shakeIntensity = 0.4f;

    // Duration of the screen shake
    public float shakeDuration = 0.3f;

    // Reference to the main camera
    private Camera mainCamera;

    // Initial position of the camera
    private Vector3 initialPosition;

    private CameraFollow cf;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
        cf = GetComponent<CameraFollow>();

        // Store the initial position of the camera
        initialPosition = mainCamera.transform.position;
    }

    // Call this method to trigger the screen shake
    public void ShakeScreen()
    {
        // Start a coroutine to handle the screen shake
        StartCoroutine(Shake());
    }

    // Coroutine to handle the screen shake
    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        cf.enabled = false;
        initialPosition = mainCamera.transform.position;

        // Shake the camera for the specified duration
        while (elapsedTime < shakeDuration)
        {
            // Generate a random offset within a unit circle
            Vector2 shakeOffset = Random.insideUnitCircle * shakeIntensity;

            // Apply the offset to the camera position
            mainCamera.transform.position = new Vector3(initialPosition.x + shakeOffset.x, initialPosition.y + shakeOffset.y, initialPosition.z);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera position after the shake is complete
        mainCamera.transform.position = initialPosition;
        cf.enabled = true;
    }
}
