using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroDialogue : Interactable
{
    public UnityEvent DisplayText;
    public UnityEvent NextSentence;
    public UnityEvent CloseText;
    public Animator animator;
    public GameObject DialogueBox;
    private bool collected = false;

    void Awake()
    {
        animator = DialogueBox.GetComponent<Animator>();
        //Interact();
    }

    public override void Interact()
    {
        if (collected == false)
        {
            DisplayText.Invoke();
            collected = true;
        }
    

        /*if (collected == false)
        {
            DisplayText.Invoke();
            collected = true;
            return;
        }
        else *//*(animator.GetBool("IsOpen") == true)*//*
        {
            NextSentence.Invoke();
            //collected = true;
            return;
        }*/
    }
    protected void OnTriggerEnter2D(Collider2D obj)
    {
        Interact();
    } 

    /*


    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            CloseText.Invoke();
            collected = true;
        }
    }*/
}




