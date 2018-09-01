using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public bool isOnGround;

    public bool checkGround()
    {
        return isOnGround;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == 8)
        {
            isOnGround = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == 8)
        {
            isOnGround = false;
        }

    }
}
