using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    Camera cam;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float rotationX;
    float rotationY;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        myInput();

        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }
    void myInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        rotationY += mouseX * sensitivityX * multiplier;
        rotationX -= mouseY * sensitivityY * multiplier;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }
}

