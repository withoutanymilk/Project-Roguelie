using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem dust;

    public float moveSpeed;

    private float activeMoveSpeed;

    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;

    private float dashCoolCounter;

    public Rigidbody2D rb;

    public Animator anim;

    public GameObject SwordSprite;

    public GameObject SwordObject;

    public Animator swordAnim;

    private Vector2 moveDirection;
   
    private Vector2 lastMoveDirection;

    [SerializeField] Transform hand;
   
    [SerializeField] GameObject arrow;

    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        ProcessInputs();

        Animate();

        RotateHand();

        Dash();
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void RotateHand()
    {
        float angle = Utility.AngleTowardsMouse(hand.position);
        hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        
        //flips the sword sprite
        Vector2 scale = SwordSprite.transform.localScale;

        if (angle >= -90 && angle <= 90) //right
        {
            scale.y = 1f;
            SwordSprite.transform.localScale = scale;
            swordAnim.SetBool("SwingLeft", false);
            swordAnim.SetBool("SwingRight", true);

        }
        else
        {
            scale.y = -1f;
            
            SwordSprite.transform.localScale = scale;
            swordAnim.SetBool("SwingLeft", true);
            swordAnim.SetBool("SwingRight", false);

        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
    }

    void Animate()
    {
        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
        anim.SetFloat("AnimMoveMagnitude", moveDirection.magnitude);
        anim.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        anim.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                CreateDust();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void CreateDust()
    {
        dust.Play();
        FindObjectOfType<AudioManager>().Play("Dash");

    }

}