using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {
    public float speed;
    public int side;
	// Use this for initialization
	void Start () {
        side = -1;
	}
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right*speed*side);
    }
    // Update is called once per frame
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void Update () {
		
	}
}
