using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Melee Weapon Settings")]
    [Tooltip("Daño que inflige el arma por golpe")]
    public int damage = 50;
    
    [Tooltip("Tiempo de cooldown entre golpes (segundos)")]
    public float cooldownTime = 1.0f;
    
    [Tooltip("Si está activa, puede hacer daño")]
    public bool isActive = true;
    
    [Header("Visual Feedback")]
    [Tooltip("Color cuando puede atacar")]
    public Color readyColor = Color.white;
    
    [Tooltip("Color durante cooldown")]
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    
    private bool canDamage = true;
    private Renderer weaponRenderer;
    private Material weaponMaterial;
    private Color originalColor;
    
    void Start()
    {
        // Obtener renderer para feedback visual
        weaponRenderer = GetComponentInChildren<Renderer>();
        if (weaponRenderer != null)
        {
            // Store material reference to avoid creating new instances
            weaponMaterial = weaponRenderer.material;
            originalColor = weaponMaterial.color;
        }
        
        // Verificar que tenga collider trigger
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            // Buscar en hijos
            col = GetComponentInChildren<Collider>();
        }
        
        if (col != null && !col.isTrigger)
        {
            Debug.LogWarning("MeleeWeapon: El Collider debe ser un Trigger. Configurando automáticamente.");
            col.isTrigger = true;
        }
        
        if (col == null)
        {
            Debug.LogError("MeleeWeapon: No se encontró ningún Collider! Añade un Collider y márcalo como Trigger.");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!isActive || !canDamage) return;
        
        // Intentar dañar al enemigo
        TryDamageEnemy(other);
    }
    
    void OnTriggerStay(Collider other)
    {
        // Intentar dañar mientras esté en contacto (respetando cooldown)
        if (!isActive || !canDamage) return;
        
        TryDamageEnemy(other);
    }
    
    void TryDamageEnemy(Collider other)
    {
        // Buscar componente EnemyA
        EnemyA enemyA = other.GetComponent<EnemyA>();
        if (enemyA == null)
        {
            enemyA = other.GetComponentInParent<EnemyA>();
        }
        
        if (enemyA != null)
        {
            enemyA.TakeDamage(damage);
            Debug.Log("✓ Lanza golpeó a EnemyA con " + damage + " de daño!");
            StartCooldown();
            return;
        }
        
        // Buscar componente EnemyB
        EnemyB enemyB = other.GetComponent<EnemyB>();
        if (enemyB == null)
        {
            enemyB = other.GetComponentInParent<EnemyB>();
        }
        
        if (enemyB != null)
        {
            enemyB.TakeDamage(damage);
            Debug.Log("✓ Lanza golpeó a EnemyB con " + damage + " de daño!");
            StartCooldown();
            return;
        }
    }
    
    void StartCooldown()
    {
        if (!canDamage) return; // Ya está en cooldown
        
        canDamage = false;
        
        // Cambiar color visual
        if (weaponMaterial != null)
        {
            weaponMaterial.color = cooldownColor;
        }
        
        StartCoroutine(CooldownCoroutine());
    }
    
    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        
        canDamage = true;
        
        // Restaurar color
        if (weaponMaterial != null)
        {
            weaponMaterial.color = readyColor;
        }
        
        Debug.Log("Lanza lista para atacar de nuevo!");
    }
    
    // Método público para activar/desactivar el arma
    public void SetActive(bool active)
    {
        isActive = active;
        gameObject.SetActive(active);
        
        if (active)
        {
            Debug.Log("Lanza equipada");
        }
        else
        {
            Debug.Log("Lanza guardada");
        }
    }
}
