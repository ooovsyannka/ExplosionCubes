using UnityEngine;

[RequireComponent(typeof(Camera))]

public class Looking : MonoBehaviour
{
    [SerializeField] private float _speed;

    private readonly string MouseX = "Mouse X";
    private readonly string MouseY = "Mouse Y";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Zoom();

        float rotateX = _speed * -Input.GetAxis(MouseY) * Time.deltaTime;
        float rotateY = _speed * Input.GetAxis(MouseX) * Time.deltaTime;

        transform.Rotate(0, rotateY, 0, Space.World);
        transform.Rotate(rotateX, 0, 0);
    }

    private void Zoom()
    {
        Camera camera = transform.GetComponent<Camera>();
        float defaultFieldOfView = 60;
        float zoomMultiple = 3;

        if (Input.GetMouseButton(1))
        {
            camera.fieldOfView = defaultFieldOfView / zoomMultiple;
        }
        else
        {
            camera.fieldOfView = defaultFieldOfView;
        }
    }
}
