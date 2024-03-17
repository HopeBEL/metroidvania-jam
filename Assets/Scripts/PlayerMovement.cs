using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRb;
    float horizontalInput, verticalInput;
    public float speed = 500f;
    public Transform orientation;
    public Vector3 inputDirection;
    public float jumpForce = 5f;
    public bool isOnGround = true;
    public Transform cam;
    Vector3 camRotation;

    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;
    public float dashForce = 10f;
    public float lastDashTime = 0f;
    public float dashCooldown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Movement on X and Z axis
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        playerRb.AddForce(inputDirection.normalized * Time.deltaTime * speed, ForceMode.Force);

        // Jump is possible only if we're on the ground
        if (Input.GetButton("Jump") && isOnGround) {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown) {
            Debug.Log("Dash");
            playerRb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            lastDashTime = Time.time;
        }

        // Orientation following the mouse movements, source : https://stackoverflow.com/questions/66248977/camera-follow-player-when-moving-mouse-with-unity-3d
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * xMouse);
        camRotation.x -= yMouse * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);
        cam.localEulerAngles = camRotation;

        if (transform.position.y <= -25f) {
                Debug.Log("Game Over !");
                SceneManager.LoadScene(2);
            }
    }

    // If we collide with the ground, then we can jump again
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
        }
    }
}
