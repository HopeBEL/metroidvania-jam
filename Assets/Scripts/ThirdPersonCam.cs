using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    //public Transform playerObj;
    public Rigidbody playerRb;

    public float rotationSpeed;
    Vector3 inputDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation de l'orientation du joueur
        orientation.forward = (player.position - new Vector3(transform.position.x, player.position.y, transform.position.z)).normalized;
    
        // Rotation de l'objet joueur en lui-même
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
   
        // Si on a bien un input
        if (inputDirection != Vector3.zero) {
            // Interpole à la vitesse du 3e argument, le vecteur 1 vers le vecteur 2
            player.forward = Vector3.Slerp(player.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
