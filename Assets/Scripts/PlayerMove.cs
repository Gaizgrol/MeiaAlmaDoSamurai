using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float starting_health, max_health, health;
    public float max_speed;
    public float damage;

    public GameObject ground_check;
    public bool onGround;

    public bool jump;
    public float jump_speed;

    public bool stun;

    public float hspeed, vspeed;

    public float knockback;

    public float cool, max_cool;
    public bool attacking;

    public bool recover;
    public float recover_cool, max_recover_cool;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    private SpriteRenderer srenderer;
    private AudioSource audio;


    //sons
    public AudioClip slash;
    public AudioClip pulo;
    public AudioClip playermorte;
    public AudioClip dano;
    public bool grito;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        srenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

        max_speed = 7.5f;
        
        health = max_health;

        jump_speed = 15;
        knockback = 200;

        max_cool = 0.33f;
        cool = max_cool;
        attacking = false;

        stun = false;

        recover = false;
        max_recover_cool = 1f;
        recover_cool = max_recover_cool;
        grito = true;

    }

    // Physics update
    void FixedUpdate()
    {

        onGround = ground_check.GetComponent<GroundCheck>().checkGround();

        if (onGround && jump)
        {
            audio.PlayOneShot(pulo);
            vspeed = jump_speed;
        }
        else
        {
            vspeed = rb.velocity.y;
            if (!jump && rb.velocity.y > 0)
            {
                vspeed = vspeed * 0.9f;
            }
        }

        if (!stun)
        {
            rb.velocity = new Vector2(max_speed * ((hspeed == 0) ? 0 : hspeed / Mathf.Abs(hspeed)), vspeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (recover)
        {

            srenderer.color = new Color(1, 1, 1, 0.5f);

            if (recover_cool >= 0)
            {
                recover_cool -= Time.deltaTime;
            }
            else
            {

                recover_cool = max_recover_cool;

                recover = false;

                srenderer.color = new Color(1, 1, 1, 1f);

                bc.enabled = true;

            }

        }

        if (attacking)
        {
            cool -= Time.deltaTime;
        }

        if ( cool <= 0 )
        {
            attacking = false;
            cool = max_cool;
        }

        if (hspeed == 0)
        {
            anim.SetBool("isMoving", false);
        }
        if ( onGround )
        {
            anim.SetBool("grounded", true);
        }
        else
        {
            anim.SetBool("grounded", false);
        }

        if (health <= 0)
        {
            
            anim.SetTrigger("death");
            stun = false;
            hspeed = 0;
            jump = false;
            anim.SetBool("isMoving", false);
            if (grito)
            {
                audio.PlayOneShot(playermorte);
                grito = false;
            }
        }
        else
        {

            if (!stun)
            {

                hspeed = Input.GetAxisRaw("Horizontal");

                if (hspeed != 0)
                {
                    anim.SetBool("isMoving", true);
                }


                if (hspeed != 0)
                {
                    if (!attacking)
                    {
                        transform.localScale = new Vector3(hspeed * 4, 4, 1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    jump = true;
                    
                }
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    jump = false;
                }

                if (Input.GetKeyDown(KeyCode.Z) && !attacking && !recover)
                {
                    audio.PlayOneShot(slash);
                    anim.SetTrigger("attack");
                    attacking = true;
                    hspeed = 0;
                }
                if (!onGround )
                {
                    anim.SetBool("onair", true);
                }
                else
                {
                    anim.SetBool("onair", false);
                }
            }
            else
            {
                jump = false;
            }

        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !stun && !recover)
        {
            audio.PlayOneShot(dano);
            anim.SetTrigger("stun");
            stun = true;

            bc.enabled = false;

            health -= 30;

            float direction = Mathf.Sign(transform.position.x - collision.transform.position.x) * knockback;

            rb.velocity = new Vector2(0, 0);

            rb.AddForce(new Vector2(direction, 500f));


        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy" && !stun && !recover)
        {
            audio.PlayOneShot(dano);
            anim.SetTrigger("stun");
            stun = true;

            bc.enabled = false;

            health -= 30;

            float direction = Mathf.Sign(transform.position.x - collision.collider.transform.position.x) * knockback;

            rb.velocity = new Vector2(0, 0);

            rb.AddForce(new Vector2(direction, 500f));
            
        }   
        
        /*
        if (collision.collider.tag == "wall" && stun)
        {

            bc.enabled = false;

            float direction = Mathf.Sign(transform.position.x - collision.collider.transform.position.x) * knockback * 2;

            rb.velocity = new Vector2(0, 0);

            rb.AddForce(new Vector2(direction, 500f));

            anim.SetTrigger("stun");
            stun = true;
        }*/

    }

    public void socorro()
    {
        stun = true;
    }

    public void socorrido()
    {
        stun = false;
        recover = true;
    }

    private void OnBecameInvisible()
    {
        health = 0;
    }

}
