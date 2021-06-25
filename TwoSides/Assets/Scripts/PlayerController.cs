using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxJumps = 2;
    public float moveSpeed = 6f;
    public float jumpForce = 5f;
    public float lookSensitivity = 2f;

    Camera cam;
    Rigidbody rb;
    int remainingJumps;

    

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Physics.gravity *= 2f;

        Cursor.lockState = CursorLockMode.Locked;
        remainingJumps = maxJumps;
    }

    // Update is called once per frame
    void Update() {
        // Movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
        if (direction.magnitude >= 0.1f) {
            rb.MovePosition(transform.position + (transform.forward * moveVertical + transform.right * moveHorizontal) * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0) {
            Vector3 vel = rb.velocity;
            vel.y = 0f;
            rb.velocity = vel;
            rb.AddForce(Vector3.up * jumpForce);
            remainingJumps--;
        }

        // Camera
        float lookHorizontal = Input.GetAxisRaw("Mouse X");
        float lookVertical = Input.GetAxisRaw("Mouse Y");
        transform.Rotate(transform.up * lookHorizontal * lookSensitivity);
        cam.transform.RotateAround(transform.position, transform.right, -lookVertical * lookSensitivity);
    }

    private void OnCollisionEnter(Collision collision) {
        remainingJumps = maxJumps;
    }

}
