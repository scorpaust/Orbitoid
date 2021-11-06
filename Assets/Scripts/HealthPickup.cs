using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private int healthAmount;

    [SerializeField]
    private GameObject pickupFX;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			PlayerHealthController.instance.HealPlayer(healthAmount);

			if (pickupFX != null)
			{
				Instantiate(pickupFX, transform.position, Quaternion.identity);
			}

			Destroy(gameObject);
		}
	}
}
