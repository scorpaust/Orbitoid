using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BulletController // INHERITANCE
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        InitializeBossBullet(); // ABSTRACTION
    }

    private void InitializeBossBullet()
	{
        DamageAmount = 10;

        MoveDir = transform.position - PlayerHealthController.instance.transform.position;

        float angle = Mathf.Atan2(MoveDir.y, MoveDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    protected override void Update()
    {
        RB.velocity = -transform.right * BulletSpeed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
            PlayerHealthController.instance.DamagePlayer(DamageAmount);
		}

        if (ImpactEffect != null)
		{
            Instantiate(ImpactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
		}
	}
}
