using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    PATROL,
    CHASE,
    DEAD
}

public enum EnemyType
{
    WALKER,
    FLYER,
    BOSS
}

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] patrolPoints;

    [SerializeField]
    protected float moveSpeed, waitAtPoints;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private EnemyType type;

    private int currentPoint;

    private float waitCounter;

    protected Animator anim;

    private EnemyState state;

    public EnemyState State { get { return state; } set { state = value; } } 


	private void Awake()
	{
        anim = GetComponentInChildren<Animator>();
	}

	// Start is called before the first frame update
	protected void Start()
    {
        waitCounter = waitAtPoints;
        state = EnemyState.IDLE;
        NullifyPatrolPointsParent();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    protected virtual void Patrol()
	{
        if (type == EnemyType.WALKER)
		{
            if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > 0.2f)
			{
                state = EnemyState.PATROL;

                if (transform.position.x < patrolPoints[currentPoint].position.x)
				{
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                    transform.localScale = new Vector3(-1f, 1f, 1f);
				}
                else
				{
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                    transform.localScale = Vector3.one;
				}

                if (transform.position.y < patrolPoints[currentPoint].position.y - 0.5f && rb.velocity.y < 0.1f)
				{
                    rb.velocity = new Vector2(0f, jumpForce);
				}
			} else
			{
                state = EnemyState.IDLE;

                rb.velocity = new Vector2(0f, rb.velocity.y);

                waitCounter -= Time.deltaTime;

                if (waitCounter <= 0)
				{
                    waitCounter = waitAtPoints;

                    currentPoint++;

                    if (currentPoint >= patrolPoints.Length)
					{
                        currentPoint = 0;
					}
				}
			}

            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		}
        else
		{
            return;
		}
	}

    protected void NullifyPatrolPointsParent()
	{
        foreach (Transform pPoint in patrolPoints)
		{
            pPoint.SetParent(null);
		} 
	}
}
