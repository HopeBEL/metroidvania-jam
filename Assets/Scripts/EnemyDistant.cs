using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistant : Enemy
{
    [SerializeField] bool isInRange = false;
    [SerializeField] bool canAttack = true;
    public GameObject magicSpellPrefab;
    GameObject magicSpellClone;
    public float spellSpeed = 5f;
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
        if (isInRange && canAttack)
            Attack();
    }

    public override void Move() {
        direction = playerObject.transform.position - transform.position;
        if (isTriggered && !isInRange) {
            enemyRb.AddForce(direction.normalized * speed); 

            if (direction.magnitude < 10f) 
                isInRange = true;
        }
        else if (direction.magnitude >= 10f) 
            isInRange = false;
    }

    public override void Attack() {
        // Decrease player's life by 1
        Debug.Log("I hit the player");
        magicSpellClone = Instantiate(magicSpellPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity, this.transform);
        canAttack = false; 
        magicSpellClone.GetComponent<Rigidbody>().AddForce(direction.normalized * spellSpeed, ForceMode.Impulse);
        StartCoroutine(DistantAttack());           
    }

    public override void Damaged() {
        health -= 1;
        if (health <= 0) Destroy(gameObject);
    }

    // If the melee enemy is in range of the player, he strikes an attack
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Collision with player");
        }
        else if (other.gameObject.CompareTag("AbilityFeathers")) {
            health -= 3;
        }
        else if (other.gameObject.CompareTag("AbilityShield")) {
            health -= 1;
        }
        if (health <= 0) {
            Instantiate(healObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }        

    }

    IEnumerator DistantAttack() {
        yield return new WaitForSeconds(3f);
        canAttack = true;
        Destroy(magicSpellClone);
    }
}
