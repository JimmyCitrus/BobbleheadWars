using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        //If the bullet is invisible, delete it
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the bullet collides with anything, delete it
        Destroy(gameObject);
    }
}
