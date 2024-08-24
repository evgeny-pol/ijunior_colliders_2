using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private const int RotateCommandButton = InputConstants.SecondMouseButton;

    [SerializeField] private Vector3 _rotateAround;
    [SerializeField] private float _moveSensitivity = 1f;
    [SerializeField] private float _zoomSensitivity = 1f;

    private Camera _camera;

    private bool IsRotating => Input.GetMouseButton(RotateCommandButton);

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.fieldOfView -= Input.mouseScrollDelta.y * _zoomSensitivity;

        if (IsRotating)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            transform.RotateAround(_rotateAround, Vector3.up, Input.GetAxis(InputConstants.HorizontalMouseAxis) * _moveSensitivity);
            transform.RotateAround(_rotateAround, transform.right, Input.GetAxis(InputConstants.VerticalMouseAxis) * -_moveSensitivity);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
