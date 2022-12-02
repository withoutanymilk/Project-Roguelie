using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHitbox : MonoBehaviour
{
    public UnityEvent DisplayText;
    public UnityEvent NextSentence;
    public UnityEvent CloseText;
    public UnityEvent InteractIconOn;
    public UnityEvent InteractIconOff;
    public Animator animator;
    public GameObject DialogueBox;
    public bool collected = false;

    void Awake()
    {
        animator = DialogueBox.GetComponent<Animator>();
    }

    protected void OnTriggerEnter2D(Collider2D obj)
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




