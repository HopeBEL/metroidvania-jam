using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] bool isAbilityActive = false;


    [Header("Shield Ability")]
    [SerializeField] bool hasAbilityShield = false;
    public GameObject shieldObject;
    GameObject cloneShield;



    [Header("Feathers Ability")]
    [SerializeField] bool hasAbilityFeathers = false;
    public int feathersCount = 10;
    public GameObject featherObject;
    public GameObject[] clonesFeathers;
    // Start is called before the first frame update
    void Start()
    {
        clonesFeathers = new GameObject[feathersCount];
    }

    // Update is called once per frame
    void Update()
    {
        // Shield ability
        if (hasAbilityShield && Input.GetKeyDown("p") && !isAbilityActive) {
            Debug.Log("I activated my shield !");
            isAbilityActive = true;
            cloneShield = Instantiate(shieldObject, this.transform);
            StartCoroutine(PowerUpShield());
        }
        // Feather ability
        else if (hasAbilityFeathers && Input.GetKeyDown("f") && !isAbilityActive) {
            Debug.Log("I'm aiming at you with feathers");
            isAbilityActive = true;
            for (int i = 0; i < feathersCount; i++) {
                clonesFeathers[i] = Instantiate(featherObject, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, this.transform);
            }
            StartCoroutine(PowerUpFeathers());
        }

        // So they always point towards where the player is looking
        foreach(GameObject feather in clonesFeathers) {
            if (feather != null) {
                feather.transform.rotation = transform.rotation;

                // Left-click to shoot them
                if (Input.GetMouseButtonDown(0)) {
                    for (int i = 0; i < feathersCount; i++) {
                        clonesFeathers[i].GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
                    }
                }
            }            
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("AbilityShield")) {
            Debug.Log("Yayy new ability unlocked ! Shield");
            // Destroy the object that gave us the ability
            Destroy(other.gameObject);
            // Stays true forever
            hasAbilityShield = true;
        }
        else if (other.gameObject.CompareTag("AbilityFeathers")) {
            Debug.Log("Yayy new ability unlocked ! Feathers");
            // Destroy the object that gave us the ability
            Destroy(other.gameObject);
            // Stays true forever
            hasAbilityFeathers = true;
        }
    }

    IEnumerator PowerUpShield() {
        // 5 seconds duration of the shield, before we destroy it
        yield return new WaitForSeconds(5f);
        isAbilityActive = false;
        Destroy(cloneShield);
        Debug.Log("Supposed to disappear now");
    }

    IEnumerator PowerUpFeathers() {
        // 7 seconds duration of the feathers, before we destroy them
        yield return new WaitForSeconds(7f);
        isAbilityActive = false;
        for (int i = 0; i < feathersCount; i++) {
            Destroy(clonesFeathers[i]);
        }
        Debug.Log("I destroyed all the feathers");
    }
}
