using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName; // Incoming x and y input values
    [SerializeField] private float mouseSensitivity; // Mouse sensitivity

    // Start is called before the first frame
    private void Start()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    // Lock the cursor to the center of the screen
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Handles the rotation of the camera
    private void CameraRotation()
    {
        // Obtain x and y values for mouse
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        // Rotates the camera
        transform.Rotate(-transform.right * mouseY);
    }
}
