using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    public float movementSpeed = 6f;
    public float movementMultiplier = 15f;
    [SerializeField] float airMovementMultiplier = 0.4f;


    [Header("Keybinds")]
    [SerializeField] KeyCode jump = KeyCode.Space;
    public float jumpForce = 400;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    float playerHight = 2f;


    float hMovement;
    float vMovement;

    bool isGrounded;

    Vector3 direction;

    Rigidbody rbody;

    void Start() {
        rbody = GetComponent<Rigidbody>();
        rbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        myInput();
        controlDrag();
        PlayerMoving();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHight / 2 + 0.1f);
        if (Input.GetKeyDown(jump) && isGrounded) {
            Jump();
        }
        print(isGrounded);
    }

    void myInput() {
        hMovement = Input.GetAxisRaw("Horizontal");
        vMovement = Input.GetAxisRaw("Vertical");

        direction = transform.forward * vMovement + transform.right * hMovement;
    }

    private void FixedUpdate(){
        
    }

    void controlDrag() {
        if (isGrounded) {
            rbody.drag = groundDrag;
        }
        else {
            rbody.drag = airDrag;
        }
    }

    void PlayerMoving() {
        if (isGrounded) {
            rbody.AddForce(direction.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded) {
            rbody.AddForce(direction.normalized * movementSpeed * movementMultiplier * airMovementMultiplier, ForceMode.Acceleration);

        }
    }

    void Jump() {
        rbody.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
    }
}
