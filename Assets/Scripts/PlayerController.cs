using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    
    public float moveSpeed = 5f;
    [FormerlySerializedAs("JumpForce")] public float jumpForce = 20f;
    //public float attackDamage = 1f;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool isGrounded;

    public float attackRange = 1.5f;
	public int damage = 1;
	public SpriteRenderer _spriteRenderer;
	

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        bool isJumping = Input.GetButtonDown("Jump");
        float horizontal = Input.GetAxis("Horizontal");
        bool isAttacking = Input.GetMouseButtonDown(0);
        
        isGrounded = Mathf.Abs(_rigidbody.linearVelocity.y) < 0.1f;

        transform.Translate(horizontal * Time.deltaTime * moveSpeed, 0, 0);
        
        _animator.SetFloat(Horizontal, Mathf.Abs(horizontal));
        _animator.SetBool(Jump, isJumping);
        
        //Permet le regard du perso droite gauche
        if (horizontal > 0)
            _spriteRenderer.flipX = false;
        if (horizontal < 0)
            _spriteRenderer.flipX = true;
        
        _animator.SetBool("Attack", Input.GetButtonDown("Fire1"));
        
        //joueur ne vole pas avec plusieurs sauts
        if (isJumping && _rigidbody.linearVelocity.y == 0 )
        {
            _rigidbody.AddForce(Vector2.up * jumpForce);
        }
		if (isAttacking)
		{
			PerformAttack();
		}
    }
	
	void PerformAttack()
    {
        _animator.SetTrigger("Attack");
		Vector2 attackDirection = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
		foreach(Collider2D collider in hitColliders)
		{
			if(collider.CompareTag("Enemy"))
			{
				Vector2 directionToEnemy = (collider.transform.position - transform.position).normalized;
	
				if(Vector2.Dot(attackDirection, directionToEnemy) > 0)
				{
					EnemyController enemyScript = collider.GetComponent<EnemyController>();
					enemyScript.TakeDamage(damage);
				}
			}
		}
    }
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}