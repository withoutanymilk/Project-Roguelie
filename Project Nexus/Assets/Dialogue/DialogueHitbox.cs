using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHitbox : Interactable
{
    public UnityEvent DisplayText;
    public UnityEvent NextSentence;
    public UnityEvent CloseText;
    public UnityEvent InteractIconOn;
    public UnityEvent InteractIconOff;
    public Animator animator;
    public GameObject DialogueBox;
    private bool collected = false;

    void Awake()
    {
        animator = DialogueBox.GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (animator.GetBool("IsOpen") == false)
        {
            DisplayText.Invoke();
            //collected = true;
        }
        else if (animator.GetBool("IsOpen") == true)
        {
            NextSentence.Invoke();
            //collected = true;
        }
    }


    protected void OnTriggerStay2D(Collider2D obj)
    {
        if (Input.GetKeyDown("e"))
        {
            Interact();
        }
    }



    /*protected void OnTriggerEnter2D(Collider2D obj)
    {
        collected = false;
    }

    protected void OnTriggerStay2D(Collider2D obj)
    {
        
        if (Input.GetKeyDown("e") && collected == true)
        {

            NextSentence.Invoke();
        }
        if (Input.GetKeyDown("e") && collected == false)
        {

            collected = true;
            DisplayText.Invoke();
        } 
        InteractIconOn.Invoke();

        if (animator.GetBool("IsOpen") == false)
        {
            collected = false;
        }
    }
*/
    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            InteractIconOff.Invoke();
            collected = false;
        }
    }
}




