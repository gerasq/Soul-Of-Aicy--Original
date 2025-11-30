using UnityEngine;

public class SimpleCameraOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 6f;
    public float mouseSensitivity = 3f;
    public float yMin = -30f;
    public float yMax = 60f;

    private float yaw;
    private float pitch;

    void Start()
    {
        if (target == null) return;

        Vector3 dir = (transform.position - target.position).normalized;
        yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        pitch = Mathf.Asin(dir.y) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Pelės judesys
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, yMin, yMax);

        // Nauja kameros pozicija
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        Vector3 targetPos = target.position + Vector3.up * 1.5f;

        transform.position = targetPos + offset;
        transform.LookAt(targetPos);
    }
}
