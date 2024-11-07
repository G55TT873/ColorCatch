using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float distance = 5.0f;
    public float sensitivity = 3.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, -20, 60);

            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            Vector3 offset = rotation * new Vector3(0, 0, -distance);

            transform.position = player.position + offset;
            transform.LookAt(player.position);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
