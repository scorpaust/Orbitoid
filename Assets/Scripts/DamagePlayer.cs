using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;

	[SerializeField]
	private bool destroyOnDamage;

	[SerializeField]
	private GameObject destroyEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			DealDamage();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			DealDamage(); // ABSTRACTION
		}
	}

	private void DealDamage()
	{
		PlayerHealthController.instance.DamagePlayer(damageAmount);

		if (destroyOnDamage && destroyEffect != null)
		{
			Instantiate(destroyEffect, transform.position, transform.rotation);

			Destroy(gameObject);
		}
	}
}
