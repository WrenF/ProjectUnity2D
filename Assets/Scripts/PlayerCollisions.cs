using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerCollisions : MonoBehaviour
{
    public int life = 3;
    private Vector3 startPos;
	public Transform respawnPoint;

    private void Start()
    {
        if (respawnPoint == null)
        {
            startPos = transform.position;
        }
        else
        {
            startPos = respawnPoint.position;
        }
    }
            
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            TakeDamages(3);
        }

        if (collision.CompareTag("EndLevel"))
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void TakeDamages(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Die();
        }
    }

//petit saut mort
    public void Die()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 0f);
        GetComponent<Collider2D>().isTrigger = true;
        
        Invoke("Respawn", 1f);
    }

    private void Respawn()
    {
        transform.position = startPos;
        life = 3;
        
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}