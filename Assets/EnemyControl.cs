using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour {
    public GameObject controller;
    private int x;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
        x = controller.GetComponent<GeneralGameController>().counter + 1;
        gameObject.GetComponent<Text>().text ="BOSS " + x.ToString();
	}
}
