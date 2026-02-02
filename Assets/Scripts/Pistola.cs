using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : Weapons
{
    private float timeshoot = 0f; // ← AÑADIDO

    void Start()
    {
        Init(12, 12, 20, 50f, 0.5f, 1.5f);
        /* 
        int ammo = 12;
        int maxammo = 12;
        int damage = 20;
        float range = 50f;
        float firecadence = 0.5f (tiempo entre disparos);
        float reloadtime = 1.5f;
        */
    }

    void Update()
    {
        timeshoot += Time.deltaTime;
        
        if (Input.GetButton("Fire1")) // Cambiado a GetButton para disparo continuo
        {
            if (timeshoot >= firecadence) // ← Cambio: => a >=
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