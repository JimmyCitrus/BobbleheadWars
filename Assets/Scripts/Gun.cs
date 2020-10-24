using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform launchPosition;

    private AudioSource audioSource;

    public bool isUpgraded;
    public float upgradeTime = 10.0f;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        //Get the gunshot audio
        audioSource = GetComponent<AudioSource>();
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

        //Increments the time. if time's up, take the power up away
        currentTime += Time.deltaTime;
        if (currentTime > upgradeTime && isUpgraded == true)
        {
            isUpgraded = false;
        }
    }

    // creates the bullet
    void fireBullet()
    {
        Rigidbody bullet = createBullet();
        bullet.velocity = transform.parent.forward * 100;

        if (isUpgraded)
        {
            //Fire the next 2 bullets at an angle
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
            (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
            ((transform.right * -1) + transform.forward / 0.5f) * 100;
        }

        //Choose which sound to play
        if (isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

    //encapsulates the bullet creation process
    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }

    //Upgrades the gun
    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }




}
