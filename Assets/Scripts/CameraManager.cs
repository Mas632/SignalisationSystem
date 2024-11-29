using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CameraManager : MonoBehaviour
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
        _camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        const float _half = 0.5f;

        float forward = Input.GetAxis("Vertical");
        float lateral = Input.GetAxis("Horizontal");
        float rotation = Input.GetAxis("Rotation");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 newPosition = transform.position + (new Vector3(transform.forward.x, 0, transform.forward.z) * forward + new Vector3(transform.right.x, 0, transform.right.z) * lateral) * Time.deltaTime * _movementSpeed;
        Vector3 correctedNewPosition = new Vector3(
            Mathf.Clamp(newPosition.x, _cameraZone.position.x - _cameraZone.localScale.x * _half, _cameraZone.position.x + _cameraZone.localScale.x * _half),
            newPosition.y,
            Mathf.Clamp(newPosition.z, _cameraZone.position.z - _cameraZone.localScale.z * _half, _cameraZone.position.z + _cameraZone.localScale.z * _half));
        //transform.position += new Vector3(transform.forward.x, 0, transform.forward.z) * forward * Time.deltaTime * _movementSpeed;
        //transform.position += new Vector3(transform.right.x, 0, transform.right.z) * lateral * Time.deltaTime * _movementSpeed;
        transform.position = correctedNewPosition;
        transform.Rotate(Vector3.up, rotation * _rotationSpeed * Time.deltaTime);
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView + _scrollSpeed * scroll, _cameraFieldOfViewMinimum, _cameraFieldOfViewMaximum);
        
        //_camera.fieldOfView += -scroll * _scrollSpeed;
        //Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
        //transform.RotateAround(transform.position, Vector3.up, rotation * _rotationSpeed * Time.deltaTime);
    }
}
