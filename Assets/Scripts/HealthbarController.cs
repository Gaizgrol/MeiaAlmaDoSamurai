using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour {

    public GameObject entity;
    public Text death_message;
    public GameObject customText;

    private Image fill;
    private Text dmg;

    // Use this for initialization
    void Start () {
        fill = GetComponent<Image>();
        death_message.enabled = false;
        dmg = customText.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (entity.GetComponent<PlayerMove>() != null)
        {

            PlayerMove pm = entity.GetComponent<PlayerMove>();

            if (dmg != null)
            {
                dmg.text = ( (int) pm.damage ).ToString();
            }

            fill.fillAmount = pm.health / pm.starting_health;

            if (pm.health <= 0)
            {
                death_message.enabled = true;
            }

        }

        if (entity.GetComponent<EnemyScript>() != null)
        {

            EnemyScript pm = entity.GetComponent<EnemyScript>();

            fill.fillAmount = pm.health / pm.max_health;

            if (pm.health <= 0)
            {
                death_message.enabled = true;
            }

        }

        if (entity.GetComponent<Enemy2Script>() != null)
        {

            Enemy2Script pm = entity.GetComponent<Enemy2Script>();

            fill.fillAmount = pm.health / pm.max_health;

            if (pm.health <= 0)
            {
                death_message.enabled = true;
            }

        }

    }

}
