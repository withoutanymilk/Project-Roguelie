using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float ArrowVelocity;
    
    public float ArrowDamage;

    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Attacked");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(ArrowDamage);
            FindObjectOfType<AudioManager>().Play("TakeDamage");
        }
        Destroy(gameObject);
    }
}
