using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the mouse button is held down, the bullets fire repeatedly
        if (Input.GetMouseButtonDown(0))
        {
            if(!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, 0.1f);
            }
        }

        //If the mouse button isn't being pressed, stop shooting
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
    }

    void fireBullet()
    {
        //1 Instantiates the bullet prefab as our new bullet variable
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        //2 Sets the bullet's position to where it will be shot
        bullet.transform.position = launchPosition.position;
        //3 Sets the bullet's velocity when shot
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;
    }
}
