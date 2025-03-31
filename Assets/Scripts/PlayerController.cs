using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    public float moveSpeed = 5f;
    [FormerlySerializedAs("JumpForce")] public float jumpForce = 20f;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

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

        transform.Translate(horizontal * Time.deltaTime * moveSpeed, 0, 0);
        // if (isJumping) _rigidbody.AddForce(Vector2.up * jumpForce);
        
        _animator.SetFloat(Horizontal, Mathf.Abs(horizontal));
        
        if (horizontal > 0)
            _spriteRenderer.flipX = false;
        if (horizontal < 0)
            _spriteRenderer.flipX = true;
        
        //joueur ne vole pas avec plusieurs sauts
        if (isJumping && _rigidbody.linearVelocity.y == 0 )
        {
            _rigidbody.AddForce(Vector2.up * jumpForce);
        }
    }
}
