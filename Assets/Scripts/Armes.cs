using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    protected int ammo;
    protected int maxammo;
    protected int damage;
    protected float range;
    protected float firecadence;
    protected float reloadtime;
    protected RaycastHit hit;

    public void Init(int ammo, int maxammo, int damage, float range, float firecadence, float reloadtime)
    {
        this.ammo = ammo;
        this.maxammo = maxammo;
        this.damage = damage;
        this.range = range;
        this.firecadence = firecadence;
        this.reloadtime = reloadtime;
    }

    public void Shoot()
    {
        if (ammo > 0)
        {
            //Sfx
            ammo--;
            Debug.Log("Ammo remaining: " + ammo);
            
            //Raycast logic to detect hit
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                Debug.Log("Hit: " + hit.transform.name);
                
                // Try to damage EnemyA (Turret)
                EnemyA enemyA = hit.transform.GetComponent<EnemyA>();
                if (enemyA != null)
                {
                    enemyA.TakeDamage(damage);
                    return;
                }

                // Try to damage EnemyB (Dron)
                EnemyB enemyB = hit.transform.GetComponent<EnemyB>();
                if (enemyB != null)
                {
                    enemyB.TakeDamage(damage);
                    return;
                }

                Debug.Log("Hit object has no damage component");
            }
        }
        else
        {
            //Sfx
            Debug.Log("Out of ammo! Press R to reload.");
        }
    }

    public void Reload()
    {
        if (ammo < maxammo)
        {
            Debug.Log("Reloading...");
            ammo = maxammo;
            Debug.Log("Reload complete! Ammo: " + ammo);
        }
        else
        {
            Debug.Log("Magazine is already full!");
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public int GetMaxAmmo()
    {
        return maxammo;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetRange()
    {
        return range;
    }
}