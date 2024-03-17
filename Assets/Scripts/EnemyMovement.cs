using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool isTriggered = false;
    public GameObject playerObject;
    public Rigidbody enemyRb;
    Vector3 direction;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered) {
            direction = playerObject.transform.position - transform.position;

            enemyRb.AddForce(direction.normalized * speed); 
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isTriggered = true;
        }
    }
}
