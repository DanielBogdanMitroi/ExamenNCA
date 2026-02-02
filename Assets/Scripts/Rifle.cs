using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapons
{
    private float timeshoot = 0f;

    void Start()
    {
        Init(30, 30, 15, 100f, 0.15f, 2.0f);
        /* 
        int ammo = 30;
        int maxammo = 30;
        int damage = 15;
        float range = 100f;
        float firecadence = 0.15f (disparo más rápido);
        float reloadtime = 2.0f;
        */
    }

    void Update()
    {
        timeshoot += Time.deltaTime;
        
        if (Input.GetButton("Fire1"))
        {
            if (timeshoot >= firecadence)
            {
                timeshoot = 0;
                Shoot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
}