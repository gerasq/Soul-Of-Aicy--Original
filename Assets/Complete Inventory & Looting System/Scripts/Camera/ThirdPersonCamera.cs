using UnityEngine;

namespace DuelatorGames
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public float lookSpeed = 2f;
        Vector2 rotation = Vector2.zero;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotation.y = transform.eulerAngles.y;
        }
        void Update()
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -60, 60);
            transform.parent.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        }
    }
}
