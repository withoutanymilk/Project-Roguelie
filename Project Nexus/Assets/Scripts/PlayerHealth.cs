using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 0f;

    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private Slider healthSlider;

    public GameObject gameOverUI;

    public bool gameOver;


    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    public void UpdateHealth(float mod)
    {
        health += mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }else if (health <= 0f)
        {
            health = 0f;
            healthSlider.value = health;
            Destroy(gameObject);
            GameOver();
        }
    }

    void GameOver()
    {

        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 1f;
        //WeaponHolder.SetActive(false);
    }
    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        healthSlider.value = Mathf.Lerp(healthSlider.value, health, t);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D " + collision.gameObject.name + " " + this.name);

        if (collision.gameObject.CompareTag("PowerupHealth") && (health < 100))      //Triggers if the player collides with object with tag "PowerupHealth", they regain health.
        {
            Destroy(collision.gameObject);
            UpdateHealth(+15);
        }

    }
}
