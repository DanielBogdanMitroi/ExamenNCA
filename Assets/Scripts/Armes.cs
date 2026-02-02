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
            ammo--;
            Debug.Log("Ammo remaining: " + ammo);
            
            // Raycast from camera (screen center)
            Camera cam = Camera.main;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.Log("Hit: " + hit.transform.name);
                
                // Try to find enemy script on hit object or parents
                EnemyA enemyA = hit.transform.GetComponentInParent<EnemyA>();
                if (enemyA != null)
                {
                    Debug.Log("✓ Damaging EnemyA with " + damage + " damage");
                    enemyA.TakeDamage(damage);
                    return;
                }

                EnemyB enemyB = hit.transform.GetComponentInParent<EnemyB>();
                if (enemyB != null)
                {
                    Debug.Log("✓ Damaging EnemyB with " + damage + " damage");
                    enemyB.TakeDamage(damage);
                    return;
                }

                Debug.Log("Hit object has no damage component");
            }
            else
            {
                Debug.Log("Raycast didn't hit anything");
            }
        }
        else
        {
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