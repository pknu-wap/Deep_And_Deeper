using UnityEngine;

public class Run : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void Start()

    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var inputX = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(inputX * 3, _rigidbody2D.velocity.y);
        _spriteRenderer.flipX = inputX switch
        {
            < 0 => true,
            > 0 => false,
            _ => _spriteRenderer.flipX
        };

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            Debug.Log("enters");
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 7f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }
}