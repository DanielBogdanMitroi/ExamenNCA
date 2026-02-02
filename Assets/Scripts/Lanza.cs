using UnityEngine;

public class Lanza : Weapons
{
    private MeleeWeapon meleeComponent;
    
    void Start()
    {
        // Inicializar con valores (la lanza no usa munición)
        Init(
            ammo: 999,        // Munición infinita
            maxammo: 999,
            damage: 50,       // Daño base
            range: 3f,        // Alcance (no se usa en melee)
            firecadence: 1f,  // Cooldown entre golpes
            reloadtime: 0f    // No necesita recarga
        );
        
        // Obtener o añadir el componente MeleeWeapon
        meleeComponent = GetComponent<MeleeWeapon>();
        if (meleeComponent == null)
        {
            meleeComponent = gameObject.AddComponent<MeleeWeapon>();
        }
        
        // Sincronizar valores
        meleeComponent.damage = damage;
        meleeComponent.cooldownTime = firecadence;
        
        Debug.Log("Lanza inicializada - Daño: " + damage);
    }
    
    void Update()
    {
        // La lanza no necesita disparar, hace daño por contacto
        // Pero podemos permitir "atacar" presionando Fire1 para activar temporalmente
        
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("¡Ataque con lanza! Acércate a los enemigos.");
        }
    }
    
    // Override del método Shoot para que no haga nada (no es raycast)
    public new void Shoot()
    {
        Debug.Log("La lanza ataca por contacto, acércate al enemigo!");
    }
    
    // Override del método Reload para que no haga nada
    public new void Reload()
    {
        Debug.Log("La lanza no necesita recarga.");
    }
}
