using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject bossToActivate;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			bossToActivate.SetActive(true);

			gameObject.SetActive(false);
		}
	}
}
