using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float moveSpeed;
    public float chargeSpeed;
    public GameObject playerRef;
    public Rigidbody2D rb;
    public Animator anim;
    private AudioSource audio;
    public float direction;
    public float health, max_health;
    public float closeDistance;
    public float MidDistance;
    public float cooldown, max_cooldown;
    public float jump_speed;
    public GameObject shockwave;
    public Transform leftSpawn;
    public Transform rightSpawn;
    public GameObject controller;
    public float CooldownReduce;
    public bool dead;

    private SpriteRenderer renderer;
    public float dmg_time, max_dmg_time;

    //sons
    public AudioClip stomp;
    public AudioClip pulo;
    public AudioClip charge;

    // Use this for initialization
    void Start () {

        max_dmg_time = 0.25f;
        dead = false;

        chargeSpeed = 6.6f;
        max_health = 500;
        health = max_health;
        playerRef = GameObject.Find("Player");
        controller = GameObject.Find("GameController");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        direction = playerRef.gameObject.transform.position.x - gameObject.transform.position.x;

        max_cooldown = 2f-(controller.GetComponent<GeneralGameController>().counter*CooldownReduce);
        if (max_cooldown <= 0.1)
        {
            max_cooldown = 0.1f;
        }
        cooldown = max_cooldown;

        renderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (dmg_time <= 0)
        {
            renderer.color = new Color(1, 1, 1, 1);
        }

        if ( rb.velocity.y < 0 )
        {
            anim.SetBool("isFalling", true);
        } else
        {
            anim.SetBool("isFalling", false);
        }

        dmg_time -= Time.deltaTime;

        AttackControl();

        if ( health <= 0 )
        {
            anim.SetBool("dead", true);
            rb.velocity = new Vector2(0, Time.deltaTime / 1000 );
            isdead();
        }

        if (!anim.GetBool("charge"))
        {
            direction = playerRef.gameObject.transform.position.x - gameObject.transform.position.x;
            if (direction != 0)
            {
                direction /= Mathf.Abs(direction);
                transform.localScale = new Vector3( -direction * 4, 4, 1);
                
            }
        }

	}

    void FixedUpdate()
    {
        FixedAttackControl();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().tag=="attack" && health > 0 && dmg_time <= 0 )
        {
            
            health -= playerRef.GetComponent<PlayerMove>().damage;

            if ( health < 0 )
            {
                anim.SetBool("charge", false);
            }
            
            dmg_time = max_dmg_time;
            renderer.color = new Color(1, 1, 1, 0.5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="wall")
        {
            anim.SetBool("charge", false);
            rb.velocity = new Vector2(0, 0);

        }

        if ( collision.collider.gameObject.layer == 8 && anim.GetBool("jump") )
        {
            audio.PlayOneShot(stomp);
            Instantiate(shockwave, leftSpawn.position, Quaternion.Euler(Vector3.zero)).transform.localScale = new Vector3(4, 4, 1);
            Instantiate(shockwave, rightSpawn.position, Quaternion.Euler(new Vector3(0, 180, 0))).transform.localScale = new Vector3(4, 4, 1);

            anim.SetBool("jump", false);
        }
    }


    void FixedAttackControl()
    {

        if (anim.GetBool("charge"))
        {
            rb.velocity = new Vector2( direction * chargeSpeed * 2, 0) ;
        }

        
        if(anim.GetBool("attack1"))
        {

        }

    }

    void AttackControl()
    {

        if (playerRef.GetComponent<PlayerMove>().health > 0 && health > 0)
        {

            if (!anim.GetBool("charge"))
            {
                if (cooldown <= 0)
                {

                    float distance = Mathf.Abs(playerRef.gameObject.transform.position.x - gameObject.transform.position.x);

                    if (distance <= closeDistance)
                    {
                        if (Random.value >= 0.2)
                        {
                            print("Close Attack" + distance);
                            anim.SetBool("attack1", true);
                        }
                        else
                        {
                            print("Charge" + distance);
                            anim.SetBool("charge", true);
                            audio.PlayOneShot(charge);
                        }
                    }
                    else if (distance <= MidDistance)
                    {
                        if (Random.value >= 0.666)
                        {
                            print("Charge" + distance);
                            anim.SetBool("charge", true);
                            audio.PlayOneShot(charge);
                        }
                        else
                        {
                            audio.PlayOneShot(pulo);
                            print("Shockwave" + distance);
                            anim.SetBool("jump", true);

                            rb.velocity = new Vector2(moveSpeed * direction, jump_speed);

                        }
                    }
                    else
                    {
                        print("Charge" + distance);
                        anim.SetBool("charge", true);
                        audio.PlayOneShot(charge);
                    }

                    cooldown = max_cooldown;

                }

                cooldown -= Time.deltaTime;
            }

        }

    }
    void endattack()
    {
        anim.SetBool("attack1", false);
        anim.SetBool("charge", false);
    }
    void isdead()
    {
        dead = true;
    }
    void stomper()
    {
        audio.PlayOneShot(stomp);
    }
}
