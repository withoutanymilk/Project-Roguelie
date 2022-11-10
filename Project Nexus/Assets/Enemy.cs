using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    [Header("Attack")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    private float canAttack;

    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public Image healthImage;
    public GameObject deathEffect;


    private Transform target;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy Health: " + health);

        if(health <= 0)
        {
            Die();
            FindObjectOfType<AudioManager>().Play("Death");
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    void Update()
    {

        healthImage.fillAmount = health / maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;

        }
    }
}
