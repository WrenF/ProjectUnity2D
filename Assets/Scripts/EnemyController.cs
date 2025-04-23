using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isAlive = true;
    public int maxHealth = 3;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                isAlive = false;
				Destroy(gameObject, 1f);
            }
        }
    }
}
