using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
internal class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private float _scrollSpeed = 30f;
    [SerializeField] private Transform _cameraZone;

    private float _cameraFieldOfViewMinimum = 30f;
    private float _cameraFieldOfViewMaximum = 90f;

    private void LateUpdate()
    {
        const float _half = 0.5f;
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
            Mathf.Clamp(newPosition.x, _cameraZone.position.x - _cameraZone.localScale.x * _half, _cameraZone.position.x + _cameraZone.localScale.x * _half),
            newPosition.y,
            Mathf.Clamp(newPosition.z, _cameraZone.position.z - _cameraZone.localScale.z * _half, _cameraZone.position.z + _cameraZone.localScale.z * _half));

        transform.position = correctedNewPosition;
        transform.Rotate(Vector3.up, rotation * _rotationSpeed * Time.deltaTime);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - _scrollSpeed * scroll, _cameraFieldOfViewMinimum, _cameraFieldOfViewMaximum);
    }
}
