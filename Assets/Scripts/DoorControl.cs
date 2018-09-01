using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour {

    public GameObject enemy;
    public Sprite doorOpen;

    private EnemyScript e;
    private Enemy2Script e2;
    private SpriteRenderer srenderer;
    private float wait = 3;

    // Use this for initialization
    void Start () {

        srenderer = GetComponent<SpriteRenderer>();

        if (enemy != null)
        {
            e = enemy.GetComponent<EnemyScript>();
            e2 = enemy.GetComponent<Enemy2Script>();
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (e != null)
        {
            if (e.health < 0 && wait > 0)
            {
                wait -= Time.deltaTime;
            }
        }

        if (e2 != null)
        {
            if (e2.health < 0 && wait > 0)
            {
                wait -= Time.deltaTime;
            }
        }

        if ( wait <= 0)
        {
            srenderer.sprite = doorOpen;
        }
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "attack" && wait <= 0)
        {
            SceneManager.LoadScene("continue");
        }
    }
}
