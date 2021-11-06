using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField]
    private bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockBomb;

    [SerializeField]
    private GameObject pickupEffect;

    [SerializeField]
    private string unlockMessage;

    [SerializeField]
    private TMP_Text unlockText;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
            PlayerAbilityTracker player = collision.GetComponentInParent<PlayerAbilityTracker>();

            if (unlockDoubleJump)
			{
                player.CanDoubleJump = true;
			}

            if (unlockDash)
            {
                player.CanDash= true;
            }

            if (unlockBecomeBall)
            {
                player.CanBecomeBall = true;
            }

            if (unlockBomb)
            {
                player.CanDropBomb = true;
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);

            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;

            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);

            Destroy(gameObject);
        }
	}

}
