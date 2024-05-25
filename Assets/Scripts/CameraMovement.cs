using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f; // Speed at which the camera moves forward

    void Update()
    {
        // Move the camera forward continuously
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
