using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowControl : MonoBehaviour {

    public float speed;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed);
    }

    // Update is called once per frame
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
