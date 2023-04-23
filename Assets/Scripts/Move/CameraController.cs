using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float mouseSensitivity = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float upDown = 0.0f;

        if (Input.GetKey(KeyCode.Space)) // Move camera up when space is pressed
        {
            upDown += 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift)) // Move camera down when left shift is pressed
        {
            upDown -= 1.0f;
        }

        transform.position += transform.forward * vertical * moveSpeed * Time.deltaTime;
        transform.position += transform.right * horizontal * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * upDown * moveSpeed * Time.deltaTime; // Move camera up and down

        yaw += mouseSensitivity * Input.GetAxis("Mouse X");
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
