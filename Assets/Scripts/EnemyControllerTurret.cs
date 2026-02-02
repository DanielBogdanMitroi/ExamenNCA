using UnityEngine;

public class EnemyA : MonoBehaviour
{
    public int health = 100;
    public Transform player;
    //Attacking
    public float timeBetweenAttacks;
    public int damage = 10;
    bool alreadyAttacked;
    //States
    public float sightRange, attackRange;
    public LayerMask whatIsPlayer;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        else if (playerInSightRange && !playerInAttackRange) Activate();
    }

    void Activate()
    {
        transform.LookAt(player);
        Debug.Log("Enemy Activated!");
    }
    void AttackPlayer()
    {


        if (!alreadyAttacked)
        {
            ///Attack code here
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Debug.Log("Enemy Attacks!");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }




    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}