using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Attack = Animator.StringToHash("Attack");
    
    public float moveSpeed = 5f;
    [FormerlySerializedAs("JumpForce")] public float jumpForce = 20f;
    public float attackDamage = 1f;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool isGrounded;

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
    }
    
}