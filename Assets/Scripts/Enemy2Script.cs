using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour {

    public float moveSpeed;
    public float speed;
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
    public GameObject snow;
    public Transform spawn;
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
    void Start()
    {

        max_dmg_time = 0.25f;
        dead = false;

        speed = 6.6f;
        max_health = 500;
        health = max_health;
        playerRef = GameObject.Find("Player");
        controller = GameObject.Find("GameController");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        direction = playerRef.gameObject.transform.position.x - gameObject.transform.position.x;

        max_cooldown = 2f - (controller.GetComponent<GeneralGameController>().counter * CooldownReduce);
        if (max_cooldown <= 0.1)
        {
            max_cooldown = 0.1f;
        }
        cooldown = max_cooldown;

        renderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if (dmg_time <= 0)
        {
            renderer.color = new Color(1, 1, 1, 1);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }

        dmg_time -= Time.deltaTime;

        AttackControl();

        if (health <= 0)
        {
            rb.velocity = new Vector2(0, Time.deltaTime / 1000);
            isdead();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().tag == "attack" && health > 0 && dmg_time <= 0)
        {

            health -= playerRef.GetComponent<PlayerMove>().damage;

            if (health < 0)
            {
                isdead();
            }

            dmg_time = max_dmg_time;
            renderer.color = new Color(1, 1, 1, 0.5f);
        }

    }

    void AttackControl()
    {

        if (playerRef.GetComponent<PlayerMove>().health > 0 && health > 0)
        {

            if (cooldown <= 0)
            {

                float distance = Mathf.Abs(playerRef.gameObject.transform.position.x - gameObject.transform.position.x);

                rb.velocity = Vector2.zero;

                if (Random.value >= 0.5)
                {
                    print("attack1" + distance);
                    anim.SetTrigger("attack1");
                    Instantiate(snow, spawn.position, Quaternion.Euler(Vector3.zero)).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 90))).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 180))).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 270))).transform.localScale = new Vector3(4, 4, 1);
                    playAttack1Sound();
                }
                else
                {
                    print("attack2" + distance);
                    anim.SetTrigger("attack1");
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 45))).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 135))).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 225))).transform.localScale = new Vector3(4, 4, 1);
                    Instantiate(snow, spawn.position, Quaternion.Euler(new Vector3(0, 0, 315))).transform.localScale = new Vector3(4, 4, 1);
                    playAttack2Sound();
                }

                cooldown = max_cooldown;

            }

            cooldown -= Time.deltaTime;

        }

    }
    
    void isdead()
    {
        dead = true;
        anim.SetBool("dead", true);
    }

    void endAttack()
    {
        rb.velocity += ( new Vector2( Random.Range(1f, -1f) , Random.Range(1f, -1f) ).normalized ) * speed;
    }

    void playAttack1Sound()
    {
        audio.PlayOneShot(stomp);
    }

    void playAttack2Sound()
    {
        audio.PlayOneShot(stomp);
    }

}