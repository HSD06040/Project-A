using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraBoom;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private CinemachineVirtualCamera vCam;

    [Header("Enlarg")]
    [SerializeField] private float minSize = 50;
    [SerializeField] private float maxSize = 70;
    [SerializeField] private float zoomSpeed = 2;
    private float curZoom;

    private Vector2 lookDelta;
    private float xRot;
    private float yRot;

    private void OnEnable()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        curZoom = vCam.m_Lens.FieldOfView;
    }

    private void OnDisable()
    {
        //Cursor.lockState = CursorLockMode.None;
    }

    private void LateUpdate()
    {
        xRot -= lookDelta.y * mouseSensitivity * Time.deltaTime;
        yRot += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -80, 80);

        cameraBoom.rotation = Quaternion.Euler(xRot, yRot, 0);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }

    private void OnEnlarge(InputValue value)
    {
        float scroll = value.Get<Vector2>().y;

        curZoom -= scroll * zoomSpeed * Time.deltaTime;
        curZoom = Mathf.Clamp(curZoom, minSize, maxSize);

        vCam.m_Lens.FieldOfView = curZoom;
    }
}
