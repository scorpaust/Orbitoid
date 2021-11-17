using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }

    [SerializeField]
    private Vector2 moveDir;

    [SerializeField]
    private GameObject impactEffect;

    public GameObject ImpactEffect {  get { return impactEffect; } }

    [SerializeField]
    private int damageAmount = 1;

    protected int DamageAmount {  get { return damageAmount; } set { damageAmount = value; } }

    // ENCAPSULATION
    public Vector2 MoveDir { get { return moveDir; } set { moveDir = value; } }

    private Rigidbody2D rb;

    public Rigidbody2D RB { get { return rb; } private set { } }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveBullet();
    }

    protected virtual void MoveBullet()
	{
        rb.velocity = moveDir * bulletSpeed;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damageAmount);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
		{
            BossHealthController.instance.TakeDamage(damageAmount);
		}

        if ((collision.CompareTag("Ground") || collision.CompareTag("Enemy")) && impactEffect != null)
		{
            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
		}

        if (collision.CompareTag("Door") && impactEffect != null)
		{
            Instantiate(impactEffect, new Vector3(transform.position.x - .5f, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }

        AudioManager.instance.PlaySFXAdjusted(3);
	}

	protected void OnBecameInvisible()
	{
        Destroy(gameObject);
	}
}
