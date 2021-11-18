using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject bossToActivate;

	[SerializeField]
	private string bossRef;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (PlayerPrefs.HasKey(bossRef))
			{
				if (PlayerPrefs.GetInt(bossRef) != 1)
				{
					bossToActivate.SetActive(true);

					gameObject.SetActive(false);
				}
			}
			else
			{
				bossToActivate.SetActive(true);

				gameObject.SetActive(false);
			}
		}
	}

}
