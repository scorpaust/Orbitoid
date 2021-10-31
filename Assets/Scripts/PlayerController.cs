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

    [SerializeField]
    private BulletController shotToFire;

    [SerializeField]
    private Transform shotPoint;

    [SerializeField]
    private float dashSpeed, dashTime;

    [SerializeField]
    private SpriteRenderer afterImageFX;

    [SerializeField]
    private float afterImafeFXLifetime;

    [SerializeField]
    private float timeBetweenAfterImages;

    [SerializeField]
    private Color afterImageColor;

    [SerializeField]
    private float waitAfterDashing;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    private Animator anim;

    private bool isOnGround;

    private bool canDoubleJump;

    private float dashCounter;

    private float afterImageCounter;

    private float dashRechargeCounter;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump();

        Animate();

        Fire();
    }

    private void Move()
	{
        if (dashRechargeCounter > 0)
		{
            dashRechargeCounter -= Time.deltaTime;
		} else
		{
            if (Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;

                ShowAfterImage();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter = dashCounter - Time.deltaTime;

            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImageCounter -= Time.deltaTime;

            if (afterImageCounter <= 0)
                ShowAfterImage();

            dashRechargeCounter = waitAfterDashing;
        }
        else
        {
            // Sideway movement
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

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
	}

    private void Jump()
	{
        // Check if on the ground
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

        // Jump
        if (Input.GetButtonDown("Jump") && (isOnGround || canDoubleJump))
		{
            if (isOnGround)
			{
                canDoubleJump = true;
            }
            else
			{
                canDoubleJump = false;

                anim.SetTrigger("DoubleJump");
            }
                

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

    private void Fire()
	{
        if (Input.GetButtonDown("Fire1"))
		{
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).MoveDir = new Vector2(transform.localScale.x, 0f);

            anim.SetTrigger("ShotFired");
		}
	}

    public void ShowAfterImage()
	{
        SpriteRenderer image = Instantiate(afterImageFX, sr.transform.position, sr.transform.rotation);

        image.sprite = sr.sprite;

        image.transform.localScale = transform.localScale;

        image.color = afterImageFX.color;

        Destroy(image.gameObject, afterImafeFXLifetime);

        afterImageCounter = timeBetweenAfterImages;
	}
}
