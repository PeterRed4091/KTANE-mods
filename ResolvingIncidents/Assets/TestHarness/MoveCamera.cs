﻿using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    //
    // VARIABLES
    //

    public float turnSpeed = 4.0f;      // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;       // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;      // Speed of the camera going back and forth

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isPanning;     // Is the camera being panned?
    private bool isRotating;    // Is the camera being rotated?
    private bool isZooming;     // Is the camera zooming?

    private Transform _camera;

    void Start()
    {
        _camera = FindObjectOfType<Camera>().transform;
    }

    //
    // UPDATE
    //

    void Update()
    {
        // Get the left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(1)) isRotating = false;
        if (!Input.GetMouseButton(2)) isPanning = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            _camera.RotateAround(_camera.position, _camera.right, -pos.y * turnSpeed);
            _camera.RotateAround(_camera.position, Vector3.up, pos.x * turnSpeed);
            mouseOrigin = Input.mousePosition;
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * -panSpeed, pos.y * -panSpeed, 0);
            _camera.Translate(move, Space.Self);
            mouseOrigin = Input.mousePosition;
        }

        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            Vector3 move = mouseWheel * zoomSpeed * _camera.forward;
            _camera.Translate(move, Space.World);
        }
    }
}