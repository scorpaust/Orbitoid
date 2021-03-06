using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 8f;

    [SerializeField]
    private bool canMove;

    public bool CanMove { get { return canMove; } set { canMove = value; } } // ENCAPSULATION

    [SerializeField]
    private float jumpForce = 20f;

    [SerializeField]
    private Transform groundPoint;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private LayerMask whatIsDestructable;

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

    [SerializeField]
    private GameObject standing, ball;

    [SerializeField]
    private float waitToBall, ballCounter;

    [SerializeField]
    private Animator ballAnim;

    [SerializeField]
    private Transform bombPoint;

    [SerializeField]
    private GameObject bomb;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    private Animator anim;

    public Animator Anim { get { return anim; } private set { } } //ENCAPSULATION

    private bool isOnGround;

    private bool isOnDestructable;

    private bool canDoubleJump;

    private float dashCounter;

    private float afterImageCounter;

    private float dashRechargeCounter;

    private PlayerAbilityTracker abilities;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();

        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0f)
		{
            Move(); // ABSTRACTION

            Jump(); // ABSTRACTION

            CheckBallMode(); // ABSTRACTION

            Fire(); // ABSTRACTION
        } else
		{
            rb.velocity = Vector2.zero;
		}

        Animate(); // ABSTRACTION
    }

    private void Move()
	{
        if (dashRechargeCounter > 0)
		{
            dashRechargeCounter -= Time.deltaTime;
		} else
		{
            if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.CanDash)
            {
                dashCounter = dashTime;

                ShowAfterImage();

                AudioManager.instance.PlaySFXAdjusted(7);
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

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

        // Check if on a destructable block
        isOnDestructable = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsDestructable);

        // Jump
        if (Input.GetButtonDown("Jump") && (isOnGround || isOnDestructable || (canDoubleJump && abilities.CanDoubleJump)))
		{
            if (isOnGround)
			{
                canDoubleJump = true;

                AudioManager.instance.PlaySFXAdjusted(12);
            }
            else
			{
                canDoubleJump = false;

                anim.SetTrigger("DoubleJump");

                AudioManager.instance.PlaySFXAdjusted(9);
            }
                

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}

    private void StandingAnimation()
	{
        // Jump animation
        if (isOnGround || isOnDestructable)
            anim.SetBool("IsOnGround", true);
        else
            anim.SetBool("IsOnGround", false);

        // Run animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
	}

    private void Fire()
	{
        if (Input.GetButtonDown("Fire1"))
		{
            if (standing.activeSelf)
			{
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).MoveDir = new Vector2(transform.localScale.x, 0f);

                anim.SetTrigger("ShotFired");

                AudioManager.instance.PlaySFXAdjusted(14);

            } else if (ball.activeSelf && abilities.CanDropBomb)
			{
                Instantiate(bomb, bombPoint.position, bombPoint.rotation);

                AudioManager.instance.PlaySFXAdjusted(14);
            }
            
		}
	}

    private void CheckBallMode()
	{
        if (!ball.activeSelf)
		{
            if (Input.GetAxisRaw("Vertical") < -.9f && abilities.CanBecomeBall)
			{
                ballCounter -= Time.deltaTime;

                if (ballCounter <= 0)
				{
                    ball.SetActive(true);

                    standing.SetActive(false);

                    AudioManager.instance.PlaySoundFX(6);
				}
			}
            else
            {
                ballCounter = waitToBall;
            }

        } else
		{
            if (Input.GetAxisRaw("Vertical") > 0.9f)
            {
                ballCounter -= Time.deltaTime;

                if (ballCounter <= 0)
                {
                    ball.SetActive(false);

                    standing.SetActive(true);

                    AudioManager.instance.PlaySoundFX(10);
                }
            }
            else
            {
                ballCounter = waitToBall;
            }
        }
        
	}

    private void BallAnimation()
	{
        ballAnim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
	}

    private void Animate()
	{
        if (standing.activeSelf)
            StandingAnimation();
        else
            BallAnimation();
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
