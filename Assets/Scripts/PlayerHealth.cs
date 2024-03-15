using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public GameObject panel;
    public int health = 5;
    public Sprite heartSprite;
    public GameObject[] heartObjectsTab;
    public float offset = 40f;
    // Start is called before the first frame update
    void Start()
    {
        heartObjectsTab= new GameObject[health];

        CreateHeart(health, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateHeart(int n, int startPos) {
        for (int i = 0; i < n; i++) {
            GameObject heartObject = new GameObject("Heart" + i);
            Image img = heartObject.AddComponent<Image>();
            img.sprite = heartSprite;
            RectTransform rect = heartObject.GetComponent<RectTransform>();
            rect.SetParent(panel.transform);
            rect.anchoredPosition = new Vector3(-100 + (startPos + i) * offset, 100, 0);
            rect.sizeDelta = new Vector2(50, 50);

            heartObjectsTab[i] = heartObject;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("Aie.. je perds une vie");
            health--;
            if (health > 0)
                Destroy(heartObjectsTab[health]);

            if (health <= 0) {
                Debug.Log("Game Over !");
                SceneManager.LoadScene(2);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Heal")) {
            Destroy(other.gameObject);
            CreateHeart(1, health);
            health++;
        }
    }
}
