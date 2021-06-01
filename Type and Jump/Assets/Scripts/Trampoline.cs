using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    bool onTop;
    GameObject bouncer;

    private Animator anim;
    public Vector2 velocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void JumpIt()
    {
        bouncer.GetComponent<Rigidbody2D>().velocity = velocity;
        AudioManager.instance.Play("Trampoline");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTop = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (onTop)
        {
            anim.SetBool("isPressed", true);
            bouncer = other.gameObject;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTop = false;
        anim.SetBool("isPressed", false);

    }
}
