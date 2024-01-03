using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator _animator;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isSwinging;
    private Rigidbody2D rb;
    public bool isGrounded;
    [SerializeField] private Transform hookTransform;

    public bool IsDie
    {
        get { return transform.position.y < -5; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isSwinging)
        {
            spriteRenderer.flipY = false;
            if (rb.velocity.x > 0)
                spriteRenderer.flipX = false;
            else if (rb.velocity.x < 0)
                spriteRenderer.flipX = true;
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            spriteRenderer.flipX = false;
            if (rb.velocity.x > 0)
                spriteRenderer.flipY = false;
            else if (rb.velocity.x < 0)
                spriteRenderer.flipY = true;

            Vector3 dir = hookTransform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Update()
    {
        if (IsDie)
        {
            SceneManager.LoadScene(0);
        }

        _animator.SetFloat("Speed", rb.velocity.magnitude);
        _animator.SetBool("isSwining", isSwinging);
        _animator.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && !isSwinging && isGrounded)
        {
            DoJump();
        }
    }

    public void DoJump()
    {
        if (isGrounded && !isSwinging)
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && !isSwinging)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}