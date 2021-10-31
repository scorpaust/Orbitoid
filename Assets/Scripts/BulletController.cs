using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Vector2 moveDir;

    [SerializeField]
    private GameObject impactEffect;

    public Vector2 MoveDir { get { return moveDir; } set { moveDir = value; } }

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
	{
        rb.velocity = moveDir * bulletSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Ground") && impactEffect != null)
		{
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
		}
	}

	private void OnBecameInvisible()
	{
        Destroy(gameObject);
	}
}
