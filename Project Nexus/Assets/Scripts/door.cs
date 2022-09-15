using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class door : MonoBehaviour
{
    public bool interactable = false;
    public Animator m_Animator;
    public UnityEvent Door_Open;
    public UnityEvent Door_Close;
    public bool DoorOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = this.gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable == true)
        {
            if (/*collider.CompareTag("Player") && */Input.GetKeyDown("e"))
            {
                if (DoorOpened == false)
                {
                    Door_Open.Invoke();
                    m_Animator.SetBool("DoorOpened", true);
                    DoorOpened = true;
                }
                else
                {
                    Door_Close.Invoke();
                    m_Animator.SetBool("DoorOpened", false);
                    DoorOpened = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        interactable = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        interactable = false;
    }
}