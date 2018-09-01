using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartTotemScript : MonoBehaviour {

    public Sprite otherSprite;
    public GameObject controller;
    public GameObject curseBolt;

    public bool casted;
    public float wait;

    // Use this for initialization
    void Start()
    {
        casted = false;
        wait = 1.5f;
        controller = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (casted)
        {
            if (wait <= 0)
            {
                if (Random.value >= 0.5f)
                    SceneManager.LoadScene("IceScene");
                else
                    SceneManager.LoadScene("SampleScene");
            }
            wait -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "attack" && !casted)
        {
            GetComponent<SpriteRenderer>().sprite = otherSprite;
            controller.GetComponent<GeneralGameController>().currentPlayerHealth = controller.GetComponent<GeneralGameController>().currentPlayerHealth / 2;
            curseBolt.GetComponent<Animator>().SetTrigger("cast");
            casted = true;
        }
    }
}
