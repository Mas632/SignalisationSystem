using UnityEngine;

internal class CameraMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private float _scrollSpeed = 30f;
    [SerializeField] private Transform _cameraZone;

    private float _cameraFieldOfViewMinimum = 30f;
    private float _cameraFieldOfViewMaximum = 90f;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        const float Half = 0.5f;
        const string VerticalAxisName = "Vertical";
        const string HorizontalAxisName = "Horizontal";
        const string RotationAxisName = "Rotation";
        const string ScrollAxisName = "Scroll";

        float forward = Input.GetAxis(VerticalAxisName);
        float lateral = Input.GetAxis(HorizontalAxisName);
        float rotation = Input.GetAxis(RotationAxisName);
        float scroll = Input.GetAxis(ScrollAxisName);

        Vector3 newPosition = transform.position + (new Vector3(transform.forward.x, 0, transform.forward.z) * forward + new Vector3(transform.right.x, 0, transform.right.z) * lateral) * Time.deltaTime * _movementSpeed;
        Vector3 correctedNewPosition = new Vector3(
            Mathf.Clamp(newPosition.x, _cameraZone.position.x - _cameraZone.localScale.x * Half, _cameraZone.position.x + _cameraZone.localScale.x * Half),
            newPosition.y,
            Mathf.Clamp(newPosition.z, _cameraZone.position.z - _cameraZone.localScale.z * Half, _cameraZone.position.z + _cameraZone.localScale.z * Half));

        transform.position = correctedNewPosition;
        transform.Rotate(Vector3.up, rotation * _rotationSpeed * Time.deltaTime);
        _camera.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - _scrollSpeed * scroll, _cameraFieldOfViewMinimum, _cameraFieldOfViewMaximum);
    }
}
