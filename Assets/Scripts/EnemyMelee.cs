using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float repulsiveForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Move() {
        if (isTriggered) {
            direction = playerObject.transform.position - transform.position;

            enemyRb.AddForce(direction.normalized * speed); 
        }
    }

    public override void Attack() {
        // Decrease player's life by 1
        Debug.Log("I hit the player");
    }

    public override void Damaged() {
        health -= 1;
        if (health <= 0) Destroy(gameObject);
    }

    // If the melee enemy is in range of the player, he strikes an attack
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Collision with player");
            enemyRb.AddForce(- direction.normalized * repulsiveForce, ForceMode.Impulse); 
            Attack();
        }
        else if (other.gameObject.CompareTag("AbilityFeathers")) {
            health -= 5;
        }
        else if (other.gameObject.CompareTag("AbilityShield")) {
            Debug.Log("Ici collision avec le shield");
            health -= 1;
        }
        if (health <= 0) {
            Instantiate(healObject);
            Destroy(gameObject);
        }          

    }
}
