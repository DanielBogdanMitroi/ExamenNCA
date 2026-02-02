using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] public int health = 100;
    [SerializeField] public int shieldHealth = 50;

    bool NoShield()
    {
        return shieldHealth <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (NoShield())
        {
            health -= damage;
            Debug.Log("Player took " + damage + " damage. Remaining health: " + health);
            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            Debug.Log("Shield absorbed the damage.");
            shieldHealth -= damage;
        }
    }

    void Die()
    {
        Debug.Log("Player is dead.");
        // Aquí puedes añadir lógica de muerte (reiniciar nivel, mostrar game over, etc.)
    }
}