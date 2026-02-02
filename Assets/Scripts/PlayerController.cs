using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("Shield Settings")]
    public int maxShield = 50;
    public int currentShield;
    
    [Header("Invulnerability")]
    public bool isInvulnerable = false;
    public Color invulnerableColor = new Color(0.5f, 0.5f, 1f, 1f); // Azul claro
    private Color originalColor;
    private Renderer playerRenderer;
    
    [Header("Death Settings")]
    public bool isDead = false;
    
    // Evento para notificar cuando el jugador muere
    public delegate void PlayerDeathEvent();
    public event PlayerDeathEvent OnPlayerDeath;
    
    // Propiedades públicas para compatibilidad con código existente
    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    
    public int shieldHealth
    {
        get { return currentShield; }
        set { currentShield = value; }
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        isDead = false;
        
        // Obtener el renderer para efectos visuales
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }
    }
    
    bool NoShield()
    {
        return currentShield <= 0;
    }

    public void TakeDamage(int damage)
    {
        // Si está muerto o invulnerable, no recibir daño
        if (isDead || isInvulnerable)
        {
            if (isInvulnerable)
            {
                Debug.Log("Jugador invulnerable! Daño bloqueado.");
            }
            return;
        }
        
        // Primero daña el escudo
        if (currentShield > 0)
        {
            int shieldDamage = Mathf.Min(damage, currentShield);
            currentShield -= shieldDamage;
            damage -= shieldDamage;
            Debug.Log("Escudo restante: " + currentShield);
        }
        
        // Luego daña la vida
        if (damage > 0)
        {
            currentHealth -= damage;
            Debug.Log("Vida restante: " + currentHealth);
        }
        
        // Prevenir vida negativa
        currentHealth = Mathf.Max(currentHealth, 0);
        currentShield = Mathf.Max(currentShield, 0);
        
        // Verificar muerte
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    
    public void Heal(int amount)
    {
        if (isDead) return;
        
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log("Vida curada. Vida actual: " + currentHealth);
    }
    
    public void RestoreShield(int amount)
    {
        if (isDead) return;
        
        currentShield += amount;
        currentShield = Mathf.Min(currentShield, maxShield);
        Debug.Log("Escudo restaurado. Escudo actual: " + currentShield);
    }
    
    public void ActivateInvulnerability(float duration)
    {
        if (isDead) return;
        
        if (!isInvulnerable)
        {
            StartCoroutine(InvulnerabilityCoroutine(duration));
        }
        else
        {
            Debug.Log("Ya tienes invulnerabilidad activa!");
        }
    }
    
    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        isInvulnerable = true;
        Debug.Log("Invulnerabilidad ACTIVADA por " + duration + " segundos");
        
        // Cambiar color del jugador (efecto visual)
        if (playerRenderer != null)
        {
            playerRenderer.material.color = invulnerableColor;
        }
        
        // Esperar la duración
        yield return new WaitForSeconds(duration);
        
        // Desactivar invulnerabilidad
        isInvulnerable = false;
        Debug.Log("Invulnerabilidad DESACTIVADA");
        
        // Restaurar color original
        if (playerRenderer != null)
        {
            playerRenderer.material.color = originalColor;
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Jugador ha muerto!");
        
        // Invocar el evento de muerte
        OnPlayerDeath?.Invoke();
        
        // Desactivar controles del jugador (opcional)
        // GetComponent<PlayerController>().enabled = false;
        
        // Nota: La pantalla de muerte se maneja en DeathScreen.cs
    }
    
    // Métodos públicos para obtener información
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
    
    public float GetShieldPercentage()
    {
        return (float)currentShield / maxShield;
    }
}