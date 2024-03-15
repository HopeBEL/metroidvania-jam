using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityManager : MonoBehaviour
{
    [SerializeField] bool isAbilityActive = false;


    [Header("Shield Ability")]
    [SerializeField] bool hasAbilityShield = false;
    public GameObject shieldObject;
    GameObject cloneShield;
    public float shieldCooldown = 12f;
    public float lastShieldTime = -12f;



    [Header("Feathers Ability")]
    [SerializeField] bool hasAbilityFeathers = false;
    public int feathersCount = 10;
    public GameObject featherObject;
    public GameObject[] clonesFeathers;
    public float feathersCooldown = 3f;
    public float lastFeatherTime = -3f;

    [Header("UI")]
    public GameObject shieldUI;
    public GameObject feathersUI;
    public TMP_Text shieldCdText;
    public TMP_Text feathersCdText;




    // Start is called before the first frame update
    void Start()
    {
        clonesFeathers = new GameObject[feathersCount];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        // Shield ability
        UICooldown();
        if (hasAbilityShield && Input.GetKeyDown("e") && !isAbilityActive && Time.time > lastShieldTime + shieldCooldown) {
            Debug.Log("I activated my shield !");
            isAbilityActive = true;
            cloneShield = Instantiate(shieldObject, this.transform);
            lastShieldTime = Time.time;
            StartCoroutine(PowerUpShield());
        }
        // Feather ability
        else if (hasAbilityFeathers && Input.GetKeyDown("f") && !isAbilityActive && Time.time > lastFeatherTime + feathersCooldown) {
            Debug.Log("I'm aiming at you with feathers");
            isAbilityActive = true;
            for (int i = 0; i < feathersCount; i++) {
                clonesFeathers[i] = Instantiate(featherObject, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, this.transform);
            }
            lastFeatherTime = Time.time;
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
            shieldUI.SetActive(true);

        }
        else if (other.gameObject.CompareTag("AbilityFeathers")) {
            Debug.Log("Yayy new ability unlocked ! Feathers");
            // Destroy the object that gave us the ability
            Destroy(other.gameObject);
            // Stays true forever
            hasAbilityFeathers = true;
            feathersUI.SetActive(true);

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
        // 3 seconds duration of the feathers, before we destroy them
        yield return new WaitForSeconds(3f);
        isAbilityActive = false;
        for (int i = 0; i < feathersCount; i++) {
            Destroy(clonesFeathers[i]);
        }
        Debug.Log("I destroyed all the feathers");
    }

    public void UICooldown() {

        if (hasAbilityShield) {
            float cdShield = shieldCooldown - (Time.time - lastShieldTime);
            if (cdShield > 0) shieldCdText.text = ((int)cdShield).ToString();
            else shieldCdText.text = ""; 
        }

        if (hasAbilityFeathers) {
            float cdFeathers = feathersCooldown - (Time.time - lastFeatherTime);
            if (cdFeathers > 0) feathersCdText.text = ((int)cdFeathers).ToString();
            else feathersCdText.text = ""; 
        }
    }
}
