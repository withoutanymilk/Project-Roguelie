using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject ArrowPrefab;

    [SerializeField] SpriteRenderer ArrowGFX;

    [SerializeField] Slider BowPowerSlider;

    [SerializeField] Transform Bow;

    [Range(0, 10)]

    [SerializeField] float BowPower;

    [SerializeField] float SwordPower;

    [Range(0, 3)]

    [SerializeField] float MaxBowCharge;

    [SerializeField] float MaxSwordCharge;

    float BowCharge;

    float SwordCharge;

    bool CanFire = true;

    bool CanSwing = true;

    bool BowActive = true;

    bool SwordActive = false;

    public float firerate = 2f;

    //public float swingrate = 2f;

    public float SwordCooldown = 1f;

    private float SwordCdCounter;

    public GameObject BowObject;

    public GameObject SwordObject;

    private float nextTimeToFire = 0f;

    //private float nextTimeToSwing = 0f;

    public Animator swordAnim;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
        SwordCdCounter = SwordCooldown;
    }
    
    private void Update()
    {
        ChangeWeapon(); //check if the player changes weapons

        if (BowActive == true && SwordActive == false)
        {
            UseBow();
        }

        else if (BowActive == false && SwordActive == true)
        {
            UseSword();
        }
    }

    void UseBow()
    {
        if (Input.GetMouseButton(0) && CanFire)
        {
            ChargeBow();
        }
        else if (Input.GetMouseButtonUp(0) && CanFire && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            FireBow();
            FindObjectOfType<AudioManager>().Play("Arrow Fired");
        }
        else
        {
            if (BowCharge > 0f)
            {
                BowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                BowCharge = 0f;
                CanFire = true;
                FindObjectOfType<AudioManager>().Play("ChargeBow");
            }

            BowPowerSlider.value = BowCharge;
        }
    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;
        
        BowCharge += Time.deltaTime;

        BowPowerSlider.value = BowCharge;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if (BowCharge > MaxBowCharge) BowCharge = MaxBowCharge;

        float ArrowSpeed = BowCharge + BowPower;

        float ArrowDamage = BowCharge * BowPower;

        float angle = Utility.AngleTowardsMouse(Bow.position);

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();

        Arrow.ArrowVelocity = ArrowSpeed;

        Arrow.ArrowDamage = ArrowDamage;

        CanFire = false;

        ArrowGFX.enabled = false;
    }

    void UseSword()
    {
        //Debug.Log(CanSwing);
        if (Input.GetMouseButton(0) && CanSwing)
        {
            SwingSword();
        }
        /*else if (Input.GetMouseButtonUp(0) && CanSwing && Time.time >= nextTimeToSwing)
        {
            nextTimeToSwing = Time.time + 1f / swingrate;
            SwingSword();
        }*/
        else
        {
            if (SwordCdCounter > 0f)
            {
                swordAnim.SetBool("CanSwing", false);
                SwordCdCounter -= 1f * Time.deltaTime;
            }
            else
            {
                //SwordCharge = 0f;
                CanSwing = true;

            }

            //BowPowerSlider.value = SwordCharge;
        }
    }

/*    void ChargeSword()
    {
        SwingSword();
    }*/

    void SwingSword()
    {
        Debug.Log("I am out here");
        Debug.Log(SwordCdCounter);
        if (SwordCdCounter <= 0)
        {
            Debug.Log("I am here");
            swordAnim.SetBool("CanSwing", true);
            SwordCdCounter = SwordCooldown;
            FindObjectOfType<AudioManager>().Play("SwordSwing");
        }
        CanSwing = false;
        //swordAnim.SetBool("CanSwing", false);
    }

    void ChangeWeapon()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("Bow");
            BowActive = true;
            SwordActive = false;
            BowObject.SetActive(true);
            SwordObject.SetActive(false);
        }

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("Sword");
            BowActive = false;
            SwordActive = true;

            BowObject.SetActive(false);
            SwordObject.SetActive(true);
        }
    }
}
