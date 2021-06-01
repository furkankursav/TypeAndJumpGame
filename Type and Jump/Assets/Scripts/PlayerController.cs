using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    
    public Rigidbody2D rb;
    public Animator anim;

    public float movementSpeed = 5f;
    public float jumpForce = 9f;

    private bool isFacingRight = true;
    private int facingDirection = 1;

    private bool isGrounded, isDetectingWall, isRunning;
    [SerializeField]
    private Transform groundCheck, wallCheck;
    public float groundCheckRadius = 0.3f, wallCheckDistance;
    public LayerMask whatIsGround;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private bool wasGrounded;

    private bool inAir;
    private bool wasInAir;

    private bool gotKey = false;
    [HideInInspector]
    public bool isAlive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAlive = true;
        transform.Find("Weapon").gameObject.SetActive(true);
    }

    private void Update()
    {

        if (!isAlive)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isDetectingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isRunning = (Mathf.Abs(rb.velocity.x) > 0.01f) ? true : false;
        inAir = (rb.velocity.y > 0.01f) ? true : false;


        if(isDetectingWall && isRunning && isGrounded)
        {
            Stop();
        }


        if(!wasGrounded && isGrounded)
        {
            Stop();
        }

        if(wasInAir && !inAir)
        {
            rb.velocity = new Vector2(movementSpeed * facingDirection, rb.velocity.y);
        }

        wasGrounded = isGrounded;
        wasInAir = inAir;


        

        anim.SetBool("run", isRunning);
        anim.SetBool("idle", !isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        
    }

    public void PlayFootSound()
    {
        AudioManager.instance.Play("FootStep");
    }

    public void GoRight()
    {
        Debug.Log("Karakter sağa gidecek");
        if (!isFacingRight)
        {
            Turn();
        }
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        //anim.SetBool("run", true);
    }

    public void GoLeft()
    {
        Debug.Log("Karakter sola gidecek");
        if (isFacingRight)
        {
            Turn();
        }
        rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        //anim.SetBool("run", true);
    }

    public void Jump()
    {
        Debug.Log("Karakter zıplayacak");
        /* if(isGrounded)
             rb.velocity = new Vector2(rb.velocity.x, jumpForce);*/
        AudioManager.instance.Play("Jump");
        //FindObjectOfType<AudioManager>().Play("Jump");
        StartCoroutine(JumpCorotuine());
        
    }

    IEnumerator JumpCorotuine()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        yield return new WaitForSeconds(0.5f);
        //rb.velocity = new Vector2(movementSpeed * facingDirection, rb.velocity.y);
    }

    public void JumpRight()
    {
        if (!isFacingRight)
        {
            Turn();
        }
        rb.velocity = new Vector2(movementSpeed / 2, jumpForce);
    }

    public void JumpLeft()
    {
        if (isFacingRight)
        {
            Turn();
        }
        rb.velocity = new Vector2(-movementSpeed / 2, jumpForce);
    }

    public void GoForward()
    {
        transform.position += new Vector3(5f * facingDirection, 0f);
    }

    

    public void Fire()
    {
        if (isGrounded)
        {
            Debug.Log("Karakter ateş edecek");
            //anim.SetTrigger("fire");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioManager.instance.Play("Fire");
        }
        
    }

    public void Turn()
    {
        Debug.Log("Karakter döndürülücek");
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        facingDirection *= -1;
        Stop();
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        //anim.SetBool("idle", true);
        //anim.SetBool("run", false);
    }

    public void Help()
    {
        //Yardım ekranı gösterilecek
        FindObjectOfType<GameManager>().YardimPaneliAc();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            //key sound
            AudioManager.instance.Play("KeyPickup");

            //particle effect
            Destroy(other.gameObject);
            gotKey = true;
        }

        else if (other.gameObject.CompareTag("KapiTetikleyici"))
        {
            if (gotKey)
            {
                AudioManager.instance.Play("KeyPickup");
                other.gameObject.GetComponentInParent<Animator>().SetTrigger("Ac");

            }

        }

        else if (other.gameObject.CompareTag("End"))
        {
            Stop();
            Debug.Log("Oyun bitti!");
            FindObjectOfType<GameManager>().FinishGame();
        }

        else if (other.gameObject.CompareTag("DeadZone"))
        {
            Stop();
            anim.SetTrigger("Death");
            gameObject.transform.Find("Weapon").gameObject.SetActive(false);
            isAlive = false;
            //play death sound
            FindObjectOfType<GameManager>().Die();
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAlive)
        {
            Stop();
            anim.SetTrigger("Death");
            gameObject.transform.Find("Weapon").gameObject.SetActive(false);
            isAlive = false;
            FindObjectOfType<GameManager>().Die();
        }
    }





    public bool GetIsAlive()
    {
        return isAlive;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
