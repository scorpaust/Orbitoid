using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 8f;

    [SerializeField]
    private float jumpForce = 20f;

    [SerializeField]
    private Transform groundPoint;

    [SerializeField]
    private LayerMask whatIsGround;

    private Rigidbody2D rb;

    private Animator anim;

    private bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump();

        Animate();
    }

    private void Move()
	{
        // Sideway movement
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed,rb.velocity.y);

        // Handle direction change
        if (rb.velocity.x < 0)
		{
            transform.localScale = new Vector3(-1f, 1f, 1f);
		}
        else if (rb.velocity.x > 0)
		{
            transform.localScale = Vector3.one;
        }
	}

    private void Jump()
	{
        // Check if on the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        // Jump
        if (Input.GetButtonDown("Jump") && isOnGround)
		{
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}

    private void Animate()
	{
        // Jump animation
        anim.SetBool("IsOnGround", isOnGround);

        // Run animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
	}
}
