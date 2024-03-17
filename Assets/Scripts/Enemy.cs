using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isTriggered = false;
    public GameObject playerObject;
    public float speed = 5f;
    public Rigidbody enemyRb;
    protected Vector3 direction;
    public int health = 5;
    public GameObject healObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move() {

    }

    public virtual void Attack() {

    }

    public virtual void Damaged() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isTriggered = true;
        }
    }
}
